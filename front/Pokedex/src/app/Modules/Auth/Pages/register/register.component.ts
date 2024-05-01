import {Component, OnInit} from "@angular/core";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {MatSnackBar} from "@angular/material/snack-bar";
import {Router} from "@angular/router";
import {AuthService} from "../../Services/auth.service";
import {HttpErrorResponse} from "@angular/common/http";
import {SnackbarErrorComponent} from "../../../Share/Components/snackbar-error/snackbar-error.component";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent implements OnInit {
  hide: boolean = true;
  inProgress: boolean = false;
  registerForm: FormGroup = this.formBuilder.group({
    name: ['', Validators.required],
    email: ['', Validators.required],
    password: ['', Validators.required],
    image: ['']
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

  verifyError(field: string): any {
    if (field == null || field == '') {
      return [];
    }

    return this.getErrorMessages(field);
  }

  getErrorMessages(field: string): string[] {
    const fieldForm = this.registerForm.get(field);
    if (fieldForm == null) {
      return [];
    }

    let errors: string[] = [];
    if (fieldForm.hasError('required')) {
      errors.push(`The field ${field} is required!`);
    }

    if (fieldForm.hasError('email')) {
      errors.push(`The field ${field} is a invalid email`);
    }

    if (fieldForm.hasError('minlength')) {
      errors.push(`The field ${field} must have at least ${fieldForm.getError('minlength').requiredLength} characters`);
    }

    if (fieldForm.hasError('maxlength')) {
      errors.push(`The field ${field} must have at most ${fieldForm.getError('maxlength').requiredLength} characters`);
    }

    return errors;
  }

  register() {
    this.inProgress = true;
    console.log(this.registerForm.value);
    this.authService.register(this.registerForm.value).subscribe(
      {
        next: (response) => {
          this._snackBar.open('User successfully registered!', 'Close', {
            duration: 5000
          });
          this.inProgress = false;
        },

        error: (error: HttpErrorResponse) => {
          this._snackBar.openFromComponent(SnackbarErrorComponent, {
            data: {messages: error.status === 0 ? ["Unknown error"] : error.error.erros},
            duration: 5000
          });
          this.markHasErros();
          this.inProgress = false;
        },

        complete: () => {
          this.router.navigate(['/auth/login']).then();
        }
      })
  }

  markHasErros() {
    this.registerForm.markAllAsTouched();
    this.registerForm.markAsDirty();
    this.registerForm.get('name')?.setErrors({invalid: true});
    this.registerForm.get('email')?.setErrors({invalid: true});
    this.registerForm.get('password')?.setErrors({invalid: true});
    this.registerForm.get('image')?.setErrors({invalid: true});
  }

  onFileSelected($event: Event) {

  }
}
