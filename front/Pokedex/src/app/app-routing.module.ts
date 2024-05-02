import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '', redirectTo: 'auth', pathMatch: 'full'
  },
  {
    path: 'auth',
    loadChildren: () => import('./Modules/Auth/auth.module').then(m => m.AuthModule),
  },
  {
    path: 'users',
    loadChildren: () => import('./Modules/Users/user.module').then(m => m.UserModule)
  },
  {
    path: 'pokemons',
    loadChildren: () => import('./Modules/Pokemons/pokemon.module').then(m => m.PokemonModule)
  },
  {
    path: '**', redirectTo: 'pokemons'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
