import { Route } from '@angular/router';

export const appRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        loadComponent: () => import('./features/home/home').then((m) => m.Home),
        title: 'Floristo'
    },
    {
        path: 'logowanie',
        loadComponent: () => import('./features/auth/login/login').then((m) => m.Login),
        title: 'Zaloguj się | Floristo'
    },
    {
        path: 'rejestracja',
        loadComponent: () => import('./features/auth/register/register').then((m) => m.Register),
        title: 'Zarejestruj się | Floristo'
    },
    // TODO: wildcard route to NotFoundComponent
];
