using Entities.Models;

namespace Contracts.Entities;

public interface IAvatarRepository
{
    // GET
    Task<IEnumerable<Avatar>> GetAllAvatarsAsync(bool trackChanges);
    Task<Avatar?> GetAvatarAsync(int avatarId, bool trackChanges);
    // POST
    void CreateAvatar(Avatar avatar);
    // DELETE
    void DeleteAvatar(Avatar avatar);
}