﻿using CodeBreakfast.Data.Entities;

namespace CodeBreakfast.Data.Repositories.Interfaces;

public interface ICourseRepository
{
    Task<List<Course>> GetAllAsync();
    Task<List<Course>> GetAllByAuthorAsync(Guid userId);
    Task<Course?> GetByIdAsync(Guid id);
    Task<Course?> GetByIdAsync(Guid id, Guid userId);
    Task<Course?> AddAsync(Course course);
    Task<Course?> UpdateAsync(Course course);
    Task<bool> DeleteAsync(Guid id);
}