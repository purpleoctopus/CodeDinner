import { Routes } from '@angular/router';
import {requireAccessTokenGuard} from './guards/require-access-token.guard';

export const routes: Routes = [
  {
    path: 'home',
    loadComponent: () => import("./components/pages/homepage/homepage.component").then(m => m.HomepageComponent),
    title: 'CodeBreakfast'
  },
  {
    path: 'courses',
    loadComponent: () => import("./components/pages/courses/courses.component").then(m => m.CoursesComponent),
    title: 'Courses'
  },
  {
    path: 'course/:id',
    loadComponent: () => import("./components/pages/course-detail/course-detail.component").then(m => m.CourseDetailComponent),
    title: 'Courses'
  },
  {
    path: 'my-profile',
    loadComponent: () => import("./components/pages/my-profile/my-profile.component").then(m => m.MyProfileComponent),
    title: 'Profile',
    canActivate: [requireAccessTokenGuard],
  },
  {
    path: 'no-access',
    loadComponent: () => import("./components/pages/no-access/no-access.component").then(m => m.NoAccessComponent),
    title: 'No Access',
  },
  {
    path: 'my-profile/settings',
    loadComponent: () => import("./components/pages/user-settings/user-settings.component").then(m => m.UserSettingsComponent),
    title: 'Settings',
  },
  {
    path: '**',
    redirectTo: 'home',
    pathMatch: 'full'
  }
];
