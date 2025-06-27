import { Component, OnInit } from '@angular/core';
import { IonHeader, IonToolbar, IonTitle, IonContent } from '@ionic/angular/standalone';
import { ExploreContainerComponent } from '../../explore-container/explore-container.component';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AlertController, IonicModule, ToastController } from '@ionic/angular';
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
    private alertController: AlertController,
    private userservice: UserService,
    private toastCtrl: ToastController,
    private router: Router,) {
    this.workScheduleForm = fb.group({
      startTimeMorning: ['1970-01-01T09:00:00'],
      endTimeMorning: ['1970-01-01T13:00:00'],
      startTimeEvening: ['1970-01-01T15:00:00'],
      endTimeEvening: ['1970-01-01T21:00:00'],
      scopeTime: ['1970-01-01T09:00:00'],
      saturdayWork: [false],
      sundayWork: [false],
      mondayWork: [false],
      tuesdayWork: [false],
      wednesdayWork: [false],
      thursdayWork: [false],
      fridayWork: [false]
    })

    this.workScheduleEditForm = fb.group({
      startTimeMorning: [''],
      endTimeMorning: [''],
      startTimeEvening: [''],
      endTimeEvening: [''],
      scopeTime: [''],
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

  workScheduleEditForm!: FormGroup;

  showScheduleForm = false;

  showEditScheduleForm = false;

  hasWorkSchedule = false;

  toggleWorkSchedule: 'تنظیم برنامه کاری' | 'ویرایش برنامه کاری' = 'تنظیم برنامه کاری';

  private toTimeSpan(iso: string): string {
    return iso.split('T')[1];
  }


  private toIsoDateTime(timespan: string): string {
    return `1970-01-01T${timespan}`;
  }



  ngOnInit() {
    this.getWoekSchedule();
  }

  getWoekSchedule1() {
    this.userservice.getWorkSchedule().subscribe((data: any) => {
      console.log("data of getWoekSchedule: ", data.data);
      if (data.isSuccess) {
        this.hasWorkSchedule = true;
        this.hasWorkSchedule = data;
        this.toggleWorkSchedule = 'ویرایش برنامه کاری';
        this.workScheduleEditForm.value['startTimeMorning'] = data.data.startTimeMorning;
        this.workScheduleEditForm.value['endTimeMorning'] = data.data.endTimeMorning;
        this.workScheduleEditForm.value['startTimeEvening'] = data.data.startTimeEvening;
        this.workScheduleEditForm.value['endTimeEvening'] = data.data.endTimeEvening;
        this.workScheduleEditForm.value['scopeTime'] = data.data.scopeTime;
        this.workScheduleEditForm.value['saturdayWork'] = data.data.saturdayWork;
        this.workScheduleEditForm.value['sundayWork'] = data.data.sundayWork;
        this.workScheduleEditForm.value['mondayWork'] = data.data.mondayWork;
        this.workScheduleEditForm.value['tuesdayWork'] = data.data.tuesdayWork;
        this.workScheduleEditForm.value['thursdayWork'] = data.data.thursdayWork;
        this.workScheduleEditForm.value['wednesdayWork'] = data.data.wednesdayWork;
        this.workScheduleEditForm.value['fridayWork'] = data.data.fridayWork;

        console.log("workScheduleEditForm: ", this.workScheduleEditForm.value);

      }
    })
  }

  getWoekSchedule() {
    this.userservice.getWorkSchedule().subscribe((res: any) => {

      console.log("getWoekSchedule:", res);

      if (!res.isSuccess) return;

      const data = res.data;
     
      this.hasWorkSchedule = true;

      this.toggleWorkSchedule = 'ویرایش برنامه کاری';

      this.workScheduleEditForm.patchValue({
        startTimeMorning: this.toIsoDateTime(data.startTimeMorning),
        endTimeMorning: this.toIsoDateTime(data.endTimeMorning),
        startTimeEvening: this.toIsoDateTime(data.startTimeEvening),
        endTimeEvening: this.toIsoDateTime(data.endTimeEvening),
        //scopeTime: Number(data.scopeTime.split(':')[2]), // از "00:15:00" → 15
        scopeTime: Number(data.scopeTime.split(':')[1]),

        saturdayWork: data.saturdayWork,
        sundayWork: data.sundayWork,
        mondayWork: data.mondayWork,
        tuesdayWork: data.tuesdayWork,
        wednesdayWork: data.wednesdayWork,
        thursdayWork: data.thursdayWork,
        fridayWork: data.fridayWork
      });

      console.log("فرم ویرایش مقداردهی شد:", this.workScheduleEditForm.value);
    });
  }


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
            this.showScheduleForm = false;
            this.hasWorkSchedule = true;
            this.toggleWorkSchedule = 'ویرایش برنامه کاری';

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


  submitEditSchedule() {
    if (this.workScheduleEditForm.invalid) return;

    console.log("workScheduleEditForm before: ", this.workScheduleEditForm);

    const f = this.workScheduleEditForm.value;
    const payload = {
      startTimeMorning: f.startTimeMorning.split('T')[1],
      endTimeMorning: f.endTimeMorning.split('T')[1],
      startTimeEvening: f.startTimeEvening.split('T')[1],
      endTimeEvening: f.endTimeEvening.split('T')[1],
      scopeTime: `00:${String(f.scopeTime).padStart(2, '0')}:00`,
      saturdayWork: f.saturdayWork,
      sundayWork: f.sundayWork,
      mondayWork: f.mondayWork,
      tuesdayWork: f.tuesdayWork,
      wednesdayWork: f.wednesdayWork,
      thursdayWork: f.thursdayWork,
      fridayWork: f.fridayWork
    };
    console.log("workScheduleEditForm after: ", this.workScheduleEditForm);


    this.userservice.updateWorkSchedule(payload).subscribe(async (res: any) => {
      console.log("data in updateWorkSchedule: ", res);
      if (res.isSuccess) {
        const t = await this.toastCtrl.create({ message: 'بروزرسانی شد 🎉', duration: 2000, color: 'success' });
        await t.present();
        this.showEditScheduleForm = false;
      } else {
        const t = await this.toastCtrl.create({ message: 'خطا در بروزرسانی', duration: 2000, color: 'danger' });
        await t.present();
      }
    }, err => console.error(err))
  }


  workSchedule = {
    startTimeMorning: '',
    endTimeMorning: '',
    startTimeEvening: '',
    endTimeEvening: '',
    scopeTime: null,
    selectedDays: [] as string[],
  };

  workScheduleEdit = {
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

  isDaySelectedEdit(day: string): boolean {
    return this.workScheduleEdit.selectedDays.includes(day);
  }



  async showForm(worrkScheduleStatus: any) {

    console.log("worrkScheduleStatus: ", worrkScheduleStatus)

    this.getWoekSchedule();

    if (this.hasWorkSchedule) {
      const alert = await this.alertController.create({
        header: 'تغییر برنامه کاری',
        message: 'شما از قبل برنامه کاری را تنظیم کرده‌اید. آیا می‌خواهید آن را تغییر دهید؟',
        buttons: [
          {
            text: 'خیر',
            role: 'cancel',
            handler: () => {
            }
          },
          {
            text: 'بله',
            handler: () => {
              this.showEditScheduleForm = true;
            }
          }
        ]
      });
      await alert.present();
    } else {
      this.showScheduleForm = true;
      this.showEditScheduleForm = false;

    }
  }


  goToProfileEdit() {
    this.showScheduleForm = false;
    this.showEditScheduleForm = false;
    //this.router.navigate(['/profile-edit']);
  }


}
