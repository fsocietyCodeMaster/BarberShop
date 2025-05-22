import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IonicModule, ModalController } from '@ionic/angular';
import { UserService } from '../../services/user.service';
import { BarbershopDetailModalComponent } from '../../shared_components/barbershop-detail-modal/barbershop-detail-modal.component';

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

  //barbershops_list = [
  //  { iD_Barbershop: 1, name: 'آرایشگاه الف' },
  //  { iD_Barbershop: 2, name: 'آرایشگاه ب' },
  //  // ... لیست واقعی شما
  //];


  ngOnInit() {
    this.userservice.barbershop_list().subscribe((data: any) => {
      console.log("data : ", data.data);
      this.barbershops_list = data.data;
    })
  }

  async openModal1(barbershop: any) {
    console.log(' شد:');

    const modal = await this.modalCtrl.create({
      component: BarbershopDetailModalComponent,
      componentProps: {
        id: barbershop.iD_Barbershop,
        name: barbershop.name
      }
    });
    console.log(' شد:');

    await modal.present();
  }

  async openModal(barbershop: any) {
    console.log('opening modal for', barbershop);
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



  onSubmit() {
    if (this.choiceBarbershopForm.valid) {
      console.log('فرم ارسال شد:', this.choiceBarbershopForm.value);
      localStorage.setItem('Role', 'barber');

      this.router.navigate(['/tabs-barber']);


    }   
  }

}
