export enum LessonType {
  Text = 1,
  Test = 2,
  Video = 3
}

export interface LessonAdd {
  lessonType: LessonType;
  courseId: string;
  name: string;
  description: string;
  htmlContent: string;
}
export interface LessonUpdate extends LessonAdd {
  id: string;
}
export interface LessonDetail extends LessonUpdate {
  duration: string;
  createdOn: string;
  updatedOn: string;
}
