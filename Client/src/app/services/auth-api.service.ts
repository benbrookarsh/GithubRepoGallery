import {inject, Injectable} from '@angular/core';
import {catchError, Observable, tap} from 'rxjs';
import {ServerResult, TokenMessage} from '../models/server-result';
import {RoutesNames} from '../constants/routes';
import {ApiService} from './api.service';
import {StorageService} from './storage.service';
import {Router} from '@angular/router';

@Injectable()
export class AuthApiService {


  private api = inject(ApiService);
  private storage = inject(StorageService);
  private router = inject(Router);


  login(email: string, password: string): Observable<ServerResult<TokenMessage>> {
    const body = {
      email,
      password
    }
    return this.api.post<TokenMessage>('/Auth/login', body)
      .pipe(
        tap( (res) => {
          this.storage.handleLogin(res.data);
        })
      );
  }

  register(email: string, password: string): Observable<ServerResult<TokenMessage>> {
    const body = {
      email,
      password
    };
    return this.api.post<TokenMessage>(`/Auth/register`, body)
      .pipe(
        tap( (res) => this.storage.handleLogin(res.data))
      );
  }

  refreshToken() {
    return this.api.get<TokenMessage>(`/Auth/refresh-token`)
      .pipe(
        tap((res) => this.storage.handleLogin(res.data)),
        catchError(() => {
          this.router.navigate([RoutesNames.login]);
          return [];
        })
      ).subscribe();
  }

  loginOrRegister(email: string, password: string, isLoginMode: boolean): Observable<ServerResult<TokenMessage>> {
    return isLoginMode? this.login(email, password) : this.register(email, password);
  }
}
