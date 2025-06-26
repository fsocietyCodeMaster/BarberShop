import { Component, OnInit } from '@angular/core';
import { IonicModule } from '@ionic/angular';
import { IonRouterOutlet } from '@ionic/angular/standalone';
import { UserService } from '../../services/user.service';
import { Router } from '@angular/router';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-tab9',
  templateUrl: './tab9.component.html',
  styleUrls: ['./tab9.component.scss'],
  standalone: true,
  imports: [
    IonicModule,
    IonRouterOutlet,
    RouterModule            
  ],
})
export class Tab9Component implements OnInit {

  constructor(private userservice: UserService,
    private router: Router) { }

  ngOnInit() { }



  logout() {

    localStorage.removeItem('Role');

    localStorage.removeItem('token');

    this.router.navigate(['/login']);


  }



}
