import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ResultsGuard implements CanActivate {

  constructor(private router: Router) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    
    const hasSubmitted = sessionStorage.getItem('hasSubmitted'); 

    if (hasSubmitted === 'true') {
      return true;
    } else {
      this.router.navigate(['/home']);
      return false;
    }
  }
}
