import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IonicModule } from '@ionic/angular';
import { UserService } from '../../services/user.service';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToastController } from '@ionic/angular/standalone';
import { HttpErrorResponse } from '@angular/common/http';
//import { ToastController } from '@ionic/angular';
@Component({
  selector: 'app-tab8',
  templateUrl: './tab8.component.html',
  styleUrls: ['./tab8.component.scss'],
  standalone: true,
  imports: [
    IonicModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
  ],
})
export class Tab8Component implements OnInit {

  constructor(private userservice: UserService,
    private toastCtrl: ToastController,
    private router: Router) {

  }

  barbers_list: any[] = [];

  barbershopId: any;


  ngOnInit() {
    this.userservice.getUserInfo().subscribe((data: any) => {
      this.barbershopId = data.data['id'];
      console.log('this.barbershopId: ', this.barbershopId)
    })
  }

  showTable = false;

  isbarberListClicked = false;


  toggleTable() {
    this.showTable = !this.showTable;
  }

  confirm(barber: any) {

    console.log('barberId in confirm methide:', barber);

    const data = {
      "userId": barber.id,
      "approval": "verify",
      "barberShopId": this.barbershopId
    };

    this.userservice.barberApproval(data).subscribe(async (res: any) => {

      console.log('data of approve ini API:', data);

      if (res.isSuccess) {
        const t = await this.toastCtrl.create({ message: `${barber.fullName} با موفقیت تایید شد `, duration: 2000, color: 'success' });
        await t.present();
        this.barberList('false');
      } else {
        const t = await this.toastCtrl.create({ message: 'خطا در بروزرسانی', duration: 2000, color: 'danger' });
        await t.present();
      }
    });

  }

  async showToast(message: string) {
    const toast = await this.toastCtrl.create({
      message,
      duration: 2000,
      position: 'bottom'
    });
    toast.present();
  }

  reject(barber: any) {

    console.log('barberId in rejct methide:', barber);

    const data = {
      "userId": barber.id,
      "approval": "reject"
    };

    this.userservice.barberApproval(data).subscribe(async (res: any) => {

      console.log('data of reject in API:', data);

      if (res.isSuccess) {
        const t = await this.toastCtrl.create({ message: `${barber.fullName} رد شد`, duration: 2000, color: 'success' });
        await t.present();
        this.barberList('false');

      } else {
        const t = await this.toastCtrl.create({ message: 'خطا در بروزرسانی', duration: 2000, color: 'danger' });
        await t.present();

      }

    });

  }

  barberList(openToggel: string) {
    console.log("openToggel: ", openToggel);

    this.userservice.Getbarberbybarbershop()
      .subscribe(
        (response: any) => {

          console.log('data is: ', response);
          this.barbers_list = response.data;
          console.log('barbers_list is: ', this.barbers_list);
          //this.toggleTable();
          if (openToggel == 'true') {
            this.isbarberListClicked = !this.isbarberListClicked;

            this.showTable = !this.showTable;
          }

        },
        (error: HttpErrorResponse) => {

          console.error('HTTP Error:', error);


          if (error.error && error.error.message === 'No pending barbers found.') {

            this.showToast('هیچ آرایشگری در انتظار یافت نشد.');

            this.barbers_list = [];
            if (this.barbers_list.length == 0) {
              console.error('this.barbers_list.length: ', this.barbers_list.length);
              this.showTable = false;
              this.isbarberListClicked = false;
            }
          } else {

            this.showToast('خطایی رخ داد. لطفاً دوباره تلاش کنید.');
          }
        }
      );
  }



  barberList1() {
    this.userservice.Getbarberbybarbershop().subscribe((data: any) => {

      console.log("data is: ", data);

      //this.barbers_list = data.data['barbers'];

      this.barbers_list = data.data;

      console.log("barbers_listis: ", this.barbers_list, typeof (this.barbers_list));

      //this.barbers_list.filter((barber) => barber.status == 1);

      this.toggleTable(),

        (error: HttpErrorResponse) => {

          console.error('HTTP Error:', error);

          if (error.error && error.error.message === 'No pending barbers found.') {

            this.showToast('هیچ آرایشگری در انتظار یافت نشد.');

            this.barbers_list = [];
          } else {

            this.showToast('خطایی رخ داد. لطفاً دوباره تلاش کنید.');
          }
        }
    })
  }

}
