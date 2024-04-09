import {HttpInterceptorFn} from '@angular/common/http';


export const TokenInterceptor: HttpInterceptorFn = (req, next) => {
    const token = localStorage.getItem('TOKEN') ?? '';

    const cloned = req.clone({
      headers: req.headers.set('Authorization', 'Bearer ' + token)
    });

    return next(cloned);
};


