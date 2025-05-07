using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts.Entities;
using Shared.DataTransferObjects.Avatar;

namespace Service.Entities;

internal sealed class AvatarService : IAvatarService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;

    public AvatarService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    // GET
    public async Task<IEnumerable<AvatarDto>> GetAllAvatarsAsync(bool trackChanges)
    {
            var avatars = await _repository.Avatar.GetAllAvatarsAsync(trackChanges);
            var avatarsDto = _mapper.Map<IEnumerable<AvatarDto>>(avatars);
            return avatarsDto;
    }

    public async Task<AvatarDto> GetAvatarAsync(int id, bool trackChanges)
    {
        var avatar = await _repository.Avatar.GetAvatarAsync(id, trackChanges);
        if (avatar is null)
        {
            throw new UniversalNotFoundException("avatar", id);
        }

        var avatarDto = _mapper.Map<AvatarDto>(avatar);
        return avatarDto;
    }

    // POST

    public async Task<AvatarDto> CreateAvatarAsync(AvatarForCreationDto avatar)
    {
        var avatarEntity = _mapper.Map<Avatar>(avatar);

        _repository.Avatar.CreateAvatar(avatarEntity);
        await _repository.SaveAsync();

        var avatarToReturn = _mapper.Map<AvatarDto>(avatarEntity);
        return avatarToReturn;
    }

    // DELETE
    public async Task DeleteAvatarAsync(int id, bool trackChanges)
    {
        var avatar = await _repository.Avatar.GetAvatarAsync(id, trackChanges);
        if (avatar is null)
            throw new UniversalNotFoundException("avatar", id);

        _repository.Avatar.DeleteAvatar(avatar);
        await _repository.SaveAsync();
    }

    // PUT

    public async Task UpdateAvatarAsync(int id, AvatarForUpdateDto avatarForUpdate, bool trackChanges)
    {
        var avatarEntity = await _repository.Avatar.GetAvatarAsync(id, trackChanges);
        if (avatarEntity is null)
            throw new UniversalNotFoundException("avatar", id);

        _mapper.Map(avatarForUpdate, avatarEntity);
        await _repository.SaveAsync();
    }
}