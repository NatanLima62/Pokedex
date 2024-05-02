import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthRoutingModule } from './auth-routing.module';
import { LoginComponent } from './Pages/login/login.component';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MatError, MatFormField, MatLabel, MatSuffix} from "@angular/material/form-field";
import {MatInput} from "@angular/material/input";
import {MatIcon} from "@angular/material/icon";
import {MatButton, MatIconButton} from "@angular/material/button";
import {AuthService} from "./Services/auth.service";
import {MatProgressSpinner} from "@angular/material/progress-spinner";
import { RegisterComponent } from './Pages/register/register.component';
import { RecoverPasswordComponent } from './Pages/recover-password/recover-password.component';
import {MatStep, MatStepLabel, MatStepper, MatStepperNext, MatStepperPrevious} from "@angular/material/stepper";

@NgModule({
  declarations: [
    RegisterComponent,
    LoginComponent,
    RecoverPasswordComponent,
  ],
  imports: [
    CommonModule,
    AuthRoutingModule,
    FormsModule,
    MatFormField,
    MatInput,
    MatIcon,
    MatLabel,
    MatIconButton,
    ReactiveFormsModule,
    MatError,
    MatButton,
    MatSuffix,
    MatProgressSpinner,
    MatStepper,
    MatStepperNext,
    MatStepLabel,
    MatStep,
    MatStepperPrevious,
  ],
  providers: [
    AuthService
  ]
})
export class AuthModule { }
