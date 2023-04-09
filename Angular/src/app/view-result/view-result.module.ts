import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ViewResultRoutingModule } from './view-result-routing.module';
import { ViewResultComponent } from './view-result.component';


@NgModule({
  declarations: [
    ViewResultComponent
  ],
  imports: [
    CommonModule,
    ViewResultRoutingModule
  ]
})
export class ViewResultModule { }
