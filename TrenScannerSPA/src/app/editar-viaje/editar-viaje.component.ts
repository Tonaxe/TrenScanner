import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service';
import { ActivatedRoute, Router } from '@angular/router';
import { TrenInfoExtended } from '../models/trenInfoExtended.model';

@Component({
  selector: 'app-editar-viaje',
  standalone: false,
  templateUrl: './editar-viaje.component.html',
  styleUrl: './editar-viaje.component.css'
})

//se puede editar todos los campos de un viaje 
export class EditarViajeComponent implements OnInit {
  id_viaje!: number;
  viaje: TrenInfoExtended = {} as TrenInfoExtended;

  constructor(private apiService: ApiService, private router: ActivatedRoute, private route: Router) { }

  ngOnInit(): void {
    this.id_viaje = Number(this.router.snapshot.paramMap.get('id'));
    this.cargarViaje();
  }

  cargarViaje(): void {
    this.apiService.getViajeById(this.id_viaje).subscribe(
      (data) => {
        this.viaje = data;
      },
      (error) => {
      }
    );
  }

  guardarCambios(): void {
    const token = sessionStorage.getItem('token');
    if (token) {
      this.apiService.updateViaje(this.id_viaje, this.viaje, token).subscribe(
        (res) => {
          this.route.navigate(['administracion']);
        },
        (error) => {
        }
      );
    } else {
    }
  }

  cancelarEdicion(): void {
    this.route.navigate(['administracion']);
  }
}
