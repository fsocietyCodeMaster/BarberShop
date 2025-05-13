import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-tabs-barbershop',
  templateUrl: './tabs-barbershop.component.html',
  styleUrls: ['./tabs-barbershop.component.scss'],
  standalone: true,
  imports: [
    RouterModule,
    IonicModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
  ],
})
export class TabsBarbershopComponent  implements OnInit {

  constructor() { }

  ngOnInit() {}

}
