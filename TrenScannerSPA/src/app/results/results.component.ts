import { Component, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-results',
  standalone: false,
  templateUrl: './results.component.html',
  styleUrl: './results.component.css'
})

//al buscar en el formulario y acabado el scrapeo te muestra la informacion obtenida
export class ResultsComponent {
  @Input() csvInfo: any[] = [];

  constructor(private route: ActivatedRoute, private router: Router) {
    const navigation = this.router.getCurrentNavigation();
    this.csvInfo = navigation?.extras.state?.['csvInfo'] || [];
  }
}
