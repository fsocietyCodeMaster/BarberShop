import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IonicModule } from '@ionic/angular';
import { UserService } from '../../services/user.service';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

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
    private router: Router) {

  }

  barbers_list: any[] = [];

  barbershopId: any; 


  ngOnInit() {
    this.userservice.getUserInfo().subscribe((data: any) => {
      this.barbershopId = data.data['id'];
      console.log('this.barbershopId: ', this.barbershopId )
    })
  }

  showTable = false;

 

  toggleTable() {
    this.showTable = !this.showTable;
  }

  confirm(barber: any) {
    console.log('تأیید شد:', barber.fullName);
  }

  reject(barber: any) {
    console.log('رد شد:', barber.fullName);
  }

  barberList() {
    this.userservice.Getbarberbybarbershop().subscribe((data: any) => {
      console.log("data is: ", data);
      //this.barbers_list = data.data['barbers'];
      this.barbers_list = data.data;
      console.log("barbers_listis: ", this.barbers_list, typeof (this.barbers_list));
      this.toggleTable();
    })
  }
  
}
