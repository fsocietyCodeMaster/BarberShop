import { Component, OnInit } from '@angular/core';
//import { IonHeader, IonToolbar, IonTitle, IonContent } from '@ionic/angular/standalone';
import { ExploreContainerComponent } from '../../explore-container/explore-container.component';
import { IonicModule } from '@ionic/angular';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AlertController } from '@ionic/angular/standalone';


@Component({
  selector: 'app-tab5',
  templateUrl: './tab5.component.html',
  styleUrls: ['./tab5.component.scss'],
  standalone: true,
  imports: [IonicModule, ExploreContainerComponent, CommonModule,
    FormsModule,
    ReactiveFormsModule]
})
export class Tab5Component implements OnInit {

  morningTimes: string[] = [];
  afternoonTimes: string[] = [];
  selectedTime: string | null = null;

  constructor(private alertController: AlertController) {
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

  ngOnInit() { }

}
