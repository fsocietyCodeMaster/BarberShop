import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IonicModule } from '@ionic/angular';
import { UserService } from '../../services/user.service';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToastController } from '@ionic/angular/standalone';

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


  toggleTable() {
    this.showTable = !this.showTable;
  }

  confirm(barber: any) {

    console.log('barberId in confirm methide:', barber);


    const data = {
      "userId": barber.id,
      "approval": "verify"
    };

    this.userservice.barberApproval(data).subscribe(async (res: any) => {

      console.log('data of approve ini API:', data);

      if (res.isSuccess) {
        const t = await this.toastCtrl.create({ message: `${barber.fullName} با موفقیت تایید شد `, duration: 2000, color: 'success' });
        await t.present();
      } else {
        const t = await this.toastCtrl.create({ message: 'خطا در بروزرسانی', duration: 2000, color: 'danger' });
        await t.present();
      }
    });

  }

  reject(barber: any) {

    console.log('barberId in rejct methide:', barber);

    const data = {
      "userId": barber.id,
      "approval": "reject"
    };

    this.userservice.barberApproval(data).subscribe(async(res: any) => {

      console.log('data of reject in API:', data);

      if (res.isSuccess) {
        const t = await this.toastCtrl.create({ message: `${barber.fullName} غیر فعال `, duration: 2000, color: 'success' });
        await t.present();
      } else {
        const t = await this.toastCtrl.create({ message: 'خطا در بروزرسانی', duration: 2000, color: 'danger' });
        await t.present();
      }

    });

  }

  barberList() {
    this.userservice.Getbarberbybarbershop().subscribe((data: any) => {

      console.log("data is: ", data);

      //this.barbers_list = data.data['barbers'];

      this.barbers_list = data.data;

      console.log("barbers_listis: ", this.barbers_list, typeof (this.barbers_list));

      //this.barbers_list.filter((barber) => barber.status == 1);

      this.toggleTable();
    })
  }

}
