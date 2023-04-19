import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ViewPageComponent } from './view-page/view-page.component';
import { UploadFileComponent } from './upload-file/upload-file.component';

import { MsalGuard } from '@azure/msal-angular';
import { HomeComponent } from './home/home.component';
import { ProfileComponent } from './profile/profile.component';
import { WebapiComponent } from './webapi/webapi.component';

const routes: Routes = [
  { path: 'view', component: ViewPageComponent },
  { path: 'upload', component: UploadFileComponent },
  { path: '', redirectTo: '/view', pathMatch: 'full' },
  {
    path: 'profile',
    component: ProfileComponent,
    canActivate: [MsalGuard]
  },
  {
    path: 'webapi',
    component: WebapiComponent,
    canActivate: [MsalGuard]
  },
  {
    path: '',
    component: HomeComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {
    initialNavigation:'enabledBlocking'
  })],
  exports: [RouterModule],
})
export class AppRoutingModule { }
