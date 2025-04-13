using CodeBreakfast.DataLayer.Entities;

namespace CodeBreakfast.Data.Repositories.Interfaces;

public interface IUserRepository
{
    Task<bool> Register(User loginDto);
}