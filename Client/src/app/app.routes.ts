import { Routes } from '@angular/router';
import {GalleryComponent} from './pages/gallery/gallery.component';
import {BookmarksComponent} from './pages/bookmarks/bookmarks.component';
import {authGuard} from './guards/auth.guard';
import {RoutesNames} from './constants/routes';
export const routes: Routes = [
  { path: RoutesNames.search, title: 'Search', component: GalleryComponent, canActivate: [authGuard] },
  { path:  RoutesNames.bookmarks, title: 'Bookmarks', component: BookmarksComponent, canActivate: [authGuard] },
  { path: RoutesNames.login, title: 'Login', loadComponent: () => import('./components/unauthorized/login/login.component').then(x => x.LoginComponent) }
];
