import { Component, OnInit } from '@angular/core';
import { IonHeader, IonToolbar, IonTitle, IonContent } from '@ionic/angular/standalone';
import { ExploreContainerComponent } from '../../explore-container/explore-container.component';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { IonicModule, ToastController } from '@ionic/angular';
import { Router } from '@angular/router';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-tab6',
  templateUrl: './tab6.component.html',
  styleUrls: ['./tab6.component.scss'],
  standalone: true,
  imports: [
    IonicModule,
    ExploreContainerComponent,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
  ]

})
export class Tab6Component implements OnInit {

  constructor(private fb: FormBuilder,
    private userservice: UserService,
    private toastCtrl: ToastController,
    private router: Router,) {
    this.workScheduleForm = fb.group({
      startTimeMorning: ['1970-01-01T09:00:00'],
      endTimeMorning: ['1970-01-01T12:00:00'],
      startTimeEvening: ['1970-01-01T03:00:00'],
      endTimeEvening: ['1970-01-01T09:00:00'],
      scopeTime: ['1970-01-01T09:00:00'],
      saturdayWork: [false],
      sundayWork: [false],
      mondayWork: [false],
      tuesdayWork: [false],
      wednesdayWork: [false],
      thursdayWork: [false],
      fridayWork: [false]
    })
  }

  workScheduleForm!: FormGroup;



  private toTimeSpan(iso: string): string {
    return iso.split('T')[1]; 
  }



  ngOnInit() { }



  submitSchedule() {
    if (this.workScheduleForm.invalid) {
      return;
    }

    if (this.workScheduleForm.valid) {
      const form = this.workScheduleForm.value;
      const payloadBody = {
        "startTimeMorning": this.toTimeSpan(form.startTimeMorning),
        "endTimeMorning": this.toTimeSpan(form.endTimeMorning),
        "startTimeEvening": this.toTimeSpan(form.startTimeEvening),
        "endTimeEvening": this.toTimeSpan(form.endTimeEvening),
        "scopeTime": `00:${String(form.scopeTime).padStart(2, '0')}:00`,
        "saturdayWork": form.saturdayWork,
        "sundayWork": form.sundayWork,
        "mondayWork": form.mondayWork,
        "tuesdayWork": form.tuesdayWork,
        "wednesdayWork": form.wednesdayWork,
        "thursdayWork": form.thursdayWork,
        "fridayWork": form.fridayWork,
      };

      console.log('payloadBody is:', payloadBody);

      this.userservice.setWorkSchedule(payloadBody).subscribe({
        next: async (res: any) => {
          console.log('برنامه با موفقیت ذخیره شد', res);
          if (res.isSuccess) {           
            const toast = await this.toastCtrl.create({
              message: 'با موفقیت ذخیره شد 🎉',
              duration: 2000,
              position: 'top',
              color: 'success'
            });
            await toast.present();
          } else {
            const toast = await this.toastCtrl.create({
              message: 'خطایی رخ داد',
              duration: 2000,
              position: 'top',
              color: 'danger'
            });
            await toast.present();

          }
        },
        
        error: err => {
          console.error('خطا در ذخیره‌سازی', err);
        }
      });
    }
  }


  workSchedule = {
    startTimeMorning: '',
    endTimeMorning: '',
    startTimeEvening: '',
    endTimeEvening: '',
    scopeTime: null,
    selectedDays: [] as string[],
  };

  timeIntervals = [5, 10, 15, 20, 25, 30]; 

  dayToggles = [
    { label: 'شنبه', controlName: 'saturdayWork' },
    { label: 'یکشنبه', controlName: 'sundayWork' },
    { label: 'دوشنبه', controlName: 'mondayWork' },
    { label: 'سه‌شنبه', controlName: 'tuesdayWork' },
    { label: 'چهارشنبه', controlName: 'wednesdayWork' },
    { label: 'پنج‌شنبه', controlName: 'thursdayWork' },
    { label: 'جمعه', controlName: 'fridayWork' },
  ];


  isDaySelected(day: string): boolean {
    return this.workSchedule.selectedDays.includes(day);
  }

}
