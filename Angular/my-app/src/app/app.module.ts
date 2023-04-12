import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ViewPageComponent } from './view-page/view-page.component';
import { UploadFileComponent } from './upload-file/upload-file.component';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import { NgPipesModule } from 'ngx-pipes';
import { environment } from 'src/environments/environment.prod';

@NgModule({
  declarations: [
    AppComponent,
    ViewPageComponent,
    UploadFileComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule,
    NgPipesModule,
    BrowserAnimationsModule
  ],
  providers: [
    {provide:'API',useValue:environment.baseUrl}
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }
