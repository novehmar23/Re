import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Developer } from '../models/Developer';
import { HttpErrorHandler } from './error-handler';

@Injectable({
  providedIn: 'root'
})
export class DeveloperService {
  endpoint = `${environment.webApi_origin}/devs`;
  options = { headers: { 'token': '', 'path': '' } };

  constructor(private http: HttpClient) { }

  getDevelopers(): any {
    this.options.headers.token = localStorage.getItem('token') || '';
    return this.http.get<Developer[]>(this.endpoint, this.options).pipe(catchError(HttpErrorHandler.handleError));
  }

}