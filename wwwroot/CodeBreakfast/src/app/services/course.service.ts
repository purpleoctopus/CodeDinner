import { Injectable } from '@angular/core';
import {environment} from '../../environments/environment';
import {map, Observable} from 'rxjs';
import {HttpClient} from '@angular/common/http';
import {CourseDetail, CourseAdd, CourseUpdate, CourseForList} from '../models/course.model';
import {ApiResponse} from '../models/response.model';

@Injectable({
  providedIn: 'root'
})
export class CourseService {
  private readonly url: string = `${environment.apiUrl}/course`;

  constructor(private http: HttpClient) { }

  public getCoursesForListView(): Observable<ApiResponse<CourseForList[]>> {
    return this.http.get<ApiResponse<CourseForList[]>>(`${this.url}/for-list-view`).pipe(
      map(res => {
        res.data!.map((x : CourseForList)=>{
          x.createdOn = new Date(x.createdOn);
          x.updatedOn = new Date(x.updatedOn);
        })
        return res;
      })
    );
  }

  public getCourseById(id: string): Observable<ApiResponse<CourseDetail>>{
    return this.http.get<ApiResponse<CourseDetail>>(`${this.url}/${id}`);
  }

  public createCourse(courseDto: CourseAdd): Observable<ApiResponse<CourseDetail> | null> {
    return this.http.post<ApiResponse<CourseDetail>>(`${this.url}`, courseDto);
  }

  public updateCourse(courseDto: CourseUpdate): Observable<ApiResponse<CourseDetail>> {
    return this.http.put<ApiResponse<CourseDetail>>(`${this.url}`, courseDto);
  }

  public deleteCourse(id: string): Observable<ApiResponse<boolean>> {
    return this.http.delete<ApiResponse<boolean>>(`${this.url}/${id}`);
  }
}
