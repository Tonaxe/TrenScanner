import { Component } from '@angular/core';

@Component({
  selector: 'app-header',
  standalone: false,
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {
  isUserInfoVisible = false;

  user = {
    name: 'Tonaxe',
    role: 'Admin'
  };

  toggleUserInfo() {
    this.isUserInfoVisible = !this.isUserInfoVisible;
  }

  logout() {
    console.log('Sesión cerrada');
  }
}
