import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { catchError } from "rxjs";
import { environment } from "src/environments/environment";
import { User } from "../models/User";
import { HttpErrorHandler } from "./error-handler";

@Injectable({
  providedIn: 'root'
})
export class UserService {
  endpoint: string;
  options = { headers: { 'token': '' } };

  constructor(private http: HttpClient) { }

  createUser(user: User, role: string): any {
    this.endpoint = `${environment.webApi_origin}/${role}s`; // The endpoint is the name of the role in plural
    this.options.headers.token = localStorage.getItem('token') || '';
    return this.http.post<User>(this.endpoint, user, this.options).pipe(catchError(HttpErrorHandler.handleError));

  }

}
