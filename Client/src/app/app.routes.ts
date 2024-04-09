import { Routes } from '@angular/router';
import {authGuard} from './guards/auth.guard';
import {RoutesNames} from './constants/routes';

export const routes: Routes = [
  {
    path: RoutesNames.search,
    title: 'Search',
    loadComponent: () => import('./pages/gallery/gallery.component').then(x => x.GalleryComponent),
    canActivate: [authGuard]
  },
  {
    path:  RoutesNames.bookmarks,
    title: 'Bookmarks',
    loadComponent: () => import('./pages/bookmarks/bookmarks.component').then(x => x.BookmarksComponent),
    canActivate: [authGuard]
  },
  {
    path: RoutesNames.login,
    title: 'Login',
    loadComponent: () => import('./components/unauthorized/login/login.component').then(x => x.LoginComponent)
  }
];
