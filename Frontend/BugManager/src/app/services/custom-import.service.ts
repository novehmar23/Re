import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { catchError } from "rxjs";
import { environment } from "src/environments/environment";
import { ImporterInfo } from "../models/CustomImporter/ImporterInfo";
import { HttpErrorHandler } from "./error-handler";

@Injectable({
  providedIn: 'root'
})
export class CustomImportService {
  endpoint: string = `${environment.webApi_origin}/bugs/custom-importers`;
  options = { headers: { 'token': '', 'path': '' } };

  constructor(private http: HttpClient) { }

  importBugs(importer: ImporterInfo): any {
    this.options.headers.token = localStorage.getItem('token') || '';
    return this.http.post(this.endpoint, importer, this.options).pipe(catchError(HttpErrorHandler.handleError));
  }

  getImportersInfo(): any {
    this.options.headers.token = localStorage.getItem('token') || '';
    return this.http.get<ImporterInfo>(this.endpoint, this.options).pipe(catchError(HttpErrorHandler.handleError));
  }

}