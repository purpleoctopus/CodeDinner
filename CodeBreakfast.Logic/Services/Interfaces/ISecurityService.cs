using CodeBreakfast.Common;

namespace CodeBreakfast.Logic.Services.Interfaces;

public interface ISecurityService
{
    Task<SectionAccess> CourseAccessLevel(Guid requestingUserId, Guid courseId);
}