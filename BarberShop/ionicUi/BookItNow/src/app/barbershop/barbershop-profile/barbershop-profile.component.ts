import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
//import { IonicModule } from '@ionic/angular';
import {
  IonHeader,
  IonToolbar,
  IonTitle,
  IonContent,
  IonButtons,
  IonMenuButton,
  IonMenu,
  IonLabel,
  IonButton,
  IonItem,
  IonList,
  IonIcon,
  IonSearchbar,
  IonBackButton,
  IonModal,
  IonRouterOutlet,
  IonListHeader,
  IonCard,
  IonCardHeader,
  IonCardTitle,
  IonCardSubtitle,
  IonCardContent,
  IonAvatar,
  IonRow,
  IonCol,
  IonGrid
} from '@ionic/angular/standalone';

@Component({
  selector: 'app-barbershop-profile',
  templateUrl: './barbershop-profile.component.html',
  styleUrls: ['./barbershop-profile.component.scss'],
  imports: [
    IonHeader,
    IonToolbar,
    IonTitle,
    IonContent,
    IonButtons,
    IonMenuButton,
    IonMenu,
    IonLabel,
    IonButton,
    IonItem,
    IonList,
    IonIcon,
    IonSearchbar,
    IonBackButton,
    IonModal,
    IonRouterOutlet,
    IonListHeader,
    IonCard,
    IonCardHeader,
    IonCardTitle,
    IonCardSubtitle,
    IonCardContent,
    IonAvatar,
    IonRow,
    IonCol,
    IonGrid,
    ReactiveFormsModule,
    RouterModule,
    IonRouterOutlet
  ],
  standalone: true,
})
export class BarbershopProfileComponent  implements OnInit {

  constructor() { }

  ngOnInit() {}

}
