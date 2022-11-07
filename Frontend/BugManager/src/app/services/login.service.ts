import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { LoginResponse } from 'src/app/views/login/models/loginResponse';
import { UserCredentials } from 'src/app/views/login/models/userCredentials';
import { environment } from 'src/environments/environment';
import { HttpErrorHandler } from './error-handler';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  endpoint = `${environment.webApi_origin}/login`;

  constructor(private http: HttpClient) { }

  login(credentials: UserCredentials): any {
    return this.http.post<LoginResponse>(this.endpoint, credentials).pipe(catchError(HttpErrorHandler.handleError));
  }
}
