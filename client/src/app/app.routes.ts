import { Routes } from '@angular/router';
import { AnonymousGuard } from './shared/guards/anonymous.guard';
import { AuthGuard } from './shared/guards/auth.guard';

export const routes: Routes = [
    { path: '', loadComponent: () => import('./home/home.component').then((mod) => mod.HomeComponent), canActivate: [AuthGuard] },
    { path: 'auth', loadComponent: () => import('./auth/auth.component').then((mod) => mod.AuthComponent), canActivate: [AnonymousGuard] },
    { path: 'profile/:userId', loadComponent: () => import('./profile/profile.component').then((mod) => mod.ProfileComponent), canActivate: [AuthGuard] },
    { path: '**', redirectTo: '/', pathMatch: 'full' }
];
