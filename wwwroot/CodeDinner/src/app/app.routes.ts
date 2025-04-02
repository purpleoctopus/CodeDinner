import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: 'home',
    loadComponent: () =>
      import("./components/pages/homepage/homepage.component")
        .then(m => m.HomepageComponent),
  },
  {
    path: 'courses',
    loadComponent: () =>
      import("./components/pages/courses/courses.component")
        .then(m => m.CoursesComponent),
  },
  {
    path: '**',
    redirectTo: 'home',
    pathMatch: 'full'
  }
];
