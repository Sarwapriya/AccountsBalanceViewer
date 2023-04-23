import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { msalConfig } from '../auth-config';
import { MsalBroadcastService, MsalService } from '@azure/msal-angular';
import { filter } from 'rxjs';
import { EventMessage, EventType, InteractionStatus } from '@azure/msal-browser';

@Component({
  selector: 'app-upload-file',
  templateUrl: './upload-file.component.html',
  styleUrls: ['./upload-file.component.css']
})
export class UploadFileComponent {
  loginDisplay = false;
  constructor(private http: HttpClient, @Inject('API') private baseUrl: string, private authService: MsalService, private msalBroadcastService: MsalBroadcastService) {

  }
  selectedFile?: File;
  onChange(event: any): void {
    this.selectedFile = event.target.files[0];
    console.log("selectedFile " + this.selectedFile);
  }
  onUpload() {
    const headers = new HttpHeaders();
    headers.append('Content-Type', 'multipart/form-data');
    headers.append('enctype', 'multipart/form-data');
    console.log("onUpload " + this.selectedFile);
    if (this.selectedFile) {
      const url = `${this.baseUrl}api/accountBalance/add-account-balance`;


      const uploadData: FormData = new FormData();
      uploadData.append('fileAccountBalances', this.selectedFile, this.selectedFile.name);

      console.log('Uploading file:', this.selectedFile.name);
      this.http.post<any[]>(url, uploadData, { headers }).subscribe(
        response => {
          alert("File uploaded successfully" + response);
          // Handle success
        }
      );
    }
  }

  setLoginDisplay() {
    this.loginDisplay = this.authService.instance.getAllAccounts().length > 0;
  }
}
