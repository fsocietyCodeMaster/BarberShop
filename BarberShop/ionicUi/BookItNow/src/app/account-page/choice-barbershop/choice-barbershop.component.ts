import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IonicModule } from '@ionic/angular';
import { UserService } from '../../services/user.service';

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


  ngOnInit() {
    this.userservice.barbershop_list().subscribe((data: any) => {
      console.log("data : ", data.data);
      this.barbershops_list = data.data;
    })
  }

  onSubmit() {
    if (this.choiceBarbershopForm.valid) {
      console.log('فرم ارسال شد:', this.choiceBarbershopForm.value);
      localStorage.setItem('Role', 'barber');

      this.router.navigate(['/tabs-barber']);


    }   
  }

}
