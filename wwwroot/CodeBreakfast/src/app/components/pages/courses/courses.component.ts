import {Component, OnDestroy, OnInit} from '@angular/core';
import {firstValueFrom,Subscription} from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { CourseService } from '../../../services/course.service';
import { Course, Language } from '../../../models/course.model';
import {AuthService} from '../../../services/auth.service';

@Component({
  selector: 'app-courses',
  imports: [],
  templateUrl: './courses.component.html',
  styleUrls: ['./courses.component.scss']
})
export class CoursesComponent implements OnInit, OnDestroy {
  public courses: Course[] = [];
  private tokenSubscription: Subscription | null = null;

  constructor(private dialog: MatDialog, private courseService: CourseService, private authService: AuthService) {}

  ngOnInit(): void {
    this.tokenSubscription = this.authService.accessToken.subscribe(token => this.getCourses());
  }

  ngOnDestroy(): void {
    this.tokenSubscription?.unsubscribe();
  }

  private async getCourses() {
    this.courses = await firstValueFrom(this.courseService.getAllCourses());
  }

  protected readonly Language = Language;
  protected readonly Number = Number;
}
