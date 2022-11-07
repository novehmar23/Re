import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthorizationGuard } from './guards/authorization.guard';
import { LoginComponent } from './views/login/login.component';
import { NotFoundComponent } from './views/not-found/not-found.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'

  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'admin',
    canActivate: [AuthorizationGuard],
    data: { role: 'admin' },
    loadChildren: () => import('./views/admin/admin.module').then(a => a.AdminModule)
  },
  {
    path: 'dev',
    canActivate: [AuthorizationGuard],
    data: { role: 'dev' },
    loadChildren: () => import('./views/dev/dev.module').then(m => m.DevModule)
  },
  {
    path: 'tester',
    canActivate: [AuthorizationGuard],
    data: { role: 'tester' },
    loadChildren: () => import('./views/tester/tester.module').then(m => m.TesterModule)
  },
  {
    path: '404-not-found',
    component: NotFoundComponent
  },
  {
    path: '**',
    redirectTo: '404-not-found'
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }