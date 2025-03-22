using CodeDinner.API.Entities;
using CodeDinner.API.Models;
using CodeDinner.API.Models.DTOs;

namespace CodeDinner.API.Repositories.Interfaces;

public interface IUserRepository
{
    Task<bool> Register(User loginDto);
}