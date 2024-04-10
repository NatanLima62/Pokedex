import {Component, Input} from '@angular/core';
import {Router} from "@angular/router";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'Pokedex';
  @Input() toggleSignal: boolean = false;
  showNavbar: boolean = false;

  constructor(private router: Router) {
    this.router.events
      .subscribe(() =>{
        this.showNavbar = this.router.url !== '/auth';
      });
  }
}
