import { Component } from '@angular/core';
import {Pokemon} from "../../Models/Pokemon";

@Component({
  selector: 'app-pokemons',
  templateUrl: './pokemons.component.html',
  styleUrl: './pokemons.component.scss'
})
export class PokemonsComponent {
  // Todo: obter os pokemons da API
  bulbasaur: Pokemon = {
    id: 1,
    name: 'Bulbasaur',
    types: ['Grass', 'Poison'],
    image: 'https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/1.png'
  };
  charmander: Pokemon = {
    id: 4,
    name: 'Charmander',
    types: ['Fire'],
    image: 'https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/4.png'
  };
  squirtle: Pokemon = {
    id: 7,
    name: 'Squirtle',
    types: ['Water'],
    image: 'https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/7.png'
  };

  pokemons: Pokemon[] = [
    this.bulbasaur,
    this.charmander,
    this.squirtle
  ]
}
