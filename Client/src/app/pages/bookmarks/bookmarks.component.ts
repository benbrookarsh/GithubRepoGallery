import {Component, inject} from '@angular/core';
import {StorageService} from '../../services/storage.service';
import {
  MatCard,
  MatCardActions,
  MatCardContent,
  MatCardHeader,
  MatCardImage,
  MatCardModule
} from '@angular/material/card';
import {MatButton, MatIconButton} from '@angular/material/button';
import {GeneralApiService} from '../../services/general-api.service';
import {Bookmark} from '../../models/bookmark';
import {BookmarkService} from '../../services/bookmark.service';
import {MatIcon} from '@angular/material/icon';

@Component({
  selector: 'app-bookmarks',
  standalone: true,
  imports: [
    MatCardModule,
    MatCardHeader,
    MatCardImage,
    MatCardContent,
    MatCardActions,
    MatButton,
    MatIconButton,
    MatIcon
  ],
  templateUrl: './bookmarks.component.html',
  styleUrl: './bookmarks.component.css'
})
export class BookmarksComponent {

  storage = inject(StorageService);
  bookmarkService = inject(BookmarkService);

  ngOnInit() {
    this.bookmarkService.getBookmarks();
  }

  deleteBookmark(bookmark: Bookmark) {
    return this.bookmarkService.deleteBookmark(bookmark);
  }
}