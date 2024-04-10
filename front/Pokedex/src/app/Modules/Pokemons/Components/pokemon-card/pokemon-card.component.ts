import {Component, Input} from '@angular/core';
import {Pokemon} from "../../Models/Pokemon";

@Component({
  selector: 'app-pokemon-card',
  templateUrl: './pokemon-card.component.html',
  styleUrl: './pokemon-card.component.scss'
})
export class PokemonCardComponent {
  @Input() pokemon!: Pokemon;
}
