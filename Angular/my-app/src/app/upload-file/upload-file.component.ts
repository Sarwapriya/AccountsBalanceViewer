import { Component ,Inject} from '@angular/core';
import { FileServiceService } from '../file-service.service';
import { HttpClient, HttpParams } from '@angular/common/http';

@Component({
  selector: 'app-upload-file',
  templateUrl: './upload-file.component.html',
  styleUrls: ['./upload-file.component.css']
})
export class UploadFileComponent {
  constructor(private fileService: FileServiceService, private http: HttpClient, @Inject('API') private baseUrl: string ){

  }
  selectedFile?: File;
  onChange(event): void {
      this.selectedFile = event.target.files[0];
      console.log("selectedFile " +this.selectedFile);
    }
  onUpload(): void {
    
    console.log("onUpload " +this.selectedFile);
    if (this.selectedFile) {
      const url = `${this.baseUrl}api/accountBalance/add-account-balance`;
      //const url = 'https://localhost:4200/api/accountBalance/add-account-balance';
      //this.http..post()
      this.fileService.uploadFile(url,this.selectedFile).subscribe(
        response => {
          console.log("file Uploaded successfully ");}
      )
      console.log('Uploading file:', this.selectedFile.name);
    }
  }
  
}
