import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class FileServiceService {
  private url = 'add-account-balance';
  constructor(private http: HttpClient) { }

  uploadFile(url: string, file: File): Observable<any> {
    const uploadData = new FormData();
    uploadData.append('file', file, file.name);

    return this.http.post(url, uploadData);
  }
  getData(url: string, month: string, year: string): Observable<any> {
    const option = {
      params : new HttpParams().set('year',year).set('month',month)
    }
    return this.http.get(url, option);
  }
}
