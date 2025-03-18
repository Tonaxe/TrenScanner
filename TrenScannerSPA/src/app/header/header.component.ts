import { Component } from '@angular/core';
import { Router } from '@angular/router';  // Importa el Router para la navegación

@Component({
  selector: 'app-header',
  standalone: false,
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {
  isUserInfoVisible = false;

  user = {
    name: sessionStorage.getItem('nombre'),
    role: sessionStorage.getItem('rol')
  };

  constructor(private router: Router) {}

  toggleUserInfo(event: Event) {
    this.isUserInfoVisible = !this.isUserInfoVisible;
  }

  logout() {
    sessionStorage.removeItem('nombre');
    sessionStorage.removeItem('rol');
    sessionStorage.removeItem('token');

    console.log('Sesión cerrada');

    this.router.navigate(['/login']);
  }
}
