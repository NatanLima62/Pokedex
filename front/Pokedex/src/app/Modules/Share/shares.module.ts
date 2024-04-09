import { NgModule } from '@angular/core';
import {CommonModule, NgOptimizedImage} from '@angular/common';
import { NavbarComponent } from './Components/navbar/navbar.component';
import {MatToolbar} from "@angular/material/toolbar";
import {MatIcon} from "@angular/material/icon";
import {MatIconButton} from "@angular/material/button";
import {MatDrawer, MatDrawerContainer} from "@angular/material/sidenav";
import {MatListItem, MatNavList} from "@angular/material/list";
import {RouterLink} from "@angular/router";



@NgModule({
  declarations: [
    NavbarComponent,
  ],
  exports: [
    NavbarComponent
  ],
  imports: [
    CommonModule,
    MatToolbar,
    NgOptimizedImage,
    MatIcon,
    MatIconButton,
    MatDrawerContainer,
    MatNavList,
    MatListItem,
    RouterLink,
    MatDrawer
  ]
})
export class SharesModule { }
