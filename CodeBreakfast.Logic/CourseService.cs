using System.Net;
using CodeBreakfast.Common;
using CodeBreakfast.Common.Models;
using CodeBreakfast.Data;
using CodeBreakfast.Data.Repositories.Interfaces;
using CodeBreakfast.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodeBreakfast.Logic;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;
    private readonly AppDbContext _appDbContext;

    public CourseService(ICourseRepository courseRepository, AppDbContext appDbContext)
    {
        _courseRepository = courseRepository;
        _appDbContext = appDbContext;
    }

    #region Get Methods
    
    public async Task<ApiResponse<List<CourseDetailDto>>> GetAllAsync()
    {
        var response = new ApiResponse<List<CourseDetailDto>>();

        try
        {
            var data = await _courseRepository.GetAllAsync();
            response.Data = data.Select(x=>x.GetCommonModel()).ToList();
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.StatusCode = HttpStatusCode.InternalServerError;
        }

        return response;
    }

    public async Task<ApiResponse<List<CourseForListDto>>> Get_ForListViewAsync()
    {
        var response = new ApiResponse<List<CourseForListDto>>();
        var coursesForList = new List<CourseForListDto>();

        try
        {
            var courses = await _courseRepository.GetAllAsync();
            foreach (var course in courses)
            {
                var associatedLessons = await _appDbContext.Lessons.Where(l => l.CourseId == course.Id).ToListAsync();
                
                var courseForView = new CourseForListDto
                {
                    Id = course.Id,
                    Name = course.Name,
                    Language = course.Language,
                    ModulesCount = course.Modules.Count,
                    StudentsCount = await _appDbContext.UserCourses
                        .Where(x => x.CourseId == course.Id).CountAsync(),
                    LessonsCount = associatedLessons.Where(x=>x.CourseId == course.Id).Count(),
                    TotalTime = TimeSpan.FromMinutes(associatedLessons.Select(l=>l.Duration?.TotalMinutes).Sum() ?? 0),
                    Author = (await _appDbContext.Users.SingleAsync(x => x.Id == course.AuthorId)).GetCommonModel(),
                    CreatedOn = course.CreatedOn,
                    UpdatedOn = course.UpdatedOn,
                };
                
                coursesForList.Add(courseForView);
            }
            response.Data = coursesForList;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.StatusCode = HttpStatusCode.InternalServerError;
        }

        return response;
    }

    public async Task<ApiResponse<CourseDetailDto>> GetByIdAsync(Guid id)
    {
        var response = new ApiResponse<CourseDetailDto>();

        try
        {
            var data = await _courseRepository.GetByIdAsync(id);
            if (data == null)
            {
                response.Success = false;
                response.Message = "Course not found";
                response.StatusCode = HttpStatusCode.NotFound;
                return response;
            }
            response.Data = data.GetCommonModel();
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.StatusCode = HttpStatusCode.InternalServerError;
        }

        return response;
    }

    public Task<ApiResponse<CourseDetailDto>> GetByNameAsync(string courseName)
    {
        throw new NotImplementedException();
    }
    
    #endregion

    #region Add Methods
    
    public async Task<ApiResponse<CourseDetailDto>> AddAsync(CourseAddDto dto, Guid userId)
    {
        var response = new ApiResponse<CourseDetailDto>();
        
        try
        {
            var course = dto.GetEntity();
            
            course.CreatedOn = DateTime.Now;
            course.UpdatedOn = DateTime.Now;
            
            response.Data = (await _courseRepository.AddAsync(course)).GetCommonModel();
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.StatusCode = HttpStatusCode.InternalServerError;
        }

        return response;
    }
    
    #endregion
    
    #region Update Methods

    public async Task<ApiResponse<CourseDetailDto>> UpdateAsync(CourseUpdateDto dto)
    {
        var response = new ApiResponse<CourseDetailDto>();

        try
        {
            var course = dto.GetEntity();
            course.UpdatedOn = DateTime.Now;
            var data = await _courseRepository.UpdateAsync(course);
            if (data == null)
            {
                response.Success = false;
                response.Message = "Course not found";
                response.StatusCode = HttpStatusCode.NotFound;
                return response;
            }
            response.Data = course.GetCommonModel();
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.StatusCode = HttpStatusCode.InternalServerError;
        }

        return response;
    }
    
    #endregion
    
    #region Delete Methods
    
    public async Task<ApiResponse<bool>> DeleteAsync(Guid id)
    {
        var response = new ApiResponse<bool>();
        
        try
        {
            response.Data = await _courseRepository.DeleteAsync(id);
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.StatusCode = HttpStatusCode.InternalServerError;
        }

        return response;
    }
    
    #endregion
}