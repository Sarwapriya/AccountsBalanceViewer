import { Component ,Inject} from '@angular/core';
import { FileServiceService } from '../file-service.service';
import { HttpClient, HttpParams } from '@angular/common/http';

@Component({
  selector: 'app-view-page',
  styleUrls: ['./view-page.component.css'],
  templateUrl: './view-page.component.html'
})
export class ViewPageComponent {
  constructor(private fileService: FileServiceService, private http: HttpClient, @Inject('API') private baseUrl: string ) {

  }
  dataSource: any[] = [];
  accountTypes: string[] = [];
  month = '';
  year = '';
  displayedColumns: string[] = ['R&D', 'Canteen', 'CEO\'s Car', 'Marketing', 'Parkings fines'];

  onChangeYear(event: any) {
    this.year = event.target.value;
  }
  onChangeMonth(event: any) {
    this.month = event.target.value;
  }
  onSubmit() {
    const url = `${this.baseUrl}/api/accountBalance/get-account-balance?year=${this.year}&month=${this.month}`;
    this.http.get<any[]>(url).subscribe(
      (data) => {
        const items = {};
        data.forEach((item)=> {
          items[item.accountType] = items[item.accountType] || 0;
          items[item.accountType]+= item.amount;
        });
        this.dataSource = Object.entries(items)
      },
      (error) => {
        console.log(error);
      }
    );
  }
  getAmount(group: any[]): number {
    return group.reduce((a, item) => a + item.amount, 0);
  };
}

