export enum AppRole{
  User = 1,
  Creator = 2,
  Moderator = 3,
  Admin = 4,
}

export enum UserProfileSection{
  LastActivity = 1,
  CourseSummary = 2,
  CourseDetailStatistics = 3
}

export interface UserUpdate{
  id: string;
  username: string;
  firstName?: string;
  lastName?: string;
}
export interface UserDetail extends UserUpdate{

}

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
  isPrivate: boolean;
  sectionsToView: UserProfileSection[]
}
