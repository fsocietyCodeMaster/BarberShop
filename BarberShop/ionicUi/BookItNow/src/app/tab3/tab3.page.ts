import { Component } from '@angular/core';
import { IonHeader, IonToolbar, IonTitle, IonContent, IonList, IonCard, IonCardHeader, IonCardTitle, IonCardContent } from '@ionic/angular/standalone';
import { ExploreContainerComponent } from '../explore-container/explore-container.component';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-tab3',
  templateUrl: 'tab3.page.html',
  styleUrls: ['tab3.page.scss'],
  imports: [CommonModule,
    IonHeader,
    IonToolbar,
    IonTitle,
    IonContent,
    IonList,
    IonCard,
    IonCardHeader,
    IonCardTitle,
    IonCardContent,
    ExploreContainerComponent],
})
export class Tab3Page {
  constructor() { }

  appointments = [
    {
      date: '2025-07-10',
      customerName: 'علی رضایی',
      salonName: 'قیچی طلایی',
      salonAddress: 'تهران، خیابان ولیعصر',
      salonPhone: '09123456789',
      time: '10:15'
    },
    {
      date: '2025-07-11',
      customerName: 'مینا احمدی',
      salonName: 'آرایشگاه صدف',
      salonAddress: 'تهران، بلوار کشاورز',
      salonPhone: '09121234567',
      time: '14:30'
    },
  ];


}
