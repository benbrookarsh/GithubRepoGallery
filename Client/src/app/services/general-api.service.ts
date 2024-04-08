import {inject, Injectable} from '@angular/core';
import {catchError, Observable, tap} from 'rxjs';
import { map } from 'rxjs/operators';
import {ServerResult, TokenMessage} from '../models/server-result';
import {ApiService} from './api.service';
import {StorageService} from './storage.service';
import {HttpParams} from '@angular/common/http';
import {GitHubRepo, GitHubRepoResult} from '../models/github-model';
import {Bookmark} from '../models/bookmark';
import {Router} from '@angular/router';
import {RoutesNames} from '../constants/routes';

@Injectable()
export class GeneralApiService {

  api = inject(ApiService);
  storage = inject(StorageService);
  router = inject(Router);

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

  searchRepos(search: string) {
    const params = new HttpParams()
      .append('search', search);

    return this.api.get<GitHubRepoResult>('/Repo/search', params)
      .pipe(
        map((res) => res.data)
      );
  }
}


