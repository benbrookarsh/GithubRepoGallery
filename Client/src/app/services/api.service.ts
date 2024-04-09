import {inject, Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import { map } from 'rxjs/operators';
import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import {ServerResult} from '../models/server-result';

@Injectable()
export class ApiService {

  http = inject(HttpClient);
  private baseUrl = 'https://localhost:7168';

  post<T>(url: string, body: any): Observable<ServerResult<T>> {
    return this.http.post<ServerResult<T>>(this.baseUrl + url, body, {observe: 'response'})
      .pipe(
        map((response) => response.body)
      );
  }

  get<T>(url: string, params?: HttpParams): Observable<ServerResult<T>> {
    const options = {headers: this.getHeaders(), params };
    return this.http.get<ServerResult<T>>(this.baseUrl + url, options);
  }

  private getHeaders(): HttpHeaders {
    let headers = new HttpHeaders();
    headers = headers.append('Content-Type', 'application/json');
    return headers;
  }

  delete<T>(url: string, body: any) {
    return this.http.delete<ServerResult<T>>(this.baseUrl + url, {body})
      .pipe(
        map((response) => response.data)
      );
  }
}


