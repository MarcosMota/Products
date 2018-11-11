import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate  {

  constructor(public router: Router) {}
  canActivate(): boolean {
    debugger;
    if (!this.isAuthenticated()) {
      this.router.navigate(['login']);
      return false;
    }
    return true;
  }

  isAuthenticated(): boolean {
    const logged = localStorage.getItem('logged');
    return logged != null;
  }
}

