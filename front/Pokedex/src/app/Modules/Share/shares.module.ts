import { NgModule } from '@angular/core';
import {CommonModule, NgOptimizedImage} from '@angular/common';
import { NavbarComponent } from './Components/navbar/navbar.component';
import {MatToolbar} from "@angular/material/toolbar";
import {MatIcon} from "@angular/material/icon";
import {MatButton, MatIconButton} from "@angular/material/button";
import {MatDrawer, MatDrawerContainer} from "@angular/material/sidenav";
import {MatListItem, MatNavList} from "@angular/material/list";
import {RouterLink} from "@angular/router";
import { SnackbarErrorComponent } from './Components/snackbar-error/snackbar-error.component';
import {MatFormField} from "@angular/material/form-field";
import {MatInput} from "@angular/material/input";
import {FormsModule} from "@angular/forms";
import {MatSnackBarAction, MatSnackBarActions, MatSnackBarLabel} from "@angular/material/snack-bar";
import {MatDialogActions, MatDialogClose, MatDialogContent, MatDialogTitle} from "@angular/material/dialog";
import {MatStep, MatStepper} from "@angular/material/stepper";



@NgModule({
  declarations: [
    NavbarComponent,
    SnackbarErrorComponent,
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
    MatDrawer,
    MatFormField,
    MatInput,
    FormsModule,
    MatButton,
    MatSnackBarLabel,
    MatSnackBarActions,
    MatSnackBarAction,
    MatDialogContent,
    MatDialogClose,
    MatDialogActions,
    MatDialogTitle,
    MatStepper,
    MatStep
  ]
})
export class SharesModule { }
