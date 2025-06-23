import { Routes } from '@angular/router';
import {AppComponent} from './app.component';
import {requireAdminGuard} from './guards/require-admin.guard';

export const routes: Routes = [
  {
    path: 'home',
    loadComponent: () => import("./components/pages/homepage/homepage.component").then(m => m.HomepageComponent),
    title: 'CodeBreakfast'
  },
  {
    path: 'courses',
    loadComponent: () => AppComponent.showLoadingFromPromise(import("./components/pages/courses/courses.component")).then(m => m.CoursesComponent),
    title: 'Courses'
  },
  {
    path: 'course/:id',
    loadComponent: () => AppComponent.showLoadingFromPromise(import("./components/pages/course-detail/course-detail.component")).then(m => m.CourseDetailComponent),
    title: 'Courses'
  },
  {
    path: 'author/dashboard',
    loadComponent: () => AppComponent.showLoadingFromPromise(import("./components/pages/author-dashboard/author-dashboard.component")).then(m => m.AuthorDashboardComponent),
    title: 'Dashboard'
  },
  {
    path: 'my-profile',
    loadComponent: () => AppComponent.showLoadingFromPromise(import("./components/pages/my-profile/my-profile.component")).then(m => m.MyProfileComponent),
    title: 'Profile',
  },
  {
    path: 'my-profile/settings',
    loadComponent: () => AppComponent.showLoadingFromPromise(import("./components/pages/user-settings/user-settings.component")).then(m => m.UserSettingsComponent),
    title: 'Settings',
  },
  {
    path: 'user/:id',
    loadComponent: () => AppComponent.showLoadingFromPromise(import("./components/pages/user-profile/user-profile.component")).then(m => m.UserProfileComponent),
    title: 'User Profile'
  },
  {
    path: 'admin',
    loadComponent: () => AppComponent.showLoadingFromPromise(import("./components/pages/admin-dashboard/admin-dashboard.component")).then(m => m.AdminDashboardComponent),
    canActivate: [requireAdminGuard],
    title: 'Dashboard'
  },
  {
    path: 'admin/users',
    loadComponent: () => AppComponent.showLoadingFromPromise(import("./components/pages/users-list/users-list.component")).then(m => m.UsersListComponent),
    canActivate: [requireAdminGuard],
    title: 'Users'
  },
  {
    path: 'no-access',
    loadComponent: () => AppComponent.showLoadingFromPromise(import("./components/pages/no-access/no-access.component")).then(m => m.NoAccessComponent),
    title: 'No Access',
  },
  {
    path: 'authorize',
    loadComponent: () => AppComponent.showLoadingFromPromise(import("./components/pages/authorize/authorize.component")).then(m => m.AuthorizeComponent)
  },
  {
    path: '**',
    redirectTo: 'home',
    pathMatch: 'full'
  }
];
