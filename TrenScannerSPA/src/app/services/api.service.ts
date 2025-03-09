import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TrenData } from '../models/trenData.model';
import { UserRegister } from '../models/userRegister.model';
import { TrenInfo } from '../models/trenInfo.model';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  private baseUrl = 'https://localhost:44355/';

  constructor(private http: HttpClient) {}

  postTrainsRoutes(trenData: TrenData): Observable<any> {
    return this.http.post(`${this.baseUrl}api/TrenData`, trenData);
  }

  postUserRegister(userRegister: UserRegister): Observable<any> {
    return this.http.post(`${this.baseUrl}api/UserData`, userRegister);
  }

  getUser(userEmail: string): Observable<any> {
    return this.http.post(`${this.baseUrl}api/GetUser`, JSON.stringify(userEmail), {
      headers: { 'Content-Type': 'application/json' }
    });
  }   

  getRecommendedTrains(): Observable<TrenInfo[]> {
    return this.http.get<TrenInfo[]>(`${this.baseUrl}api/GetRecomendedTrains`);
  }
}
