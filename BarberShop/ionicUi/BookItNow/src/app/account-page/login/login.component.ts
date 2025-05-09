import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { UserService } from '../../services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  imports: [
    IonicModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
  ],
})
export class LoginComponent  implements OnInit {

  loginForm!: FormGroup;

  user_type = [
    { id: '1', name: 'صاحب آرایشگاه', role: 'barbershop' },
    { id: '2', name: 'مشتری', role: 'user' },
    { id: '3', name: 'آرایشگر', role: 'barber' },
  ];

  constructor(private fb: FormBuilder,
    private userservice: UserService,
    private router: Router,) {
    this.loginForm = fb.group({
      'UserName': '',
      'Password': ''
    })
  }

  ngOnInit() {
    //this.userForm = this.fb.group({
    //  fullName: ['', [Validators.required, Validators.maxLength(60)]],
    //  bio: [''],
    //  imageUrl: [''],
    //  startTime: [''],
    //  endTime: [''],
    //  isActive: [false],
    //  barberShopId: ['']
    //});
  }

  onSubmit() {
    if (this.loginForm.valid) {
      console.log('فرم صحیح است:', this.loginForm.value);
      this.userservice.login(this.loginForm.value)
        .subscribe((data: any) => {
          console.log("data of register: ", data);
          this.router.navigate(['/tabs/tab1']);
        });

    }
  }

}
