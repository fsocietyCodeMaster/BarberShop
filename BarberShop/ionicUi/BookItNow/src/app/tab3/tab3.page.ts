import { Component } from '@angular/core';
import { IonHeader, IonToolbar, IonTitle, IonContent, IonList, IonCard, IonCardHeader, IonCardTitle, IonCardContent } from '@ionic/angular/standalone';
import { ExploreContainerComponent } from '../explore-container/explore-container.component';
import { CommonModule } from '@angular/common';
import { UserService } from '../services/user.service';
import { clientAppointment } from '../models/user'
import { Jalali } from 'jalali-ts';

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
  constructor(private userservice: UserService) { }

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

  clientappointment: clientAppointment = {

    appointmentDate: "2025-06-25T00:00:00",
    barberName: "",
    barberShopAddress: "",
    barberShopName: "",
    barberShopPhone: "",
    startTime: "",
    endTime: "",
  };

  shamsiDate: string = '';


  ngOnInit() {
    this.getclientappointment();
  }

  getclientappointment() {
    this.userservice.getclientappointment().subscribe((data: any) => {

      this.clientappointment = data.data;

      console.log("clientappointment: ", this.clientappointment);

      this.changeDate(this.clientappointment.appointmentDate);
    })
  }

  changeDate(gregorianDateStr: string) {
   
    const gregorianDate = new Date(gregorianDateStr);

    const jalaliDate = new Jalali(gregorianDate);
   
    this.shamsiDate = jalaliDate.format('YYYY/MM/DD');

    console.log(this.shamsiDate);

    this.clientappointment.appointmentDate = this.shamsiDate;
  }


}
