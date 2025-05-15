import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormsModule, ReactiveFormsModule, FormBuilder, Validators, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { IonicModule } from '@ionic/angular';
import { ExploreContainerComponent } from '../../explore-container/explore-container.component';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-create-barbershop',
  templateUrl: './create-barbershop.component.html',
  styleUrls: ['./create-barbershop.component.scss'],
  standalone: true,
  imports: [
    IonicModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    ExploreContainerComponent
  ],
})
export class CreateBarbershopComponent  implements OnInit {

  constructor(private fb: FormBuilder,
    private userservice: UserService,
    private router: Router,
    private http: HttpClient) {
    this.barbershopForm = fb.group({
      'Name': '',
      'Address': ['', Validators.required],
      'Description': ['', Validators.required],
      'Phone': ['', [Validators.required, Validators.minLength(11), Validators.maxLength(11)]],
      'ImageUrl': '',
      //'CreatedAt': [''],
      //'IsActive': [true],

    })
  }

  barbershopForm!: FormGroup;

  fileToUpload: File | null = null;


  barber_shop = {
    "name": "string",
    "address": "string",
    "phone": "string",
    "description": "string"
  };

  ngOnInit() {

  }

  onSubmit() {
    console.log('barbershopForm: ', this.barbershopForm.value);
    const formData = new FormData();
    formData.append('Name', this.barbershopForm.get('Name')?.value);
    formData.append('Address', this.barbershopForm.get('Address')?.value);
    formData.append('Description', this.barbershopForm.get('Description')?.value);
    formData.append('Phone', this.barbershopForm.get('Phone')?.value);
    if (this.fileToUpload) {
      console.log('fileToUpload is loaded ...');
      formData.append('ImageUrl', this.fileToUpload, this.fileToUpload.name);
    }
    formData.forEach((value, key) => {
      console.log(`formData key: ${key} , ${key.length}, formData value: ${value}`);
    });

    console.log("i am in barber form");
    this.userservice.create_barbershop(formData).subscribe((data: any) => {
      console.log("data of register: ", data);
      if (data.isSuccess == true) {
        localStorage.setItem('Role', 'barbershop');
        this.router.navigate(['/tabs-barbershop']);
      }
    })
  }

  allowOnlyNumbers(event: any) {
    const input = event.target as HTMLInputElement;
    input.value = input.value.replace(/[^0-9]/g, '');
  }

  onFileChange(event: any) {
    if (event.target.files.length > 0) {
      this.fileToUpload = event.target.files[0];
    }
    console.log('selectedFile ... ', this.fileToUpload);

  }


}
