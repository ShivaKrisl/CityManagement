import { Injectable } from '@angular/core';
import { City } from '../models/city'; 
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

const API_BASE_URL : string = "https://localhost:7198/api/";

@Injectable({
  providedIn: 'root'
})
export class CitiesService {
  cities: City[] = [];
  constructor(private httpClient: HttpClient) {
    
  }
  
  public getCities(): Observable<City[]> {
    //let headers = new HttpHeaders();
    //headers = headers.append("Authorization", "Bearer mytoken");
    return this.httpClient.get<City[]>(`${API_BASE_URL}v1/city`);
  }

  public AddCity(city: City): Observable<City> {
    return this.httpClient.post<City>(`${API_BASE_URL}v1/city`, city);
  }


  public UpdateCity(city:City) : Observable<City>{
    return this.httpClient.put<City>(`${API_BASE_URL}v1/city/${city.cityId}`, city);
  }


  public DeleteCity(cityId: any): Observable<string> {
    return this.httpClient.delete<string>(`${API_BASE_URL}v1/city/${cityId}`);
  }


}
