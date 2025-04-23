import { Injectable } from '@angular/core';
import {environment} from '../../environments/environment';
import {catchError, firstValueFrom, map, of} from 'rxjs';
import {HttpClient} from '@angular/common/http';
import {Course, CourseAddDto, CourseUpdateDto} from '../models/course.model';

@Injectable({
  providedIn: 'root'
})
export class CourseService {
  readonly url: string = `${environment.apiUrl}/Course`;

  constructor(private http: HttpClient) { }

  public getAllCourses(){
    return this.http.get<any>(`${this.url}/GetAll`).pipe(
      map(res => res.data),
      catchError((error)=>{
        return of([]);
      })
    );
  }

  public getCourseById(id: string){
    return this.http.get<any>(`${this.url}/GetById/${id}`).pipe(
      map(res => res.data),
      catchError((error)=>{
        return of(null);
      })
    );
  }

  public createCourse(courseDto: CourseAddDto){
    return this.http.post<any>(`${this.url}/Create`, courseDto).pipe(
      map(res => res.data),
      catchError((error)=>{
        return of(false);
      })
    );
  }

  public updateCourse(courseDto: CourseUpdateDto){
    return this.http.put<any>(`${this.url}/Update`, courseDto).pipe(
      map(res => res.data),
      catchError((error)=>{
        return of(null);
      })
    );
  }

  public deleteCourse(id: string) {
    return this.http.delete<any>(`${this.url}/Delete/${id}`).pipe(
      map(res => res.data),
      catchError((error)=>{
        return of(false);
      })
    );
  }
}
