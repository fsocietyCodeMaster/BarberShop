import { Component, OnInit } from '@angular/core';
import { IonHeader, IonToolbar, IonTitle, IonContent } from '@ionic/angular/standalone';
import { ExploreContainerComponent } from '../../explore-container/explore-container.component';


@Component({
  selector: 'app-tab5',
  templateUrl: './tab5.component.html',
  styleUrls: ['./tab5.component.scss'],
  standalone: true,
  imports: [IonHeader, IonToolbar, IonTitle, IonContent, ExploreContainerComponent]
})
export class Tab5Component  implements OnInit {

  constructor() { }

  ngOnInit() {}

}
