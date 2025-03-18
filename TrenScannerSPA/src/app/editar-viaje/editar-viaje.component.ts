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
        console.error(error);
      }
    );
  }

  guardarCambios(): void {
    const token = sessionStorage.getItem('token');
    console.log(token);
    if (token) {
      console.log("123");
      this.apiService.updateViaje(this.id_viaje, this.viaje, token).subscribe(
        (res) => {
          this.route.navigate(['administracion']);
        },
        (error) => {
          console.error("Error al actualizar el viaje", error);
        }
      );
    } else {
      console.log('No token found');
    }
  }

  cancelarEdicion(): void {
    this.route.navigate(['administracion']);
  }
}
