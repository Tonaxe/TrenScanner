import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TrenData } from '../models/trenData.model';
import { UserRegister } from '../models/userRegister.model';
import { TrenInfo } from '../models/trenInfo.model';
import { TrenInfoExtended } from '../models/trenInfoExtended.model';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  private baseUrl = 'https://localhost:44355/';

  constructor(private http: HttpClient) { }

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

  getAllTrains(): Observable<TrenInfoExtended[]> {
    return this.http.get<TrenInfoExtended[]>(`${this.baseUrl}api/AllTrains`);
  }

  deleteTrain(idViaje: number, token: string): Observable<any> {
    const headers = {
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    };
    return this.http.delete(`${this.baseUrl}api/DeleteTrain/${idViaje}`, { headers });
  }

  getViajeById(idViaje: number): Observable<TrenInfoExtended> {
    return this.http.get<TrenInfoExtended>(`${this.baseUrl}api/viajes/${idViaje}`);
  }

  updateViaje(id: number, viaje: TrenInfoExtended, token: string): Observable<TrenInfoExtended> {
    const headers = {
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    };
    return this.http.put<TrenInfoExtended>(`${this.baseUrl}api/viajes/${id}`, viaje, { headers });
  }
}
