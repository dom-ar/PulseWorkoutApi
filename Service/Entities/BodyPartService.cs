using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts.Entities;
using Shared.DataTransferObjects.BodyPart;

namespace Service.Entities;

internal sealed class BodyPartService : IBodyPartService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;

    public BodyPartService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    // GET

    public async Task<IEnumerable<BodyPartDto>> GetAllBodyPartsAsync(bool trackChanges)
    {
        var bodyParts = await _repository.BodyPart.GetAllBodyPartsAsync(trackChanges);
        var bodyPartsDto = _mapper.Map<IEnumerable<BodyPartDto>>(bodyParts);
        return bodyPartsDto;
    }

    public async Task<IEnumerable<BodyPartDto>> GetBodyPartsByIdsAsync(IEnumerable<int> ids, bool trackChanges)
    {
        if (ids is null)
            throw new IdParametersBadRequestException();

        var bodyPartEntities = await _repository.BodyPart.GetBodyPartsByIdsAsync(ids, trackChanges);
        if (ids.Count() != bodyPartEntities.Count())
            throw new CollectionByIdsBadRequestException();

        var bodyPartsToReturn = _mapper.Map<IEnumerable<BodyPartDto>>(bodyPartEntities);
        return bodyPartsToReturn;
    }

    public async Task<BodyPartDto> GetBodyPartAsync(int id, bool trackChanges)
    {
        var bodyPart = await _repository.BodyPart.GetBodyPartAsync(id, trackChanges);
        if (bodyPart is null)
        {
            throw new UniversalNotFoundException("bodyPart", id);
        }

        var bodyPartDto = _mapper.Map<BodyPartDto>(bodyPart);
        return bodyPartDto;
    }

    // POST

    public async Task<BodyPartDto> CreateBodyPartAsync(BodyPartForCreationDto bodyPart)
    {
        var bodyPartEntity = _mapper.Map<BodyPart>(bodyPart);

        _repository.BodyPart.CreateBodyPart(bodyPartEntity);
        await _repository.SaveAsync();

        var bodyPartToReturn = _mapper.Map<BodyPartDto>(bodyPartEntity);
        return bodyPartToReturn;
    }

    // DELETE

    public async Task DeleteBodyPartAsync(int id, bool trackChanges)
    {
        var bodyPart = await _repository.BodyPart.GetBodyPartAsync(id, trackChanges);
        if (bodyPart is null)
            throw new UniversalNotFoundException("bodyPart", id);

        _repository.BodyPart.DeleteBodyPart(bodyPart);
        await _repository.SaveAsync();
    }

    // PUT

    public async Task UpdateBodyPartAsync(int id, BodyPartForUpdateDto bodyPartForUpdate, bool trackChanges)
    {
        var bodyPartEntity = await _repository.BodyPart.GetBodyPartAsync(id, trackChanges);
        if (bodyPartEntity is null)
            throw new UniversalNotFoundException("bodyPart", id);

        _mapper.Map(bodyPartForUpdate, bodyPartEntity);
        await _repository.SaveAsync();
    }
}