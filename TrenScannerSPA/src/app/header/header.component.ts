import { Component, ElementRef, Renderer2, OnInit, OnDestroy } from '@angular/core';

@Component({
  selector: 'app-header',
  standalone: false,
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  isUserInfoVisible = false;
  user = { name: 'Tonaxe', role: 'Admin' };

  constructor(private elRef: ElementRef, private renderer: Renderer2) {}

  ngOnInit() {
    this.renderer.listen('document', 'click', (event: Event) => {
      if (!this.elRef.nativeElement.contains(event.target)) {
        this.isUserInfoVisible = false;
      }
    });
  }

  toggleUserInfo(event: Event) {
    event.stopPropagation();
    this.isUserInfoVisible = !this.isUserInfoVisible;
  }

  logout() {
    console.log('Sesión cerrada');
  }
}