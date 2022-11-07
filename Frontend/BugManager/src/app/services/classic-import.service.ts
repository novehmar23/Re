import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { catchError } from "rxjs";
import { environment } from "src/environments/environment";
import { HttpErrorHandler } from "./error-handler";

@Injectable({
  providedIn: 'root'
})
export class ClassicImportService {
  endpoint: string = `${environment.webApi_origin}/bugs/import/xml`;
  options = { headers: { 'token': '', 'path': '' } };

  constructor(private http: HttpClient) { }

  importBugs(path: string): any {
    this.options.headers.token = localStorage.getItem('token') || '';
    this.options.headers.path = path;
    return this.http.post(this.endpoint, {}, this.options).pipe(catchError(HttpErrorHandler.handleError));

  }

}
