export enum AppRole{
  User = 1,
  Creator = 2,
  Moderator = 3,
  Admin = 4,
}

export interface UserUpdate{
  id: string;
  username: string;
  firstName?: string;
  lastName?: string;
}
export interface UserDetail extends UserUpdate{

}

// public Guid Id { get; set; }
// public AppRole Role { get; set; }
// public string Username { get; set; }
// public string? FirstName { get; set; }
// public string? LastName { get; set; }
// public int CoursesCount { get; set; }
// public int CompletedCoursesCount { get; set; }
// public int? CreatedCoursesCount { get; set; }

export interface UserProfile{
  id: string;
  role: AppRole;
  username: string;
  firstName: string;
  lastName: string;
  coursesCount: number;
  completedCoursesCount: number;
  createdCoursesCount: number;
  registeredOn: Date;
}
