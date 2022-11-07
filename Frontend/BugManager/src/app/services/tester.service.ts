import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Tester } from '../models/Tester';
import { HttpErrorHandler } from './error-handler';

@Injectable({
  providedIn: 'root'
})
export class TesterService {
  endpoint = `${environment.webApi_origin}/testers`;
  options = { headers: { 'token': '', 'path': '' } };

  constructor(private http: HttpClient) { }

  getTesters(): any {
    this.options.headers.token = localStorage.getItem('token') || '';
    return this.http.get<Tester[]>(this.endpoint, this.options).pipe(catchError(HttpErrorHandler.handleError));
  }

}