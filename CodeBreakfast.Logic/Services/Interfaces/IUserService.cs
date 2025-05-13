using CodeBreakfast.Common.Models;
using CodeBreakfast.Data.Entities;

namespace CodeBreakfast.Logic.Services.Interfaces;

public interface IUserService
{
    Task<ApiResponse<UserProfileDto>> GetUserProfileForView(Guid requestingUserId, Guid userId);
}