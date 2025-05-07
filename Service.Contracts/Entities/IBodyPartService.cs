using Entities.Models;
using Shared.DataTransferObjects.BodyPart;
using Shared.DataTransferObjects.Exercise;

namespace Service.Contracts.Entities;

public interface IBodyPartService
{
    // GET
    Task<IEnumerable<BodyPartDto>> GetAllBodyPartsAsync(bool trackChanges);
    Task<IEnumerable<BodyPartDto>> GetBodyPartsByIdsAsync(IEnumerable<int> ids, bool trackChanges);
    Task<BodyPartDto> GetBodyPartAsync(int id, bool trackChanges);
    // POST
    Task<BodyPartDto> CreateBodyPartAsync(BodyPartForCreationDto bodyPart);
    // DELETE
    Task DeleteBodyPartAsync(int id, bool trackChanges);
    // PUT
    Task UpdateBodyPartAsync(int id, BodyPartForUpdateDto bodyPartForUpdate, bool trackChanges);
}