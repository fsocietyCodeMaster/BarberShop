import { Component, OnInit, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { AlertController, IonicModule } from '@ionic/angular';
import { UserService } from '../services/user.service';
import { IDatepickerTheme, NgPersianDatepickerModule, IActiveDate } from 'ng-persian-datepicker';
import { CommonModule } from '@angular/common';
import { ExploreContainerComponent } from '../explore-container/explore-container.component';
import { Jalali } from 'jalali-ts';
import { ToastController } from '@ionic/angular/standalone';

@Component({
  selector: 'app-appointment-reservation',
  templateUrl: './appointment-reservation.component.html',
  styleUrls: ['./appointment-reservation.component.scss'],
  standalone: true,
  imports: [
    NgPersianDatepickerModule,
    IonicModule,
    ExploreContainerComponent,
    CommonModule,
    FormsModule,
    RouterModule,
    ReactiveFormsModule
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class AppointmentReservationComponent implements OnInit {

  disabledWeekDays: number[] = [];

  morningTimes: string[] = [];

  afternoonTimes: string[] = [];

  selectedTime: string | null = null;

  dateValue = new FormControl();

  setAppointmentForm: FormGroup;

  oppoinment_hour = false;

  barberId = '';

  selctDate: any;

  selctGorgianDate: any;

  scopeTime: any;

  bookedTimes: string[] = ['18:30'];



  private schedule: any = null;

  customTheme: Partial<IDatepickerTheme> = {
    selectedBackground: '#D68E3A',
    selectedText: '#FFFFFF'
  };

  constructor(
    private alertController: AlertController,
    private route: ActivatedRoute,
    private toastCtrl: ToastController,
    private userservice: UserService,
    private fb: FormBuilder
  ) {

    this.barberId = this.route.snapshot.paramMap.get('id')!;

    this.setAppointmentForm = this.fb.group({
      "iD_Barber": [''],
      "appointmentDate": [''],
      "startTime": [''],
      "endTime": ['']
    })

  }

  ngOnInit() {

    this.barberId = this.route.snapshot.paramMap.get('id')!;

    console.log("barberId : ", this.barberId);

    this.userservice.showBarberSchedule(this.barberId)
      .subscribe((res: any) => {
        if (res.isSuccess && res.data) {
          console.log("showBarberSchedule: ", res.data);
          this.getDisabledWeekDays(res.data);
       
        }
      });

  }

  async onSelect(event: IActiveDate) {

    const isoString = event.gregorian;


    const jsDate = new Date(isoString);


    const weekday = jsDate.getDay();


    if (this.disabledWeekDays.includes(weekday)) {
      const toast = await this.toastCtrl.create({
        message: 'امکان انتخاب این روز بدلیل عدم فعالیت آرایشگر وجود ندارد.',
        duration: 2000,
        color: 'warning'
      });
      await toast.present();
      this.oppoinment_hour = false;
      return;
    }


    this.selctDate = event.shamsi;
    this.selctGorgianDate = event.gregorian;
    this.dateValue.setValue(this.selctDate);
    this.selectedTime = null;
    this.oppoinment_hour = false;


    console.log("selctGorgianDate: ", this.selctGorgianDate)

    this.userservice.showBarberSchedule(this.barberId)
      .subscribe((res: any) => {
        if (res.isSuccess && res.data) {
          console.log("showBarberSchedule: ", res.data);
          this.getDisabledWeekDays(res.data);
          this.schedule = res.data;
          this.scopeTime = this.schedule.scopeTime;
          this.buildTimeGrids();
          this.oppoinment_hour = true;

          this.getAppointment();

        }
      });
  }

  private buildTimeGrids() {
    // تابع کمکی برای تبدیل HH:mm:ss به تعداد دقیقه از نیمه‌شب
    const toMinutes = (hms: string) => {
      const [h, m] = hms.split(':').map(x => parseInt(x, 10));
      return h * 60 + m;
    };

    const scopeMin = toMinutes(this.schedule.scopeTime);
    this.scopeTime = this.schedule.scopeTime;


    const startM = toMinutes(this.schedule.startTimeMorning);
    const endM = toMinutes(this.schedule.endTimeMorning);
    this.morningTimes = this.generateTimes(startM, endM, scopeMin);


    const startE = toMinutes(this.schedule.startTimeEvening);
    const endE = toMinutes(this.schedule.endTimeEvening);
    this.afternoonTimes = this.generateTimes(startE, endE, scopeMin);
  }

  private generateTimes(start: number, end: number, step: number) {
    const times: string[] = [];
    for (let t = start; t <= end; t += step) {
      const h = Math.floor(t / 60).toString().padStart(2, '0');
      const m = (t % 60).toString().padStart(2, '0');
      times.push(`${h}:${m}`);
    }
    return times;
  }

  private addScopeTimeToStartTime(startTime: string, scopeTime: string): string {
    // 1. تبدیل startTime به ساعت و دقیقه
    const [startHour, startMinute] = startTime.split(':').map(Number);

    // 2. تبدیل scopeTime به ساعت، دقیقه، ثانیه
    const [scopeHour, scopeMinute, scopeSecond] = scopeTime.split(':').map(Number);

    // 3. محاسبه مجموع دقیقه‌ها و ثانیه‌ها
    let totalSeconds = 0;
    totalSeconds += (startHour * 3600) + (startMinute * 60);
    totalSeconds += (scopeHour * 3600) + (scopeMinute * 60) + scopeSecond;

    // 4. تبدیل دوباره به ساعت و دقیقه و ثانیه
    const finalHour = Math.floor(totalSeconds / 3600);
    const finalMinute = Math.floor((totalSeconds % 3600) / 60);
    const finalSecond = totalSeconds % 60;

    // 5. ساخت خروجی فرمت‌شده "HH:mm:ss"
    const formatted = [
      finalHour.toString().padStart(2, '0'),
      finalMinute.toString().padStart(2, '0'),
      finalSecond.toString().padStart(2, '0')
    ].join(':');

    console.log("formatted: ", formatted);


    return formatted;
  }

  async selectTime(time: string) {

    this.setAppointmentForm.patchValue({
      'iD_Barber': this.barberId,
      'appointmentDate': this.selctGorgianDate,
      'startTime': time,
      'endTime': this.addScopeTimeToStartTime(time, this.scopeTime)
    });


    console.log("setAppointmentForm: ", this.setAppointmentForm.value);

    const alert = await this.alertController.create({
      header: 'تایید نوبت',
      message: `آیا از نوبت ${this.selctDate} در ساعت ${time}اطمینان دارید؟`,
      cssClass: 'alert-html',
      buttons: [
        {
          text: 'خیر',
          role: 'cancel',
          handler: () => {
            this.selectedTime = null;
          }
        },
        {
          text: 'بله',
          handler: () => {
            this.selectedTime = time;
            this.userservice.setAppointment(this.setAppointmentForm.value).subscribe({
              next: async (res: any) => {
                console.log("res: ", res);
                if (res.isSuccess) {
                  const toast = await this.toastCtrl.create({
                    message: 'نوبت شما با موفقیت ذخیره شد 🎉',
                    duration: 2000,
                    position: 'top',
                    color: 'success'
                  });
                  await toast.present();
                  this.getAppointment();
                }
              }
            })
          }
        }
      ]
    });

    await alert.present();
  }

  getAppointment() {

    this.bookedTimes = [];


    this.userservice.getAppointment(this.barberId, this.selctGorgianDate).subscribe((res: any) => {
      console.log("getAppointment: ", res);
      this.bookedTimes = res.data.map((item: any) => item.startTime.slice(0, 5));
      console.log("bookedTimes: ", this.bookedTimes);
      ;
    })
  }

  private getDisabledWeekDays(schedule: BarberSchedule): void {
    const mapping: { [K in keyof BarberSchedule]?: number } = {
      sundayWork: 0,
      mondayWork: 1,
      tuesdayWork: 2,
      wednesdayWork: 3,
      thursdayWork: 4,
      fridayWork: 5,
      saturdayWork: 6,
    };

    //const disabled: number[] = [];

    for (const key in mapping) {
      // @ts-ignore
      if (mapping.hasOwnProperty(key) && schedule[key] === false) {
        // @ts-ignore
        this.disabledWeekDays.push(mapping[key]);
      }
    }
    console.log("disabled: ", this.disabledWeekDays)

    //return disabled;
  }

}


interface BarberSchedule {
  sundayWork: boolean;
  mondayWork: boolean;
  tuesdayWork: boolean;
  wednesdayWork: boolean;
  thursdayWork: boolean;
  fridayWork: boolean;
  saturdayWork: boolean;
}
