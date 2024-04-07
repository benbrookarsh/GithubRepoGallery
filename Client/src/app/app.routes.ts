import { Routes } from '@angular/router';
import {LoginComponent} from './components/unauthorized/login/login.component';
import {GalleryComponent} from './pages/gallery/gallery.component';
import {BookmarksComponent} from './pages/bookmarks/bookmarks.component';
import {authGuard} from './guards/auth.guard';
import {RoutesNames} from './constants/routes';
export const routes: Routes = [
  { path: RoutesNames.search, title: 'Search', component: GalleryComponent, canActivate: [authGuard] },
  { path:  RoutesNames.bookmarks, title: 'Bookmarks', component: BookmarksComponent, canActivate: [authGuard] },
  { path: RoutesNames.login, title: 'Login', component: LoginComponent }
];
