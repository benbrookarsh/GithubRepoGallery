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
import {MatButton} from '@angular/material/button';
import {GeneralApiService} from '../../services/general-api.service';
import {Bookmark} from '../../models/bookmark';

@Component({
  selector: 'app-bookmarks',
  standalone: true,
  imports: [
    MatCardModule,
    MatCardHeader,
    MatCardImage,
    MatCardContent,
    MatCardActions,
    MatButton
  ],
  templateUrl: './bookmarks.component.html',
  styleUrl: './bookmarks.component.css'
})
export class BookmarksComponent {

  storage = inject(StorageService);
  api = inject(GeneralApiService);

  ngOnInit() {
    this.api.getBookmarks();
  }

  deleteBookmark(bookmark: Bookmark) {
    return this.api.deleteBookmark(bookmark);
  }
}
