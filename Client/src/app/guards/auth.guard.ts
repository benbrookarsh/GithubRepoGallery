import {ActivatedRouteSnapshot, CanActivateFn, Router, RouterStateSnapshot} from '@angular/router';
import {inject} from '@angular/core';
import {RoutesNames} from '../constants/routes';

export const authGuard: CanActivateFn = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean => {
  const router = inject(Router);
  const jwtHelper = inject(JwtHelperService);

  const isTokenExists = !!window.localStorage.getItem('TOKEN');
  if(!isTokenExists) {
    router.navigate([RoutesNames.login])
  }
  return isTokenExists;
};

export const canActivate = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
  const token = localStorage.getItem('TOKEN');
  if (!token || this.jwtHelper.isTokenExpired(token)) {
    this.router.navigate([RoutesNames.login]);
    return false;
  }
  return true;
}
