using System.ComponentModel;

namespace CodeBreakfast.Data;

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

public enum ActivityType
{
    Course = 1,
    Lesson = 2
}

public enum AppRole
{
    [Description("User")]
    User = 1,
    [Description("Creator")]
    Creator = 2,
    [Description("Moderator")]
    Moderator = 3,
    [Description("Admin")]
    Admin = 4,
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