import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { IonicModule, ModalController } from '@ionic/angular';
import { UserService } from '../../services/user.service';
import { ToastController } from '@ionic/angular/standalone';
@Component({
  selector: 'app-barbershop-detail-modal',
  templateUrl: './barbershop-detail-modal.component.html',
  styleUrls: ['./barbershop-detail-modal.component.scss'],
  standalone: true,   
  imports: [
    IonicModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
  ],
})
export class BarbershopDetailModalComponent  implements OnInit {

  @Input() id!: number;
  @Input() name!: string;
  @Input() phone!: string;
  @Input() address!: string;
  @Input() description!: string;
  @Input() iD_Barbershop!: string;



  

  constructor(private modalCtrl: ModalController,
    private toastController: ToastController,
    private userservice: UserService) { }

  dismiss() {
    this.modalCtrl.dismiss();
  }

  ngOnInit() { }

  async presentToast(position: 'top' | 'middle' | 'bottom') {
    const toast = await this.toastController.create({
      message: 'درخواست شما برای آرایشگاه ارسال شد',
      duration: 1500,
      position: position,
      color: 'success',
      cssClass: 'toast-rtl'
    });

    await toast.present();
  }

  sendRequest() {
    console.log("iD_Barbershop: ", this.iD_Barbershop);
    this.userservice.sendRequest(this.iD_Barbershop).subscribe((data: any) => {
      console.log("response from sendRequest: ", data);
      if (data.message == "You are either verified or rejected you can't continue.") {
      console.log("xxxxxxxxxxxxxxxxx ");
        this.presentToast('top');
        this.dismiss()
      }

    })
  }

}
