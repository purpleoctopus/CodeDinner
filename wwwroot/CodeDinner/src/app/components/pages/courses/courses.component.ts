import {Component, inject, OnDestroy, OnInit} from '@angular/core';
import { MatFabButton, MatIconButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { AddCourseFormComponent } from '../../features/add-course-form/add-course-form.component';
import {BehaviorSubject, debounceTime, firstValueFrom, skip, Subscription} from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { CourseService } from '../../../services/course.service';
import { Course, Language } from '../../../models/course.model';
import { MatTableModule } from '@angular/material/table';
import { MatInputModule } from '@angular/material/input';
import { FormsModule } from '@angular/forms';
import { MatOption } from '@angular/material/core';
import { MatSelect } from '@angular/material/select';
import { Store} from '@ngrx/store';
import {selectAccessToken} from '../../../store/auth/selectors';

@Component({
  selector: 'app-courses',
  imports: [MatFabButton, MatIcon, MatIconButton, MatTableModule, MatInputModule, FormsModule, MatOption, MatSelect],
  templateUrl: './courses.component.html',
  styleUrls: ['./courses.component.scss']
})
export class CoursesComponent implements OnInit, OnDestroy {
  protected displayedColumns: string[] = ['name', 'language', 'actions'];
  public courses: Course[] = [];
  private courseSubject: BehaviorSubject<Course[]> = new BehaviorSubject<Course[]>([]);
  private courseSubscription: Subscription | null = null;
  private storeSubscription: Subscription | null = null;

  constructor(private dialog: MatDialog, private courseService: CourseService, private store: Store) {}

  ngOnInit(): void {
    this.getCourses();
    this.courseSubscription = this.courseSubject
      .pipe(debounceTime(1000)).subscribe(async courses => {
      const uniqueCourses = this.getUniqueChanges(courses);
      uniqueCourses.forEach(course => {
        console.log(course);
        this.courseService.updateCourse(course);
      });
      if (uniqueCourses.length > 0) {
        this.courseSubject.next([]);
      }
    });

    this.storeSubscription = this.store.select(selectAccessToken).pipe(skip(1)).subscribe(() => {
      this.getCourses();
    });
  }

  ngOnDestroy(): void {
    this.courseSubscription?.unsubscribe();
    this.storeSubscription?.unsubscribe();
  }

  public async openAddForm() {
    const dialog = this.dialog.open(AddCourseFormComponent);
    const dialogResult = await firstValueFrom(dialog.afterClosed());
    if(dialogResult){
      const res = await firstValueFrom(this.courseService.createCourse(dialogResult));
      this.courses = [...this.courses, res];
    }
  }

  private async getCourses() {
    this.courses = await firstValueFrom(this.courseService.getAllCourses());
  }

  protected async deleteCourse(id: string) {
    if(await firstValueFrom(this.courseService.deleteCourse(id))){
      this.courses = this.courses.filter(course => course.id !== id);
    }
  }

  protected change(value: Course) {
    const prev = [...this.courseSubject.value];
    const index = prev.findIndex(course => course.id === value.id);
    if (index > -1) {
      prev[index] = value;
    } else {
      prev.push(value);
    }
    this.courseSubject.next(prev);
  }

  private getUniqueChanges(courses: Course[]): Course[] {
    const map = new Map<string, Course>();
    courses.forEach(course => {
      map.set(course.id, course);
    });
    return Array.from(map.values());
  }

  protected readonly Language = Language;
  protected readonly Number = Number;
}
