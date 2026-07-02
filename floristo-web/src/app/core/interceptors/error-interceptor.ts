import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';
import { AuthService } from '../auth/auth';
import { ApiError } from '../models/api-error';
import { environment } from '../../../environments/environment';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
    const auth = inject(AuthService);
    const router = inject(Router);

    const isApiRequest = req.url.startsWith(environment.apiUrl);

    return next(req).pipe(
        catchError((error: HttpErrorResponse) => {
            if (!isApiRequest){
                return throwError(() => error);
            }

            if (error.status === 401){
                auth.signOut();
                router.navigate(['/login']);
            }

            const apiError: ApiError = {
                status: error.status,
                errorCode: error.error?.errorCode ?? 'UNKNOWN',
                message: error.error?.message ?? error.message ?? 'unknown error'
            }

            return throwError(() => apiError);
        })
    )
}