import {inject, Injectable} from '@angular/core';
import {GitHubRepo} from '../models/github-model';
import {Bookmark} from '../models/bookmark';
import {tap} from 'rxjs';
import {ApiService} from './api.service';
import {StorageService} from './storage.service';

@Injectable()
export class BookmarkService {

  api = inject(ApiService);
  storage = inject(StorageService);


  addBookmark(repo: Bookmark) {
    return this.api.post<Bookmark>('/Bookmark/add', repo)
      .pipe(
        tap((res) => {
            this.storage.bookmarks.update(bookmarks => [...bookmarks, res.data ])
          }
        )).subscribe()
  }

  deleteBookmark(bookmark: Bookmark) {
    return this.api.post<Bookmark>('/Bookmark/delete', bookmark)
      .pipe(
        tap(() => {
            this.storage.bookmarks.update(bookmarks => bookmarks.filter(a => a.id !== bookmark.id))
          }
        )).subscribe()
  }

  getBookmarks() {
    return this.api.get<Bookmark[]>('/Bookmark')
      .pipe(
        tap((res) => this.storage.bookmarks.set(res.data))
      ).subscribe();
  }
}
