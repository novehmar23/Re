import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DevRoutingModule } from './dev-routing.module';
import { DevComponent } from './dev.component';
import { AngularMaterialModule } from 'src/app/components/angular-material/angular-material.module';
import { ComponentsModule } from 'src/app/components/components.module';


@NgModule({
  declarations: [
    DevComponent
  ],
  imports: [
    CommonModule,
    DevRoutingModule,
    AngularMaterialModule,
    ComponentsModule,
  ]
})
export class DevModule { }
