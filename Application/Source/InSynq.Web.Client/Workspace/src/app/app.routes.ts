import { Routes } from '@angular/router';
import { Constants } from './constants/constants';

export const routes: Routes = [
    // Authentication and Authorization
    {
        path: '',
        redirectTo: Constants.ROUTE_AUTH,
        pathMatch: 'full',
    },
    {
        path: Constants.ROUTE_AUTH,
        children: [
            {
                path: '',
                redirectTo: Constants.ROUTE_AUTH_SIGNIN,
                pathMatch: 'full',
            },
            {
                path: Constants.ROUTE_AUTH_SIGNIN,
                title: 'Signin | ' + Constants.TITLE,
                loadComponent: () =>
                    import('./pages/auth/signin/signin.component').then(_ => _.SigninComponent),
            },
            {
                path: Constants.ROUTE_AUTH_SIGNUP,
                title: 'Signup | ' + Constants.TITLE,
                loadComponent: () =>
                    import('./pages/auth/signup/signup.component').then(_ => _.SignupComponent),
            },
        ]
    }
];
