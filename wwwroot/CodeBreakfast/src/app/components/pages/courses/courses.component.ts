import {Component, OnDestroy, OnInit} from '@angular/core';
import {firstValueFrom, Subscription} from 'rxjs';
import {MatDialog} from '@angular/material/dialog';
import {CourseService} from '../../../services/course.service';
import {CourseForList, Language} from '../../../models/course.model';
import {AuthService} from '../../../services/auth.service';

@Component({
  selector: 'app-courses',
  imports: [],
  templateUrl: './courses.component.html',
  styleUrls: ['./courses.component.scss']
})
export class CoursesComponent implements OnInit, OnDestroy {
  public courses: CourseForList[] = [];
  private tokenSubscription: Subscription | null = null;

  constructor(private dialog: MatDialog, private courseService: CourseService, private authService: AuthService) {}

  ngOnInit(): void {
    this.tokenSubscription = this.authService.sessionData.subscribe(() => this.getCourses());
  }

  ngOnDestroy(): void {
    this.tokenSubscription?.unsubscribe();
  }

  private async getCourses() {
    try {
      const res = await firstValueFrom(this.courseService.getCoursesForListView());
      if (res.data) {
        this.courses = res.data;
      }
    }catch(err) {
      this.courses = [];
    }
  }

  protected readonly Language = Language;
  protected readonly Number = Number;
}
