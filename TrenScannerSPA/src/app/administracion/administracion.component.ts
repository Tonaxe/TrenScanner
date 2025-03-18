import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service';
import { Router } from '@angular/router';
import { TrenInfoExtended } from '../models/trenInfoExtended.model';

@Component({
  selector: 'app-administracion',
  standalone: false,
  templateUrl: './administracion.component.html',
  styleUrls: ['./administracion.component.css']
})

//hago la llamada de todos los viajes y los muestro en pantalla 
  //puedo borrar y editar un registro
  //solo los admins pueden entrar a aqui
export class AdministracionComponent implements OnInit {

  trenes: TrenInfoExtended[] = [];
  trenesPorPagina: TrenInfoExtended[] = [];
  paginaActual: number = 1;
  totalTrenes: number = 0;
  trenesPorPaginaCount: number = 10;

  constructor(private apiService: ApiService, private router: Router) { }

  ngOnInit(): void {
    this.apiService.getAllTrains().subscribe(
      (res) => {
        this.trenes = res;
        this.totalTrenes = this.trenes.length;
        this.cargarPagina(this.paginaActual);
      },
      (error) => {
      }
    );
  }

  cargarPagina(pagina: number): void {
    this.paginaActual = pagina;

    const inicio = (pagina - 1) * this.trenesPorPaginaCount;
    const fin = pagina * this.trenesPorPaginaCount;

    this.trenesPorPagina = this.trenes.slice(inicio, fin);
  }

  siguientePagina(): void {
    if (this.paginaActual < Math.ceil(this.totalTrenes / this.trenesPorPaginaCount)) {
      this.cargarPagina(this.paginaActual + 1);
    }
  }

  paginaAnterior(): void {
    if (this.paginaActual > 1) {
      this.cargarPagina(this.paginaActual - 1);
    }
  }

  deleteTren(id_viaje: number): void {
    const token = sessionStorage.getItem('token');

    if (!token) {
      return;
    }

    this.apiService.deleteTrain(id_viaje, token).subscribe(
      (res) => {
        window.location.reload();
      },
      (error) => {
      }
    );
  }

  editTren(id_viaje: number): void {
    this.router.navigate(['administracion/editar-viaje', id_viaje]);
  }
}
