import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
//import { IonContent, IonHeader, IonItem, IonTitle, IonToolbar, IonLabel } from '@ionic/angular/standalone';

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


  userForm!: FormGroup;
  user_type = [
    { id: '1', name: 'صاحب آرایشگاه' },
    { id: '2', name: 'مشتری' },
    { id: '3', name: 'آرایشگر' },
  ];

  constructor(private fb: FormBuilder) { }

  ngOnInit() {
    this.userForm = this.fb.group({
      fullName: ['', [Validators.required, Validators.maxLength(60)]],
      bio: [''],
      imageUrl: [''],
      startTime: [''],
      endTime: [''],
      isActive: [false],
      barberShopId: ['']
    });
  }

  onSubmit() {
    if (this.userForm.valid) {
      console.log('فرم ارسال شد:', this.userForm.value);
    }
  }
}


