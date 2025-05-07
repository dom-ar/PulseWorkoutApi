using Entities.Models;
using Shared.DataTransferObjects.Avatar;
using Shared.DataTransferObjects.Exercise;

namespace Service.Contracts.Entities;

public interface IAvatarService
{
    // GET
    Task<IEnumerable<AvatarDto>> GetAllAvatarsAsync(bool trackChanges);
    Task<AvatarDto> GetAvatarAsync(int id, bool trackChanges);
    // POST
    Task<AvatarDto> CreateAvatarAsync(AvatarForCreationDto avatar);
    // DELETE
    Task DeleteAvatarAsync(int id, bool trackChanges);
    // PUT
    Task UpdateAvatarAsync(int id, AvatarForUpdateDto avatarForUpdate, bool trackChanges);
}