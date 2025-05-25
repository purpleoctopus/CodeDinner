import {Component, OnDestroy, OnInit} from '@angular/core';
import {firstValueFrom, Subscription} from 'rxjs';
import {MatDialog} from '@angular/material/dialog';
import {CourseService} from '../../../services/course.service';
import {CourseForList, Language} from '../../../models/course.model';
import {AuthService} from '../../../services/auth.service';
import {AppComponent} from '../../../app.component';
import {RouterLink} from '@angular/router';

@Component({
  selector: 'app-courses',
  imports: [
    RouterLink
  ],
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
      const promise = AppComponent.showLoadingFromPromise(firstValueFrom(this.courseService.getCoursesForListView()));
      const res = await promise;
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
