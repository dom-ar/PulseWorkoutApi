using Contracts.Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Entities;

public class BodyPartRepository : RepositoryBase<BodyPart>, IBodyPartRepository
{
    public BodyPartRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    // GET
    public async Task<IEnumerable<BodyPart>> GetAllBodyPartsAsync(bool trackChanges) =>
        await FindAll(trackChanges)
            .OrderBy(b => b.Name)
            .ToListAsync();

    public async Task<IEnumerable<BodyPart>> GetBodyPartsByIdsAsync(IEnumerable<int> ids, bool trackChanges) => 
        await FindByCondition(b => ids.Contains(b.Id), trackChanges)
            .OrderBy(b => b.Name)
            .ToListAsync();

    public async Task<BodyPart?> GetBodyPartAsync(int bodyPartId, bool trackChanges) =>
        await FindByCondition(b => b.Id.Equals(bodyPartId), trackChanges)
            .SingleOrDefaultAsync();

    // POST
    public void CreateBodyPart(BodyPart bodyPart)
    {
        Create(bodyPart);
    }

    // DELETE

    public void DeleteBodyPart(BodyPart bodyPart) => Delete(bodyPart);
}