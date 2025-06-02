import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../services/user.service';
import { IonicModule } from '@ionic/angular';

//import { IonContent, IonHeader, IonTitle, IonToolbar } from '@ionic/angular/standalone';

interface Barber {
  id: string;
  fullName: string;
  imageUrl?: string;
}
@Component({
  selector: 'app-salon-barbers',
  templateUrl: './salon-barbers.page.html',
  styleUrls: ['./salon-barbers.page.scss'],
  standalone: true,
  imports: [IonicModule, CommonModule, FormsModule]
})
export class SalonBarbersPage implements OnInit {

  salonId: string = '';
  barbers: Barber[] = [];
  filteredbarbers : any[] = [];
  loading = true;

  constructor(
    private route: ActivatedRoute,
    private userService: UserService
  ) { }

  ngOnInit() {    
    this.salonId = this.route.snapshot.paramMap.get('id')!;
    this.loadBarbers();
  }

  loadBarbers() {
    this.userService.barbershop_list().subscribe({
      next: (res: any) => {
        // فرض API: { data: { barbers: [...] } }
        this.barbers = res.data;
        console.log("this.barbers: ", this.barbers);
        this.onSearch();
        this.loading = false;
      },
      error: () => {
        this.loading = false;
      }
    });
  }

  //iD_Barbershop
  onSearch() {
    const id = this.salonId.trim().toLowerCase();
    console.log("receive id: ", id);
    if (id) {
      this.filteredbarbers = this.barbers.filter((s: any) =>
        s.iD_Barbershop == id
      );
      console.log("this.filteredbarbers : ", this.filteredbarbers[0].barbers)

    }
    //else {
    //  this.filteredSalons = [...this.salons];
    //}
  }

}
