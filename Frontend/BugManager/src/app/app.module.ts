import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './views/login/login.component';
import { AngularMaterialModule } from './components/angular-material/angular-material.module';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthorizationGuard } from './guards/authorization.guard';
import { HttpClientModule } from '@angular/common/http';
import '@angular/compiler';
import { ComponentsModule } from './components/components.module';
import { NotFoundComponent } from './views/not-found/not-found.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    NotFoundComponent,
  ],
  imports: [
    AngularMaterialModule, // All Angular Material modules are importers to prevent missing dependancies 
    AppRoutingModule,
    CommonModule,
    RouterModule,
    BrowserAnimationsModule,
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    ComponentsModule,
  ],
  providers: [AuthorizationGuard],
  exports: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
