export enum UserConfigKey{
  IsPrivate = 1,


  // View Profile sections 100+
  ViewCourseSummary = 100,
  ViewCourseDetailStatistics = 101,
  ViewLastActivity = 102
}

export type ConfigKeySectionVisibility = Extract<UserConfigKey, 100 | 101 | 102>

export interface UserConfigUpdateDto<T extends string | boolean> {
  key: UserConfigKey;
  value: T;
}
export interface UserConfigDetailDto<T extends string | boolean> extends UserConfigUpdateDto<T> {
  userId: string;
}
