import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {AuthService} from "../../Services/auth.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit {
  hide: boolean = true;
  loginForm: FormGroup = this.formBuilder.group({
    email: ['', Validators.required, Validators.email],
    password: ['', Validators.required]
  });

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router
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
    if (this.loginForm.invalid){
      this.loginForm.markAllAsTouched();
      this.loginForm.markAsDirty();
    }

    this.authService.login(this.loginForm.value).subscribe(
      {
        next: (response) => {
          localStorage.setItem('token', response.token);
        },
        error: (error) => {
          console.log(error);
        },
        complete: () => {
          this.router.navigate(['/pokemons']).then();
      }
    })
  }
}
