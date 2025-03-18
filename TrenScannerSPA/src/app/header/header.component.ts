import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  standalone: false,
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})

//el header contiene el logo un navbar que tiene el inicio y el panel de administracion (si eres admin)
//y un icono de usuario para ver tu informacion o cerrar sesion
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

    this.router.navigate(['/login']);
  }
}
