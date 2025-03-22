using CodeDinner.API.Entities;
using CodeDinner.API.Repositories.Interfaces;

namespace CodeDinner.API.Repositories.Implementation;

public class UserRepository : IUserRepository
{
    public Task<bool> Register(User loginDto)
    {
        throw new NotImplementedException();
    }
}