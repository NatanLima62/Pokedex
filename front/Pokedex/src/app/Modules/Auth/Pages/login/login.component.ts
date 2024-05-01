import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {AuthService} from "../../Services/auth.service";
import {Router} from "@angular/router";
import {MatSnackBar} from "@angular/material/snack-bar";
import {SnackbarErrorComponent} from "../../../Share/Components/snackbar-error/snackbar-error.component";
import {HttpErrorResponse} from "@angular/common/http";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit {
  hide: boolean = true;
  inProgress: boolean = false;
  loginForm: FormGroup = this.formBuilder.group({
    email: ['', Validators.required],
    password: ['', Validators.required]
  });

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private _snackBar: MatSnackBar
  ) {
  }

  ngOnInit(): void {

  }

  verifyError(campo: string): any {
    if(campo == null && campo == ''){
      return [];
    }

    const emailForm = this.loginForm.get('email');
    if(emailForm == null){
      return [];
    }

    if (emailForm.touched || emailForm.dirty) {
      return this.getErrorMessage(campo);
    }
  }

  getErrorMessage(campo: string): string[] {
    const campoForm = this.loginForm.get(campo);
    if(campoForm == null){
      return [];
    }

    let errors: string[] = [];
    if (campoForm.hasError('required')) {

      errors.push(`O campo ${campo} é obrigatório`);
    }
    if (campoForm.hasError('email')) {
      errors.push(`O campo ${campo} não é um email válido`);
    }
    if (campoForm.hasError('minlength')) {
      errors.push(`O campo ${campo} deve ter no mínimo ${campoForm.getError('minlength').requiredLength} caracteres`);
    }
    if (campoForm.hasError('maxlength')) {
      errors.push(`O campo ${campo} deve ter no máximo ${campoForm.getError('maxlength').requiredLength} caracteres`);
    }
    return errors;
  }

  login() {
    this.inProgress = true;
    this.authService.login(this.loginForm.value).subscribe(
      {
        next: (response) => {
          localStorage.setItem('token', response.token);
        },
        error: (error: HttpErrorResponse) => {
          this._snackBar.openFromComponent(SnackbarErrorComponent, {
            data: {messages: error.status === 0 ? ["Erro desconhecido"] : error.error},
            duration: 5000
          });
          this.markHasErros();
          this.inProgress = false;
        },
        complete: () => {
          this.router.navigate(['/pokemons']).then();
      }
    })
  }

  markHasErros(){
    this.loginForm.markAllAsTouched();
    this.loginForm.markAsDirty();
    this.loginForm.get('email')?.setErrors({invalid: true});
    this.loginForm.get('password')?.setErrors( {invalid: true});
  }
}
