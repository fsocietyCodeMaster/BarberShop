import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { UserService } from '../../services/user.service';
import { HttpClient } from '@angular/common/http';
//import { IonContent, IonHeader, IonItem, IonTitle, IonToolbar, IonLabel } from '@ionic/angular/standalone';
import { Router } from "@angular/router";


@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    IonicModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],


})
export class RegisterComponent implements OnInit {

  registerForm!: FormGroup;

  user_type = [
    { id: '1', name: 'صاحب آرایشگاه', role: 'barbershop' },
    { id: '2', name: 'مشتری', role: 'user' },
    { id: '3', name: 'آرایشگر', role: 'barber' },
  ];

  constructor(private fb: FormBuilder,
    private router: Router,
    private http: HttpClient,
    private userservice: UserService
  ) {
    this.registerForm = fb.group({
      'UserName': '',
      'Password': ['', Validators.required],
      'FullName': ['', Validators.required],
      'PhoneNumber': ['', [Validators.required, Validators.minLength(11), Validators.maxLength(11)]],
      //'ImageUrl': ['', Validators.required],
      'Role': ['', Validators.required],

    })
  }

  barbershop: any;

  ngOnInit() {

    this.barbershop = {
      "name": "string",
      "address": "string",
      "phone": "string",
      "description": "string"
    }

  }

  allowOnlyNumbers(event: any) {
    const input = event.target as HTMLInputElement;
    input.value = input.value.replace(/[^0-9]/g, '');
  }

  onSubmit() {
    if (this.registerForm.valid) {
      console.log('فرم ارسال شد:', this.registerForm.value);
      this.userservice.register(this.registerForm.value)
        .subscribe((data: any) => {
          console.log("data of register: ", data);
          console.log("this.registerForm.value.Role: ", this.registerForm.value.Role);
          localStorage.setItem('Role', this.registerForm.value.Role);
          this.router.navigate(['/login']);
        });

    }
    if (this.registerForm.invalid) {
      Object.keys(this.registerForm.controls).forEach(field => {
        const control = this.registerForm.get(field);
        control?.markAsTouched({ onlySelf: true });
      });
      return;
    }
  }
}


