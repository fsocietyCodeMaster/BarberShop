import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IonicModule, ModalController } from '@ionic/angular';
import { UserService } from '../../services/user.service';
import { BarbershopDetailModalComponent } from '../../shared_components/barbershop-detail-modal/barbershop-detail-modal.component';
import { count } from 'rxjs';

@Component({
  selector: 'app-choice-barbershop',
  templateUrl: './choice-barbershop.component.html',
  styleUrls: ['./choice-barbershop.component.scss'],
  standalone: true,
  imports: [
    IonicModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
  ],
})
export class ChoiceBarbershopComponent implements OnInit {


  constructor(private fb: FormBuilder,
    private modalCtrl: ModalController,
    private router: Router,
    private http: HttpClient,
    private userservice: UserService
  ) {
    this.choiceBarbershopForm = fb.group({
      'barbershopId': ''

    })
  }

  choiceBarbershopForm: any;

  barbershops_list: any[] = [];

  selectedID_Barbershop: any;

  userInfo: any;

  //barbershops_list = [
  //  { iD_Barbershop: 1, name: 'آرایشگاه الف' },
  //  { iD_Barbershop: 2, name: 'آرایشگاه ب' },
  //  // ... لیست واقعی شما
  //];


  ngOnInit() {

    this.userservice.getUserInfo().subscribe((data: any) => {
      console.log("getUserInfo : ", data.data.id);
      this.userInfo = data.data.id;

    })



    this.userservice.barbershop_list().subscribe((data: any) => {
      console.log("barbershop_list : ", data.data);
      this.barbershops_list = data.data;
    })
  }


  async openModal(barbershop: any) {

    let counter = 0

    console.log('opening modal for', barbershop);

    this.selectedID_Barbershop = barbershop.iD_Barbershop;

    const sendRequestBefore = this.barbershops_list.filter(item => item.iD_Barbershop == this.selectedID_Barbershop);

    console.log("sendRequestBefore: ", sendRequestBefore);
    console.log("barbers: ", sendRequestBefore[0].barbers);
    console.log("barbers.length: ", sendRequestBefore[0].barbers.length);

    if (sendRequestBefore[0].barbers.length > 0) {
      sendRequestBefore[0].barbers.forEach(async (barber: any) => {
        console.log('this.userInfo in for loop ', this.userInfo);
        console.log('barber.id in for loop ', barber.id);

        if (barber.id == this.userInfo) {
          console.log('barber.id == this.userInfo.id');

          counter += 1;
        }
      })
      if (counter > 0) {
        console.log('counter: ', counter);
        this.router.navigate(['/tabs-barber']);
      }
      else {
        console.log('barber.id !== this.userInfo.id');

        const modal = await this.modalCtrl.create({
          component: BarbershopDetailModalComponent,
          componentProps: {
            id: barbershop.iD_Barbershop,
            name: barbershop.name,
            address: barbershop.address,
            description: barbershop.description,
            ownerId: barbershop.ownerId,
            phone: barbershop.phone,
            iD_Barbershop: barbershop.iD_Barbershop,
          }
        });
        await modal.present();
      }

      //})
    } else {
      console.log("there is not sendRequestBefore[0].barbers");
      const modal = await this.modalCtrl.create({
        component: BarbershopDetailModalComponent,
        componentProps: {
          id: barbershop.iD_Barbershop,
          name: barbershop.name,
          address: barbershop.address,
          description: barbershop.description,
          ownerId: barbershop.ownerId,
          phone: barbershop.phone,
          iD_Barbershop: barbershop.iD_Barbershop,
        }
      });
      await modal.present();
    }

  }



  onBarbershopSelect(barbershop: any) {
    console.log('آرایشگاه انتخاب‌شده:', barbershop);
  }



  onSubmit() {
    if (this.choiceBarbershopForm.valid) {
      console.log('فرم ارسال شد:', this.choiceBarbershopForm.value);
      localStorage.setItem('Role', 'barber');

      this.router.navigate(['/tabs-barber']);


    }
  }

}
