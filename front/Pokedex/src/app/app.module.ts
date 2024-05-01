import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {SharesModule} from "./Modules/Share/shares.module";
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import {MatDrawer, MatDrawerContainer} from "@angular/material/sidenav";
import {MatListItem, MatNavList} from "@angular/material/list";
import {HttpClientModule} from "@angular/common/http";
import {JwtModule} from "@auth0/angular-jwt";
import {AuthModule} from "./Modules/Auth/auth.module";
import {UserModule} from "./Modules/Users/user.module";
import {PokemonModule} from "./Modules/Pokemons/pokemon.module";

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    SharesModule,
    MatDrawerContainer,
    MatDrawer,
    MatNavList,
    MatListItem,
    HttpClientModule,
    AuthModule,
    UserModule,
    PokemonModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: function tokenGetter() {
          return localStorage.getItem('access_token');
        }
      },
    }),
  ],
  providers: [
    provideAnimationsAsync()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
