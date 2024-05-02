import {Injectable} from '@angular/core';
import {
  ChangePasswordAuthenticatedUserViewModel,
  ChangePasswordUserViewModel,
  LoginViewModel,
  RegisterResponseViewModel,
  RegisterViewModel,
  TokenViewModel
} from "../Models/AuthViewModel";
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {baseUrl} from "../../../../environment";
import {Router} from "@angular/router";
import {JwtHelperService} from "@auth0/angular-jwt";

@Injectable({
  providedIn: 'root'
})
export class AuthService{

  constructor(
    private http: HttpClient,
    private router: Router,
    private jwtHelper: JwtHelperService
  ) {
  }

  login(login: LoginViewModel): Observable<TokenViewModel>{
    return this.http.post<TokenViewModel>(`${baseUrl}/api/v1/auth`, login);
  }

  register(register: RegisterViewModel): Observable<RegisterResponseViewModel>{
    const formData = new FormData();
    formData.append('name', register.name);
    formData.append('email', register.email);
    formData.append('password', register.password);
    formData.append('image', register.image);

    return this.http.post<RegisterResponseViewModel>(`${baseUrl}/api/v1/users`, formData);
  }

  recoverPassword(email: string): Observable<void>{
    return this.http.post<any>(`${baseUrl}/api/v1/auth/recover-password?email=${email}`, null);
  }

  resetPassword(reset: ChangePasswordUserViewModel): Observable<void>{
    return this.http.post<any>(`${baseUrl}/api/v1/auth/reset-password`, reset);
  }

  changePassword(change: ChangePasswordAuthenticatedUserViewModel): Observable<void>{
    return this.http.post<any>(`${baseUrl}/api/v1/auth/change-password`, change);
  }

  logout(){
    localStorage.removeItem('token');
    this.router.navigate(['/auth/login']).then();
  }

  verifyToken(): boolean{
    const token = localStorage.getItem('token');
    if(token == null || this.jwtHelper.isTokenExpired(token)){
      this.router.navigate(['/auth/login']).then();
      return false;
    }

    if(this.jwtHelper.isTokenExpired(token)){
      localStorage.removeItem('token');
      this.router.navigate(['/auth/login']).then();
      return false;
    }

    return true;
  }
}
