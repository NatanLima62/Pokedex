import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PokemonRoutingModule } from './pokemon-routing.module';
import { PokemonsComponent } from './Pages/pokemons/pokemons.component';
import { PokemonCardComponent } from './Components/pokemon-card/pokemon-card.component';
import {MatCard, MatCardContent, MatCardFooter, MatCardHeader, MatCardTitle} from "@angular/material/card";


@NgModule({
  declarations: [
    PokemonsComponent,
    PokemonCardComponent
  ],
  imports: [
    CommonModule,
    PokemonRoutingModule,
    MatCard,
    MatCardHeader,
    MatCardContent,
    MatCardFooter,
    MatCardTitle
  ]
})
export class PokemonModule { }
