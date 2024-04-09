import {inject, Injectable} from '@angular/core';
import { map } from 'rxjs/operators';
import {ApiService} from './api.service';
import {HttpParams} from '@angular/common/http';
import {GitHubRepoResult} from '../models/github-model';

@Injectable()
export class GeneralApiService {

  api = inject(ApiService);

  searchRepos(search: string) {
    const params = new HttpParams().append('search', search);

    return this.api.get<GitHubRepoResult>('/GithubRepository', params)
      .pipe(
        map((res) => res.data)
      );
  }
}


