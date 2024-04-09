import {inject, Injectable} from '@angular/core';
import {Bookmark} from '../models/bookmark';
import {shareReplay, tap} from 'rxjs';
import {ApiService} from './api.service';
import {StorageService} from './storage.service';

@Injectable()
export class BookmarkService {

  api = inject(ApiService);
  storage = inject(StorageService);

  addBookmark(repo: Bookmark): void {
    this.api.post<Bookmark>('/Bookmark', repo)
      .pipe(
        tap((res) => {
            this.storage.bookmarks.update(bookmarks => [...bookmarks, res.data ]);
            this.storage.session.setItem('BOOKMARKS', JSON.stringify(this.storage.bookmarks()));
          }
        )).subscribe()
  }

  deleteBookmark(bookmark: Bookmark): void {
    this.api.delete<Bookmark>('/Bookmark', bookmark)
      .pipe(
        tap(() => {
            this.storage.bookmarks.update(bookmarks => bookmarks.filter(a => a.id !== bookmark.id));
            this.storage.session.setItem('BOOKMARKS', JSON.stringify(this.storage.bookmarks()));
          }
        )).subscribe()
  }

  getBookmarks(): void {
    this.api.get<Bookmark[]>('/Bookmark')
      .pipe(
        tap((res) => {
          this.storage.bookmarks.set(res.data);
          this.storage.session.setItem('BOOKMARKS', JSON.stringify(this.storage.bookmarks()));
        }),
        shareReplay(1)
      ).subscribe();
  }
}


