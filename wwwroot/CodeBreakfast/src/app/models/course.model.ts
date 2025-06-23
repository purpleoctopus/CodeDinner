import {UserDetail} from './user.model';

export enum Language{
  Ukrainian,
  English
}

export interface CourseAdd {
  name: string;
  description?: string;
  language: Language;
  primarySpecialization: string;
  tags: string[];
}
export interface CourseUpdate extends CourseAdd {
  id: string;
  modules?: string[];
  description?: string;
  isVisible: boolean;
}
export interface CourseDetail extends CourseUpdate {
  author: UserDetail;
  createdOn: Date;
  updatedOn: Date;
}

export interface CourseForList{
  id: string;
  name: string;
  description?: string;
  language: Language;
  author: UserDetail;
  modulesCount: number;
  lessonsCount: number;
  studentsCount: number;
  totalTime: string;
  createdOn: Date;
  updatedOn: Date;
}
