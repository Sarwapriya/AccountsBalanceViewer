import { Component ,Inject} from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

@Component({
  selector: 'app-upload-file',
  templateUrl: './upload-file.component.html',
  styleUrls: ['./upload-file.component.css']
})
export class UploadFileComponent {
  constructor(private http: HttpClient, @Inject('API') private baseUrl: string ){

  }
  selectedFile?: File;
  onChange(event :any): void {
      this.selectedFile = event.target.files[0];
      console.log("selectedFile " +this.selectedFile);
    }
  onUpload() {
    
    console.log("onUpload " +this.selectedFile);
    if (this.selectedFile) {
      const url = `${this.baseUrl}api/accountBalance/add-account-balance`;
      //const url = 'https://localhost:4200/api/accountBalance/add-account-balance';
      //this.http..post()


      const uploadData: FormData = new FormData();
      uploadData.append('file', this.selectedFile, this.selectedFile.name);
  
      console.log('Uploading file:', this.selectedFile.name);
      this.http.post<any[]>(url, uploadData).subscribe(
        response => {
          console.log('File uploaded successfully', response);
          // Handle success
        }
      );
    }
  }
  
}
