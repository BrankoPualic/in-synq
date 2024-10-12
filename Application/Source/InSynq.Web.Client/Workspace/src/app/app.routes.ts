import { Routes } from '@angular/router';
import { Constants } from './constants/constants';
import { authGuard } from './guards/auth.guard';
import { myProfileGuard } from './guards/my-profile.guard';

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
                children: [
                    {
                        path: '',
                        title: 'Profile | ' + Constants.TITLE,
                        loadComponent: () =>
                            import('./pages/profile/profile.component').then(_ => _.ProfileComponent),
                    },
                    {
                        path: Constants.ROUTE_SETTINGS,
                        children: [
                            {
                                path: '',
                                title: 'Settings | ' + Constants.TITLE,
                                loadComponent: () =>
                                    import('./pages/profile/profile-settings/profile-settings.component').then(_ => _.ProfileSettingsComponent),
                            },
                            {
                                path: Constants.ROUTE_SETTINGS_AND_PRIVACY,
                                canActivate: [myProfileGuard],
                                children: [
                                    {
                                        path: '',
                                        title: 'Settings and Privacy | ' + Constants.TITLE,
                                        loadComponent: () =>
                                            import('./pages/profile/profile-settings/profile-settings-and-privacy/profile-settings-and-privacy.component').then(_ => _.ProfileSettingsAndPrivacyComponent)
                                    },
                                    {
                                        path: Constants.ROUTE_SETTINGS_AND_PRIVACY_PROFILE_PRIVACY,
                                        title: 'Profile Privacy | ' + Constants.TITLE,
                                        loadComponent: () =>
                                            import('./pages/profile/profile-settings/profile-settings-and-privacy/profile-privacy/profile-privacy.component').then(_ => _.ProfilePrivacyComponent)
                                    },
                                    {
                                        path: Constants.ROUTE_SETTINGS_AND_PRIVACY_PROFILE_ABOUT,
                                        title: 'Profile About | ' + Constants.TITLE,
                                        loadComponent: () =>
                                            import('./pages/profile/profile-settings/profile-settings-and-privacy/profile-about/profile-about.component').then(_ => _.ProfileAboutComponent)
                                    },
                                    {
                                        path: Constants.ROUTE_SETTINGS_AND_PRIVACY_ADD_PROFILE,
                                        title: 'Add Profile | ' + Constants.TITLE,
                                        loadComponent: () =>
                                            import('./pages/profile/profile-settings/profile-settings-and-privacy/add-profile/add-profile.component').then(_ => _.AddProfileComponent)
                                    }
                                ]
                            },
                            {
                                path: Constants.ROUTE_SETTINGS_ARCHIVE,
                                canActivate: [myProfileGuard],
                                title: 'Archive | ' + Constants.TITLE,
                                loadComponent: () =>
                                    import('./pages/profile/profile-settings/profile-archive/profile-archive.component').then(_ => _.ProfileArchiveComponent)
                            },
                            {
                                path: Constants.ROUTE_SETTINGS_SAVED,
                                canActivate: [myProfileGuard],
                                title: 'Saved | ' + Constants.TITLE,
                                loadComponent: () =>
                                    import('./pages/profile/profile-settings/profile-saved/profile-saved.component').then(_ => _.ProfileSavedComponent)
                            },
                            {
                                path: Constants.ROUTE_SETTINGS_REPORT,
                                title: 'Report | ' + Constants.TITLE,
                                loadComponent: () =>
                                    import('./pages/profile/profile-settings/profile-report/profile-report.component').then(_ => _.ProfileReportComponent)
                            },
                            {
                                path: Constants.ROUTE_SETTINGS_HELP,
                                children: [
                                    {
                                        path: '',
                                        title: 'Help | ' + Constants.TITLE,
                                        loadComponent: () =>
                                            import('./pages/profile/profile-settings/profile-help/profile-help.component').then(_ => _.ProfileHelpComponent)
                                    },
                                    {
                                        path: Constants.ROUTE_SETTINGS_HELP_DESK,
                                        title: 'Help Desk | ' + Constants.TITLE,
                                        loadComponent: () =>
                                            import('./pages/profile/profile-settings/profile-help/profile-help-desk/profile-help-desk.component').then(_ => _.ProfileHelpDeskComponent)
                                    },
                                    {
                                        path: Constants.ROUTE_SETTINGS_HELP_PRIVACY_POLICY,
                                        title: 'Privacy Policy | ' + Constants.TITLE,
                                        loadComponent: () =>
                                            import('./pages/profile/profile-settings/profile-help/profile-privacy-policy/profile-privacy-policy.component').then(_ => _.ProfilePrivacyPolicyComponent)
                                    },
                                    {
                                        path: Constants.ROUTE_SETTINGS_HELP_TERMS_OF_USE,
                                        title: 'Terms of Use | ' + Constants.TITLE,
                                        loadComponent: () =>
                                            import('./pages/profile/profile-settings/profile-help/profile-terms-of-use/profile-terms-of-use.component').then(_ => _.ProfileTermsOfUseComponent)
                                    },
                                ]
                            }
                        ]
                    }
                ]
            },
        ]
    },

    // Authentication and Authorization Pages
    {
        path: Constants.ROUTE_AUTH,
        canActivate: [authGuard],
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
