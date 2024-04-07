import {inject, Injectable, signal} from '@angular/core';
import {Router} from '@angular/router';
import {TokenMessage} from '../models/server-result';
import {Bookmark} from '../models/bookmark';
@Injectable()
export class StorageService {

  static readonly tokenKey = 'TOKEN';

  private router = inject(Router);

  userEmail = signal<string>('');
  bookmarks = signal<Bookmark[]>([]);

  private storage: Storage = window.localStorage;

  setToken(token: string) {
    // debug test if token is updated
    // const oldToken = this.getToken();
    // const eqality = oldToken === token;
    // console.log('Tokens are equal: ' + eqality);
    if (token) {
      this.setItem(StorageService.tokenKey, token);
    }
  }

  handleLogin(tokenMessage: TokenMessage) {
    this.setToken(tokenMessage?.token);
    this.userEmail.set(tokenMessage?.email);
    this.router.navigate(['search']);
  }

  logout() {
    this.storage.clear();
    this.userEmail.set('');
    this.router.navigate(['login']);
  }

  private setItem(key: string, value: string) {
    this.storage.setItem(key, value);
  }
}
