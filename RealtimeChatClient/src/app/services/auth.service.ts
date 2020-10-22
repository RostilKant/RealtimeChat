import { Injectable } from '@angular/core';
import {Observable} from 'rxjs';
import {HttpClient} from '@angular/common/http';
import {RegistrationUserModel} from '../interfaces/registration-user.model';
import {tryCatch} from 'rxjs/internal-compatibility';
import {catchError, tap} from 'rxjs/operators';
import {LoginUserModel} from '../interfaces/login-user.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }

  private static setToken(response?): void {
    if (response){
      localStorage.setItem('jwt', `${response.token}`);
    } else {
      localStorage.clear();
    }
  }

  get token(): string {
    return localStorage.getItem('jwt');
  }

  register(user: RegistrationUserModel): Observable<any>{
    return this.http.post('https://localhost:5001/api/users/register', user);
  }

  login(user: LoginUserModel): Observable<any>{
    return this.http.post('https://localhost:5001/api/users/login',
      user)
      .pipe(
        tap(AuthService.setToken),
      );
  }

  logout(): void {
    AuthService.setToken(null);
  }

  isAuthenticated(): boolean{
    return !!this.token;
  }
}
