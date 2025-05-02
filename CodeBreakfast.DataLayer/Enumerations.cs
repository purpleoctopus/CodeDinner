using System.ComponentModel;

namespace CodeBreakfast.DataLayer.Enumerations;

public enum NotificationType
{
    // All Users 1-99
    Newsletter = 1,
    NewPrivateMessage = 2,
    ProfileFollow = 3,
    CommentReply = 4,
    NewCourseContent = 5,
    
    // Course Lecturers Only 100+
    CourseSubscribe = 100,
    CourseComment = 101
}

public enum AppRole
{
    [Description("User")]
    User = 1,
    [Description("Moderator")]
    Moderator = 2,
    [Description("Admin")]
    Admin = 3,
    [Description("Super Admin")]
    Supervisor = 4
}

public enum CourseRole
{
    Student = 1,
    Lecturer = 2,
    Owner = 3
}

public enum CourseLanguage
{
    Ukrainian,
    English,
}

public enum LessonType
{
    Text = 1,
    Test = 2,
    Video = 3,
}

public enum UserConfigKey
{
    
}