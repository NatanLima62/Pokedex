import {Component, OnInit} from '@angular/core';
import {FormBuilder, Validators} from "@angular/forms";
import {AuthService} from "../../Services/auth.service";
import {ActivatedRoute, Router} from "@angular/router";
import {MatSnackBar} from "@angular/material/snack-bar";

@Component({
  selector: 'app-recover-password',
  templateUrl: './recover-password.component.html',
  styleUrl: './recover-password.component.scss'
})
export class RecoverPasswordComponent implements OnInit {
  email: string = '';
  step: number = 1;


  hide: boolean = true;
  inProgress: boolean = false;
  emailForm = this.formBuilder.group({
    email: ['', Validators.required]
  });
  recoverPasswordForm = this.formBuilder.group({
    email: ['', Validators.required],
    password: ['', Validators.required],
    confirmPassword: ['', Validators.required],
    token: ['', Validators.required],
  });

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute,
    private _snackBar: MatSnackBar,
  ) {
  }

  ngOnInit(): void {
    const email = this.route.snapshot.queryParamMap.get('email');
    const token = this.route.snapshot.queryParamMap.get('token');
    if ((email != null && email != '') && (token != null && token != '')) {
      this.recoverPasswordForm.get('email')?.setValue(email);
      this.recoverPasswordForm.get('token')?.setValue(token);
      this.step = 2;
    }
  }

  sendEmail() {
    this.inProgress = true;
    const email = this.emailForm.get('email')?.value;
    if (email != null && email != '' && email != undefined) {
      this.authService.recoverPassword(email).subscribe({
        complete: () => {
          this._snackBar.open('Email sent successfully! Verify your email to continue', 'Close', {
            duration: 5000
          });
          this.inProgress = false;
        }
      });
    }
  }

  recoverPassword() {
    this.inProgress = true;
    this.authService.resetPassword({
      email: this.recoverPasswordForm.get('email')?.value || '',
      password: this.recoverPasswordForm.get('password')?.value || '',
      confirmPassword: this.recoverPasswordForm.get('confirmPassword')?.value || '',
      token: this.recoverPasswordForm.get('token')?.value || '',
    }).subscribe({
      complete: () => {
        this.router.navigate(['/auth/login']).then();
        this.inProgress = false;
      }
    });
  }

  verifyError(field: string, form: any): any {
    if (field == null || field == '') {
      return [];
    }

    return this.getErrorMessage(field, form);
  }

  getErrorMessage(campo: string, form: any): string[] {
    const fieldForm = form.get(campo);
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

  checkIsValid(): boolean {
    const senha = this.recoverPasswordForm.get('password')?.value;
    const confirmacao = this.recoverPasswordForm.get('confirmPassword')?.value;
    if(senha == '' || senha == null || confirmacao == '' || confirmacao == null){
      return false;
    }

    return senha == confirmacao;
  }
}
