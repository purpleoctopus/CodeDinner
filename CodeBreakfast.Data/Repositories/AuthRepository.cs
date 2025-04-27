using CodeBreakfast.Data.Repositories.Interfaces;
using CodeBreakfast.DataLayer.Entities;

namespace CodeBreakfast.Data.Repositories;

public class AuthRepository : IAuthRepository
{
    public Task<bool> Register(User loginDto)
    {
        throw new NotImplementedException();
    }
}