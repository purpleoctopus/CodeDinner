import { Routes } from '@angular/router';
import {AppComponent} from './app.component';
import {requireAdminGuard} from './guards/require-admin.guard';
import {requireCreatorGuard} from './guards/require-creator.guard';

export const routes: Routes = [
  {
    path: 'home',
    loadComponent: () => import("./components/pages/homepage/homepage.component").then(m => m.HomepageComponent),
    title: 'CodeBreakfast'
  },
  {
    path: 'courses',
    loadComponent: () => AppComponent.showLoadingFromPromise(import("./components/pages/courses/courses.component")).then(m => m.CoursesComponent),
    title: 'Курси'
  },
  {
    path: 'course/add',
    loadComponent: () => AppComponent.showLoadingFromPromise(import("./components/pages/add-course/add-course.component")).then(m => m.AddCourseComponent),
    title: 'Додати курс'
  },
  {
    path: 'course/:id',
    loadComponent: () => AppComponent.showLoadingFromPromise(import("./components/pages/course-detail/course-detail.component")).then(m => m.CourseDetailComponent),
    title: 'Курс'
  },
  {
    path: 'author/dashboard',
    loadComponent: () => AppComponent.showLoadingFromPromise(import("./components/pages/author-dashboard/author-dashboard.component")).then(m => m.AuthorDashboardComponent),
    title: 'Панель автора',
    canActivate: [requireCreatorGuard]
  },
  {
    path: 'my-profile',
    loadComponent: () => AppComponent.showLoadingFromPromise(import("./components/pages/my-profile/my-profile.component")).then(m => m.MyProfileComponent),
    title: 'Мій профіль',
  },
  {
    path: 'my-profile/settings',
    loadComponent: () => AppComponent.showLoadingFromPromise(import("./components/pages/user-settings/user-settings.component")).then(m => m.UserSettingsComponent),
    title: 'Налаштування',
  },
  {
    path: 'user/:id',
    loadComponent: () => AppComponent.showLoadingFromPromise(import("./components/pages/user-profile/user-profile.component")).then(m => m.UserProfileComponent),
    title: 'Профіль користувача'
  },
  {
    path: 'admin',
    loadComponent: () => AppComponent.showLoadingFromPromise(import("./components/pages/admin-dashboard/admin-dashboard.component")).then(m => m.AdminDashboardComponent),
    canActivate: [requireAdminGuard],
    title: 'Панель адміна'
  },
  {
    path: 'admin/users',
    loadComponent: () => AppComponent.showLoadingFromPromise(import("./components/pages/users-list/users-list.component")).then(m => m.UsersListComponent),
    canActivate: [requireAdminGuard],
    title: 'Користувачі'
  },
  {
    path: 'no-access',
    loadComponent: () => AppComponent.showLoadingFromPromise(import("./components/pages/no-access/no-access.component")).then(m => m.NoAccessComponent),
    title: 'Немає доступу',
  },
  {
    path: 'authorize',
    title: 'Авторизація',
    loadComponent: () => AppComponent.showLoadingFromPromise(import("./components/pages/authorize/authorize.component")).then(m => m.AuthorizeComponent)
  },
  {
    path: '**',
    redirectTo: 'home',
    pathMatch: 'full'
  }
];
