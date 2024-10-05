import { Routes } from '@angular/router';
import { Constants } from './constants/constants';
import { authGuard } from './guards/auth.guard';

export const routes: Routes = [
    {
        path: Constants.ROUTE_HOME,
        canActivate: [authGuard],
        runGuardsAndResolvers: 'always',
        children: [
            {
                path: Constants.ROUTE_HOME,
                title: 'Home | ' + Constants.TITLE,
                loadComponent: () =>
                    import('./pages/home/home.component').then(_ => _.HomeComponent),
                pathMatch: 'full'
            },

            // Profile Pages
            {
                path: Constants.ROUTE_PROFILE + '/' + Constants.PARAM_ID,
                title: 'Profile | ' + Constants.TITLE,
                loadComponent: () =>
                    import('./pages/profile/profile.component').then(_ => _.ProfileComponent)
            }
        ]
    },

    // Authentication and Authorization Pages
    {
        path: Constants.ROUTE_AUTH,
        children: [
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
            {
                path: '',
                redirectTo: Constants.ROUTE_AUTH_SIGNIN,
                pathMatch: 'full',
            },
        ]
    },

    // Error Pages
    {
        path: Constants.ROUTE_NOT_FOUND,
        title: 'Not Found | ' + Constants.TITLE,
        loadComponent: () =>
            import('./pages/errors/not-found.component').then(_ => _.NotFoundComponent),
    },
    {
        path: Constants.ROUTE_UNAUTHORIZED,
        title: 'Unauthorized | ' + Constants.TITLE,
        loadComponent: () =>
            import('./pages/errors/unauthorized.component').then(_ => _.UnauthorizedComponent),
    },
    {
        path: '**',
        redirectTo: Constants.ROUTE_NOT_FOUND,
        pathMatch: 'full',
    },
];
