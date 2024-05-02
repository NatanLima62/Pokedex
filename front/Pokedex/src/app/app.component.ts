import {Component, Input} from '@angular/core';
import {Router} from "@angular/router";
import {AuthService} from "./Modules/Auth/Services/auth.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'Pokedex';
  @Input() toggleSignal: boolean = false;
  showNavbar: boolean = false;

  constructor(
    private router: Router,
    public authService: AuthService
  ) {
    this.router.events
      .subscribe(() =>{
        this.showNavbar = !this.router.url.startsWith('/auth');
      });

    if (this.router.url !== '/'){
      if (this.authService.verifyToken()){
        this.router.navigate(['/pokemons']).then();
      }
    }
  }
}
