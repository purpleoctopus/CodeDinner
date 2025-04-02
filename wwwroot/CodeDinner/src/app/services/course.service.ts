import { Injectable } from '@angular/core';
import {environment} from '../../environments/environment';
import {firstValueFrom} from 'rxjs';
import {HttpClient} from '@angular/common/http';
import {Course, CourseAddDto, CourseUpdateDto} from '../models/course.model';

@Injectable({
  providedIn: 'root'
})
export class CourseService {
  readonly url: string = `${environment.apiUrl}/Course`;

  constructor(private http: HttpClient) { }

  public async getAllCourses(): Promise<Course[]>{
    return (await firstValueFrom(this.http.get<any>(`${this.url}/GetAll`))).data;
  }

  public async getCourseById(id: string): Promise<Course>{
    return (await firstValueFrom(this.http.get<any>(`${this.url}/GetById/${id}`))).data;
  }

  public async createCourse(courseDto: CourseAddDto): Promise<Course> {
    return (await firstValueFrom(this.http.post<any>(`${this.url}/Create`, courseDto))).data;
  }

  public async updateCourse(courseDto: CourseUpdateDto): Promise<Course> {
    return (await firstValueFrom(this.http.put<any>(`${this.url}/Update`, courseDto))).data;
  }

  public async deleteCourse(id: string): Promise<boolean> {
    return (await firstValueFrom(this.http.delete<any>(`${this.url}/Delete/${id}`))).data;
  }
}
