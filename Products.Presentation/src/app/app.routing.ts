import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { AppComponent } from './app.component';
import { NgModule } from '@angular/core';
import { IndexComponent } from './index/index.component';
import { AuthGuard } from './auth-guard.service';

const routes: Routes = [
  { 
    
    path: 'index',
    canActivate: [AuthGuard], 
   component: IndexComponent },
  { path: 'login', component: LoginComponent },
  { path: '**', redirectTo: 'login' }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [
    RouterModule
  ],
  declarations: []
})
export class AppRoutingModule { }