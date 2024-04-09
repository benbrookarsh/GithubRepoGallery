import {ChangeDetectionStrategy, Component, inject} from '@angular/core';
import {RouterLink, RouterOutlet} from '@angular/router';
import {CommonModule} from '@angular/common';
import {MatToolbar} from '@angular/material/toolbar';
import {MatButton} from '@angular/material/button';
import {MatIcon} from '@angular/material/icon';
import {HttpClientModule} from '@angular/common/http';
import {ApiService} from './services/api.service';
import {GeneralApiService} from './services/general-api.service';
import {StorageService} from './services/storage.service';
import {MatSidenav, MatSidenavContainer, MatSidenavContent} from '@angular/material/sidenav';
import {MatListItem, MatNavList} from '@angular/material/list';
import {RoutesNames} from './constants/routes';
import {BookmarkService} from './services/bookmark.service';
import {AuthApiService} from './services/auth-api.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule, RouterLink, MatToolbar, MatButton, MatIcon, MatSidenavContent, MatSidenav, MatSidenavContainer, MatNavList, MatListItem],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [HttpClientModule, ApiService, GeneralApiService, StorageService, BookmarkService, AuthApiService]
})
export class AppComponent {
  state = inject(StorageService);
  authApi = inject(AuthApiService);

  routes = RoutesNames;

  ngOnInit() {
    this.authApi.refreshToken();
  }
}
