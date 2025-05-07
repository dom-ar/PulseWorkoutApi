using Entities.Models;

namespace Contracts.Entities;
public interface IBodyPartRepository
{
    // GET
    Task<IEnumerable<BodyPart>> GetAllBodyPartsAsync(bool trackChanges);
    Task<IEnumerable<BodyPart>> GetBodyPartsByIdsAsync(IEnumerable<int> ids, bool trackChanges);
    Task<BodyPart?> GetBodyPartAsync(int bodyPartId, bool trackChanges);
    // POST
    void CreateBodyPart(BodyPart bodyPart);
    // DELETE
    void DeleteBodyPart(BodyPart bodyPart);
}