export enum Language{
  Ukrainian,
  English
}

export interface CourseAddDto {
  name: string;
  language: Language;
}

export interface CourseUpdateDto extends CourseAddDto {
  id: string;
  modules?: string[];
}

export interface Course extends CourseUpdateDto {

}
