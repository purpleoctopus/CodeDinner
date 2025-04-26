import { Routes } from '@angular/router';

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
    path: '**',
    redirectTo: 'home',
    pathMatch: 'full'
  }
];
