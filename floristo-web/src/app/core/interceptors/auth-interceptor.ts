import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '../auth/auth';
import { environment } from '../../../environments/environment';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
    const auth = inject(AuthService);

    const isApiRequest = req.url.startsWith(environment.apiUrl);
    const token = auth.accessToken();

    if (!isApiRequest || !token){
        return next(req);
    }

    const authorized = req.clone({
        setHeaders: { Authorization: `Bearer ${token}`}
    })

    return next(authorized);
}