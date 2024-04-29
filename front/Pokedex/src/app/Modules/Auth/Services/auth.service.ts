import {Injectable} from '@angular/core';
import {LoginViewModel, TokenViewModel} from "../Models/AuthViewModel";
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

  logout(){
    localStorage.removeItem('token');
    this.router.navigate(['/auth']).then();
  }

  verifyToken(){
    const token = localStorage.getItem('token');
    if(token == null){
      this.router.navigate(['/auth']).then();
    }

    if(this.jwtHelper.isTokenExpired(token)){
      localStorage.removeItem('token');
      this.router.navigate(['/auth']).then();
    }
  }
}
