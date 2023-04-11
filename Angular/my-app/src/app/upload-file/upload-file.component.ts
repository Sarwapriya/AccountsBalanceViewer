import { Component } from '@angular/core';
import { FileServiceService } from '../file-service.service';

@Component({
  selector: 'app-upload-file',
  templateUrl: './upload-file.component.html',
  styleUrls: ['./upload-file.component.css']
})
export class UploadFileComponent {
  constructor(private fileService: FileServiceService){

  }
  selectedFile?: File;
  onChange(event): void {
      this.selectedFile = event.target.files[0];
      console.log("selectedFile " +this.selectedFile);
    }
  onUpload(): void {
    
    console.log("onUpload " +this.selectedFile);
    if (this.selectedFile) {
      const url = 'http://localhost:7106/api/accountBalance/add-account-balance';
      this.fileService.uploadFile(url,this.selectedFile).subscribe(
        response => {
          console.log("file Uploaded successfully ");}
      )
      console.log('Uploading file:', this.selectedFile.name);
    }
  }
  
}
