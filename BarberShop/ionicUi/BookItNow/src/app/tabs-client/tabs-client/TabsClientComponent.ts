import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
//import { IonTabs, IonTabBar, IonTabButton, IonIcon, IonLabel } from '@ionic/angular/standalone';
import { RouterModule } from '@angular/router'; 

@Component({
    selector: 'app-tabs-client',
    templateUrl: './tabs-client.component.html',
    styleUrls: ['./tabs-client.component.scss'],
    standalone: true,
  imports: [
        RouterModule,
        IonicModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
    ],
})
export class TabsClientComponent implements OnInit {

    constructor() { }

    ngOnInit() { }

}
