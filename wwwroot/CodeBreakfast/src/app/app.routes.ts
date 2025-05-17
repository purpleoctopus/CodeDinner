import { Routes } from '@angular/router';
import {requireAccessTokenGuard} from './guards/require-access-token.guard';

export const routes: Routes = [
  {
    path: 'home',
    loadComponent: () =>
      import("./components/pages/homepage/homepage.component")
        .then(m => m.HomepageComponent),
    title: 'CodeBreakfast'
  },
  {
    path: 'courses',
    loadComponent: () =>
      import("./components/pages/courses/courses.component")
        .then(m => m.CoursesComponent),
    title: 'CodeBreakfast - Courses'
  },
  {
    path: 'course/:id',
    loadComponent: () =>
      import("./components/pages/course-detail/course-detail.component")
        .then(m => m.CourseDetailComponent),
    title: 'CodeBreakfast - Courses'
  },
  {
    path: 'my-profile',
    loadComponent: () =>
      import("./components/pages/my-profile/my-profile.component")
        .then(m => m.MyProfileComponent),
    title: 'CodeBreakfast - Profile',
    canActivate: [requireAccessTokenGuard],
  },
  {
    path: 'no-access',
    loadComponent: () =>
      import("./components/pages/no-access/no-access.component")
        .then(m => m.NoAccessComponent),
    title: 'No Access',
  },
  {
    path: '**',
    redirectTo: 'home',
    pathMatch: 'full'
  }
];
