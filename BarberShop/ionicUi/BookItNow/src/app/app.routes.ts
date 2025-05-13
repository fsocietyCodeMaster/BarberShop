import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'register',
    pathMatch: 'full'
  },
  {
    path: 'tabs-client',
    loadChildren: () => import('./tabs-client/tabs-client/tabs-client.routes').then(m => m.routes),
  },
  {
    path: 'tabs-barbershop',
    loadChildren: () => import('./tabs-barbershop/tabs-barbershop/tabs-barbershop.routes').then(m => m.routes),
  },
  {
    path: '',
    loadChildren: () => import('./tabs/tabs.routes').then((m) => m.routes),
  },
  {
    path: 'register',
    loadComponent: () => import('./account-page/register/register.component').then((m) => m.RegisterComponent),
  },
  {
    path: 'login',
    loadComponent: () => import('./account-page/login/login.component').then((m) => m.LoginComponent),
  }
];



