import { Component } from '@angular/core';
import { IonHeader, IonToolbar, IonTitle, IonContent, AlertController } from '@ionic/angular/standalone';
import { ExploreContainerComponent } from '../explore-container/explore-container.component';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { UserService } from '../services/user.service';
import { RouterModule } from '@angular/router';


interface Salon {
  id: string;
  name: string;
  address: string;
  iD_Barbershop: string
}

@Component({
  selector: 'app-tab2',
  templateUrl: 'tab2.page.html',
  styleUrls: ['tab2.page.scss'],
  imports: [IonicModule, ExploreContainerComponent, CommonModule,
    FormsModule, RouterModule,
    ReactiveFormsModule]
})
export class Tab2Page {

  morningTimes: string[] = [];
  afternoonTimes: string[] = [];
  selectedTime: string | null = null;

  salons: Salon[] = [];
  filteredSalons: Salon[] = [];
  searchTerm = '';

  constructor(private alertController: AlertController,
    private userservice: UserService,) {
    this.morningTimes = this.generateTimes(9 * 60, 13 * 60 + 30);
    this.afternoonTimes = this.generateTimes(16 * 60, 20 * 60);
  }

  generateTimes(start: number, end: number): string[] {
    const times = [];
    for (let mins = start; mins <= end; mins += 15) {
      const h = Math.floor(mins / 60).toString().padStart(2, '0');
      const m = (mins % 60).toString().padStart(2, '0');
      times.push(`${h}:${m}`);
    }
    return times;
  }

  selectTime2(time: string) {
    this.selectedTime = this.selectedTime === time ? null : time;
  }

  async selectTime(time: string) {
    const alert = await this.alertController.create({
      header: 'تایید نوبت',
      message: `آیا از نوبت "${time}" اطمینان دارید؟`,
      buttons: [
        {
          text: 'نوبت مجدد',
          role: 'cancel',
          handler: () => {
            this.selectedTime = null;
          }
        },
        {
          text: 'بله',
          handler: () => {
            this.selectedTime = time;
          }
        }
      ]
    });

    await alert.present();
  }

  ngOnInit() {
    this.loadSalons();
  }

  loadSalons() {
    this.userservice.barbershop_list().subscribe((res: any) => {      
      console.log("res.data: ", res.data)
      this.salons = res.data;
      this.filteredSalons = [...this.salons];
    });
  }

  onSearch(event: any) {
    const term = this.searchTerm.trim().toLowerCase();
    if (term) {
      this.filteredSalons = this.salons.filter(s =>
        s.name.toLowerCase().includes(term)
      );
    } else {
      this.filteredSalons = [...this.salons];
    }
  }


}
