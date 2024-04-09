import {ActivatedRouteSnapshot, CanActivateFn, Router, RouterStateSnapshot} from '@angular/router';
import {inject} from '@angular/core';
import {RoutesNames} from '../constants/routes';

export const authGuard: CanActivateFn = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean => {
  const router = inject(Router);
  const isTokenExists = !!window.localStorage.getItem('TOKEN');
  if(!isTokenExists) {
    router.navigate([RoutesNames.login])
  }
  return isTokenExists;
};

