using CodeBreakfast.DataLayer.Entities;

namespace CodeBreakfast.Data.Repositories.Interfaces;

public interface IAuthRepository
{
    Task<bool> Register(User loginDto);
}