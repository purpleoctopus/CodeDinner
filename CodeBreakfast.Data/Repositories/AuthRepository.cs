using CodeBreakfast.Data.Entities;
using CodeBreakfast.Data.Repositories.Interfaces;

namespace CodeBreakfast.Data.Repositories;

public class AuthRepository : IAuthRepository
{
    public Task<bool> RegisterAsync(User loginDto)
    {
        throw new NotImplementedException();
    }
}