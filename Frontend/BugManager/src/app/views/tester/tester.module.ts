import { NgModule } from '@angular/core';

import { TesterRoutingModule } from './tester-routing.module';
import { TesterComponent } from './tester.component';
import { AngularMaterialModule } from 'src/app/components/angular-material/angular-material.module';
import { ComponentsModule } from 'src/app/components/components.module';


@NgModule({
  declarations: [
    TesterComponent,
  ],
  imports: [
    TesterRoutingModule,
    AngularMaterialModule,
    ComponentsModule,
  ]
})
export class TesterModule {

}
