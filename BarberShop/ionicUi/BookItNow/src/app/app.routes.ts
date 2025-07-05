import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'register',
    pathMatch: 'full'
  },
  {
    path: 'tabs-barber',
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
  },
  {
    path: 'createBarbershop',
    loadComponent: () => import('./account-page/create-barbershop/create-barbershop.component').then((m) => m.CreateBarbershopComponent),
  },
  {
    path: 'choiceBarbershop',
    loadComponent: () => import('./account-page/choice-barbershop/choice-barbershop.component').then((m) => m.ChoiceBarbershopComponent),
  },
  {
    path: 'barbershopProfile',
    loadComponent: () => import('./barbershop/barbershop-profile/barbershop-profile.component').then((m) => m.BarbershopProfileComponent),
  },
  {
    path: 'salons/:id/barbers',
    loadComponent: () => import('./salon-barbers/salon-barbers.page').then(m => m.SalonBarbersPage)
  },
  {
    path: 'appointment-reservation/:id',
    loadComponent: () => import('./appointment-reservation/appointment-reservation.component').then(m => m.AppointmentReservationComponent)
  },



];



