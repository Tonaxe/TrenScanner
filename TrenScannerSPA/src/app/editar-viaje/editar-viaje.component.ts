import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service';
import { ActivatedRoute } from '@angular/router';
import { TrenInfoExtended } from '../models/trenInfoExtended.model';

@Component({
  selector: 'app-editar-viaje',
  standalone: false,
  templateUrl: './editar-viaje.component.html',
  styleUrl: './editar-viaje.component.css'
})
export class EditarViajeComponent implements OnInit{
  id_viaje!: number;
  //viaje: TrenInfoExtended = {};
  viaje: any = {};

  constructor(private apiService: ApiService, private router: ActivatedRoute) {}

  ngOnInit(): void {
    this.id_viaje = Number(this.router.snapshot.paramMap.get('id'));
    //this.cargarViaje();
  }

  cargarViaje(): void {
    // this.apiService.getViajeById(this.id_viaje).subscribe((data) => {
    //   this.viaje = data;
    // });
  }

  guardarCambios(): void {
    // this.apiService.updateViaje(this.id_viaje, this.viaje).subscribe(() => {
    //   alert('Viaje actualizado con éxito');
    // });
  }
}
