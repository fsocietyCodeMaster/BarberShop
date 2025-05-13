import { Routes } from '@angular/router';
import { TabsBarbershopComponent } from './tabs-barbershop.component';

export const routes: Routes = [
  {
    path: '',
    component: TabsBarbershopComponent,
    children: [
      {
        path: 'tab7',
        loadComponent: () => import('../tab7/tab7.component').then(m => m.Tab7Component),
      },
      {
        path: 'tab8',
        loadComponent: () => import('../tab8/tab8.component').then(m => m.Tab8Component),
      },
      {
        path: 'tab9',
        loadComponent: () => import('../tab9/tab9.component').then(m => m.Tab9Component),
      },
      {
        path: '',
        redirectTo: 'tab8',
        pathMatch: 'full',
      }
    ]
  }
];
