import { Component } from '@angular/core';
//import { IonHeader, IonToolbar, IonTitle, IonContent, IonItem, IonIcon, IonLabel } from '@ionic/angular/standalone';
import { ExploreContainerComponent } from '../explore-container/explore-container.component';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { IonicModule } from '@ionic/angular';
import { UserService } from '../services/user.service';


@Component({
  selector: 'app-tab1',
  templateUrl: 'tab1.page.html',
  styleUrls: ['tab1.page.scss'],
  imports: [
    IonicModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    ExploreContainerComponent
  ],
})
export class Tab1Page {
  constructor(private fb: FormBuilder,    
    private userservice: UserService,
    private router: Router,
    private http: HttpClient) {
    this.barbershopForm = fb.group({
      'Name': '',
      'Address': ['', Validators.required],
      'Description': ['', Validators.required],
      'Phone': ['', [Validators.required, Validators.minLength(11), Validators.maxLength(11)]],
      //'CreatedAt': [''],
      //'IsActive': [true],

    })
  }

  barbershopForm!: FormGroup;


  barber_shop = {
    "name": "string",
    "address": "string",
    "phone": "string",
    "description": "string"
  };

  onSubmit() {
    console.log('barbershopForm: ', this.barbershopForm.value);

    console.log("i am in barber form");
    this.userservice.create_barbershop(this.barbershopForm.value).subscribe((data: any) => {
      console.log("data of register: ", data);
    })
  }

  allowOnlyNumbers(event: any) {
    const input = event.target as HTMLInputElement;
    input.value = input.value.replace(/[^0-9]/g, '');
  }



}
