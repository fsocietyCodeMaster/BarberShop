import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IonicModule } from '@ionic/angular';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-tab8',
  templateUrl: './tab8.component.html',
  styleUrls: ['./tab8.component.scss'],
  standalone: true,
  imports: [
    IonicModule,
  ],
})
export class Tab8Component  implements OnInit {

  constructor(private userservice: UserService,
    private router: Router) {
   
  }

  barbers_list = [];

  ngOnInit() { }

  barberList() {
    this.userservice.barber_list('ae328c03-68ad-4dc8-d8dd-08dd987887c4').subscribe((data: any) => {
      console.log("data is: ", data);
      this.barbers_list = data.data['barbers'];
      console.log("barbers_listis: ", this.barbers_list, typeof (this.barbers_list));

    })
  }

}
