import {Component} from '@angular/core';
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
export class LoginComponent {
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
    private _snackBar: MatSnackBar,
  ) {
  }

  verifyError(field: string): any {
    if (field == null || field == '') {
      return [];
    }

    return this.getErrorMessage(field);
  }

  getErrorMessage(campo: string): string[] {
    const fieldForm = this.loginForm.get(campo);
    if (fieldForm == null) {
      return [];
    }

    let errors: string[] = [];
    if (fieldForm.hasError('required')) {
      errors.push(`The field ${campo} is required!`);
    }

    if (fieldForm.hasError('email')) {
      errors.push(`The field ${campo} is a invalid email`);
    }

    if (fieldForm.hasError('minlength')) {
      errors.push(`The field ${campo} must have at least ${fieldForm.getError('minlength').requiredLength} characters`);
    }

    if (fieldForm.hasError('maxlength')) {
      errors.push(`The field ${campo} must have at most ${fieldForm.getError('maxlength').requiredLength} characters`);
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
      });
  }

  markHasErros() {
    this.loginForm.markAllAsTouched();
    this.loginForm.markAsDirty();
    this.loginForm.get('email')?.setErrors({invalid: true});
    this.loginForm.get('password')?.setErrors({invalid: true});
  }
}
