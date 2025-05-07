using Contracts.Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Entities;

public class AvatarRepository : RepositoryBase<Avatar>, IAvatarRepository
{
    public AvatarRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    // GET
    public async Task<IEnumerable<Avatar>> GetAllAvatarsAsync(bool trackChanges) =>
        await FindAll(trackChanges)
            .OrderBy(a => a.Id)
            .ToListAsync();

    public async Task<Avatar?> GetAvatarAsync(int avatarId, bool trackChanges) =>
        await FindByCondition(a => a.Id.Equals(avatarId), trackChanges)
            .SingleOrDefaultAsync();

    // POST

    public void CreateAvatar(Avatar avatar)
    {
        Create(avatar);
    }

    // DELETE

    public void DeleteAvatar(Avatar avatar) => Delete(avatar);
}