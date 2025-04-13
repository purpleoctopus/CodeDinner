using CodeBreakfast.Data.Repositories.Interfaces;
using CodeBreakfast.DataLayer.Entities;

namespace CodeBreakfast.Data.Repositories.Implementation;

public class UserRepository : IUserRepository
{
    public Task<bool> Register(User loginDto)
    {
        throw new NotImplementedException();
    }
}