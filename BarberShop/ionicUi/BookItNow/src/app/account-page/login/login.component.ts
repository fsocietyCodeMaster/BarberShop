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
export class LoginComponent implements OnInit {

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
          console.log("data of login: ", data);
          console.log("data of isSuccess: ", data.isSuccess);
          console.log("userRole[0]: ", data.data['userRole'][0]);
          console.log("data of message: ", data.message);
          if (data.isSuccess == true) {
            localStorage.setItem('token', data.message);
            //localStorage.setItem('token', JSON.stringify(data.message));
            if (data.data['userRole'][0] == "barbershop") {
              if (data.data['barberShopStatus'] == true) {
                this.router.navigate(['/tabs-barbershop']);
              } else {
                this.router.navigate(['/createBarbershop']);
              }
            } else if (data.data['userRole'][0] == "user") {
              localStorage.setItem('Role', 'user');

              this.router.navigate(['/tabs']);
            } else if (data.data['userRole'][0] == "barber") {
              this.router.navigate(['/choiceBarbershop']);
            }
            document.cookie = `token=${data.message}; path=/; max-age=3600; secure; samesite=strict`;

          }
        });

    }
  }

}
