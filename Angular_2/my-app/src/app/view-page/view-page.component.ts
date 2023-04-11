import { Component } from '@angular/core';
import { FileServiceService } from '../file-service.service';


export interface PeriodicElement {
  Canteen: number;
  RD: number;
  CEO_Car: number;
  Marketing: number;
  Parkings_fines: number;
}

const ELEMENT_DATA: PeriodicElement[] = [
  { RD: 1, Canteen: 1000, CEO_Car: 1.0079, Marketing: 20000, Parkings_fines: 10000 }]

@Component({
  selector: 'app-view-page',
  styleUrls: ['./view-page.component.css'],
  templateUrl: './view-page.component.html'
})
export class ViewPageComponent {
  constructor(private fileService: FileServiceService) {

  }
  year: string = '';
  month: string = '';
  displayedColumns: string[] = ['R&D', 'Canteen', 'CEO\'s Car', 'Marketing', 'Parkings fines'];
  dataSource = ELEMENT_DATA;
  onChangeYear(event: any){
    this.year = event.target.value;
  }
  onChangeMonth(event: any){
    this.month = event.target.value;
  }
  getData(): void {

    const url = 'https://localhost:7106/api/accountBalance/get-account-balance';
    this.fileService.getData(url, this.month, this.year).subscribe(
      response => {
        console.log('Get Data:', response);
      }
    )
  }

}

