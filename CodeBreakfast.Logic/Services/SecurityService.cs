using CodeBreakfast.Common;
using CodeBreakfast.Data.Repositories.Interfaces;
using CodeBreakfast.DataLayer.Enumerations;
using CodeBreakfast.Logic.Services.Interfaces;

namespace CodeBreakfast.Logic.Services;

public class SecurityService(IUserRepository userRepository) : ISecurityService
{
    public async Task<SectionAccess> CourseAccessLevel(Guid requestingUserId, Guid courseId)
    {
        var userCourse = await userRepository.GetUserCourseForUserAsync(courseId, requestingUserId);
        if (userCourse == null)
        {
            return SectionAccess.None;
        }
        return userCourse.Role is CourseRole.Owner or CourseRole.Lecturer ? 
            SectionAccess.Manage : SectionAccess.View;
    }
}