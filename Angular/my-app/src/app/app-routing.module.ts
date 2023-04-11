import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ViewPageComponent } from './view-page/view-page.component';
import { UploadFileComponent } from './upload-file/upload-file.component';

const routes: Routes = [{ path: 'view', component: ViewPageComponent},
{ path: 'upload', component: UploadFileComponent },
{ path: '', redirectTo: '/view', pathMatch: 'full' }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
