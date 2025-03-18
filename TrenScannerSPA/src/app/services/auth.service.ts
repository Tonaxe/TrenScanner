import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private authenticated = false;
  private userRole: number | null = null;

  constructor() {
    this.authenticated = sessionStorage.getItem('token') !== null;
    this.userRole = sessionStorage.getItem('rol') ? parseInt(sessionStorage.getItem('rol')!, 10) : null;
  }

  isAuthenticated(): boolean {
    return this.authenticated;
  }

  isAdminUser(): number | null {
    return this.userRole;
  }

  login(role: number): void {
    this.authenticated = true;
    this.userRole = role;
    sessionStorage.setItem('rol', role.toString());
  }

  logout(): void {
    this.authenticated = false;
    this.userRole = null;
    sessionStorage.removeItem('token');
    sessionStorage.removeItem('rol');
  }
}
