import { Routes } from '@angular/router';
import { TabsClientComponent } from './TabsClientComponent';

export const routes: Routes = [
  {
    path: '',
    component: TabsClientComponent,
    children: [
      {
        path: 'tab4',
        loadComponent: () => import('../tab4/tab4.component').then(m => m.Tab4Component),
      },
      {
        path: 'tab5',
        loadComponent: () => import('../tab5/tab5.component').then(m => m.Tab5Component),
      },
      {
        path: 'tab6',
        loadComponent: () => import('../tab6/tab6.component').then(m => m.Tab6Component),
      },
      {
        path: '',
        redirectTo: 'tab5',
        pathMatch: 'full',
      }
    ]
  }
];
