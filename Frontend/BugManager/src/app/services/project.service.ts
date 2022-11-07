import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError } from 'rxjs';
import { Project } from 'src/app/models/Project';
import { environment } from 'src/environments/environment';
import { Developer } from '../models/Developer';
import { Tester } from '../models/Tester';
import { HttpErrorHandler } from './error-handler';

@Injectable({
  providedIn: 'root'
})
export class ProjectsService {
  endpoint = `${environment.webApi_origin}/projects`;
  options = { headers: { 'token': '', 'path': '' } };

  constructor(private http: HttpClient) {
    // this.option.header.token =....  cannot go here because 
    // the same instance of the class may be reuse across diferente users (and the token wouldn't be refresh)
  }

  getProjects(): any {
    this.options.headers.token = localStorage.getItem('token') || '';
    return this.http.get<Project[]>(this.endpoint, this.options).pipe(catchError(HttpErrorHandler.handleError));
  }

  getProject(id: number): any {
    this.options.headers.token = localStorage.getItem('token') || '';
    return this.http.get<Project>(`${this.endpoint}/${id}`, this.options).pipe(catchError(HttpErrorHandler.handleError));
  }

  createProject(project): any {
    this.options.headers.token = localStorage.getItem('token') || '';
    return this.http.post<Project[]>(this.endpoint, project, this.options).pipe(catchError(HttpErrorHandler.handleError));
  }

  editProject(project, id: number): any {
    this.options.headers.token = localStorage.getItem('token') || '';
    return this.http.put<Project[]>(`${this.endpoint}/${id}`, project, this.options).pipe(catchError(HttpErrorHandler.handleError));
  }

  getDevelopers(id: number): any {
    this.options.headers.token = localStorage.getItem('token') || '';
    return this.http.get<Developer[]>(`${this.endpoint}/${id}/devs`, this.options).pipe(catchError(HttpErrorHandler.handleError));
  }

  getTesters(id: number): any {
    this.options.headers.token = localStorage.getItem('token') || '';
    return this.http.get<Tester[]>(`${this.endpoint}/${id}/testers`, this.options).pipe(catchError(HttpErrorHandler.handleError));
  }

  deleteProject(id: number): any {
    this.options.headers.token = localStorage.getItem('token') || '';
    return this.http.delete(`${this.endpoint}/${id}`, this.options).pipe(catchError(HttpErrorHandler.handleError));
  }

  addDevToProject(projectId: number, devId: number): any {
    this.options.headers.token = localStorage.getItem('token') || '';
    let endpoint = `${this.endpoint}/${projectId}/devs/${devId}`;
    return this.http.post<Developer>(endpoint, null, this.options).pipe(catchError(HttpErrorHandler.handleError));
  }

  removeDevFromProject(projectId: number, devId: number): any {
    this.options.headers.token = localStorage.getItem('token') || '';
    let endpoint = `${this.endpoint}/${projectId}/devs/${devId}`;
    return this.http.delete<Developer>(endpoint, this.options).pipe(catchError(HttpErrorHandler.handleError));
  }

  addTesterToProject(projectId: number, testerId: number): any {
    this.options.headers.token = localStorage.getItem('token') || '';
    let endpoint = `${this.endpoint}/${projectId}/testers/${testerId}`;
    return this.http.post<Tester>(endpoint, null, this.options).pipe(catchError(HttpErrorHandler.handleError));
  }

  removeTesterFromProject(projectId: number, testerId: number): any {
    this.options.headers.token = localStorage.getItem('token') || '';
    let endpoint = `${this.endpoint}/${projectId}/testers/${testerId}`;
    return this.http.delete<Tester>(endpoint, this.options).pipe(catchError(HttpErrorHandler.handleError));
  }

}
