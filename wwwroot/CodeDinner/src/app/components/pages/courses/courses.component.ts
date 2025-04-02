import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { MatFabButton, MatIconButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { AddCourseFormComponent } from '../../features/add-course-form/add-course-form.component';
import { BehaviorSubject, debounceTime, firstValueFrom } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { CourseService } from '../../../services/course.service';
import { Course, Language } from '../../../models/course.model';
import { MatTableModule } from '@angular/material/table';
import { MatInputModule } from '@angular/material/input';
import { FormsModule } from '@angular/forms';
import { MatOption } from '@angular/material/core';
import { MatSelect } from '@angular/material/select';

@Component({
  selector: 'app-courses',
  imports: [MatFabButton, MatIcon, MatIconButton, MatTableModule, MatInputModule, FormsModule, MatOption, MatSelect],
  templateUrl: './courses.component.html',
  styleUrls: ['./courses.component.scss']
})
export class CoursesComponent implements OnInit {
  protected displayedColumns: string[] = ['name', 'language', 'actions'];
  public courses: Course[] = [];
  private courseSubject: BehaviorSubject<Course[]> = new BehaviorSubject<Course[]>([]);

  constructor(private view: ViewContainerRef, private dialog: MatDialog, private courseService: CourseService) {}

  ngOnInit(): void {
    this.getCourses();
    this.courseSubject.pipe(debounceTime(1000)).subscribe(async courses => {
      // Унікальні зміни в масиві
      const uniqueCourses = this.getUniqueChanges(courses);
      uniqueCourses.forEach(course => {
        console.log(course);
        this.courseService.updateCourse(course);
      });
      if (uniqueCourses.length > 0) {
        this.courseSubject.next([]); // Очищення після оновлення
      }
    });
  }

  public async openAddForm() {
    const dialog = this.dialog.open(AddCourseFormComponent);
    const res = await firstValueFrom(dialog.afterClosed());
    if(res && typeof res != 'boolean'){
      this.courses = [...this.courses, res];
    }
  }

  private async getCourses() {
    this.courses = await this.courseService.getAllCourses();
  }

  protected async deleteCourse(id: string) {
    if(await this.courseService.deleteCourse(id)){
      this.courses = this.courses.filter(course => course.id !== id);
    }
  }

  protected change(value: Course) {
    const prev = [...this.courseSubject.value]; // Копія поточного масиву
    const index = prev.findIndex(course => course.id === value.id);
    if (index > -1) {
      prev[index] = value; // Оновлення існуючого значення
    } else {
      prev.push(value); // Додавання нового значення
    }
    this.courseSubject.next(prev); // Оновлення з урахуванням debounce
  }

  private getUniqueChanges(courses: Course[]): Course[] {
    const map = new Map<string, Course>();
    courses.forEach(course => {
      map.set(course.id, course); // Використання ID як ключа
    });
    return Array.from(map.values()); // Унікальні значення
  }

  protected readonly Language = Language;
  protected readonly Number = Number;
}
