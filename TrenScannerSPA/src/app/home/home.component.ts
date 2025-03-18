import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ApiService } from '../services/api.service';
import { TrenData } from '../models/trenData.model';
import { TrenInfo } from '../models/trenInfo.model';
import { CsvInfo } from '../models/csvInfo.model';
import { Router } from '@angular/router';

@Component({
  standalone: false,
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  homeForm: FormGroup;
  trains: TrenInfo[] = [];
  csvInfo: CsvInfo[] = [];
  isDropdownOpen = false;
  selectedPassengersText = 'Selecciona los pasajeros';
  adults: number = 1;
  children: number = 0;
  infants: number = 0;

  constructor(private fb: FormBuilder, private apiService: ApiService, private router: Router) {
    this.homeForm = this.fb.group({
      origin: ['', Validators.required],
      destination: ['', Validators.required],
      departureDate: ['', Validators.required],
      returnDate: ['', Validators.required],
      adults: [1, Validators.min(1)],
      children: [0, Validators.min(0)],
      infants: [0, Validators.min(0)],
    });

  }

  ngOnInit(): void {
    const token = sessionStorage.getItem('token');
    console.log(token);
    this.getRecommendedTrains();
  }

  getRecommendedTrains(): void {
    this.apiService.getRecommendedTrains().subscribe(
      (res) => {
        this.trains = res;
        this.parseDate(this.trains);
      },
      (error) => {
        console.error('Error al obtener los trenes recomendados', error);
      }
    );
  }

  toggleDropdown(): void {
    this.isDropdownOpen = !this.isDropdownOpen;
  }

  updatePassengers(): void {
    this.selectedPassengersText = `Adultos: ${this.adults}, Niños: ${this.children}, Bebés: ${this.infants}`;
  }

  getTrainImage(destino: string): string {
    return `assets/images/${destino}.jpg`;
  }

  parseDate(trenInfo: TrenInfo[]): TrenInfo[] {
    trenInfo.forEach(tren => {
      if (tren.fecha) {
        const [startDate, endDate] = tren.fecha.split('/').map(date =>
          new Date(date).toLocaleDateString('es-ES')
        );
        tren.fecha = `${startDate} - ${endDate}`;
      }
    });

    return trenInfo;
  }

  onSubmit(): void {
    const trenData: TrenData = {
      origin: this.homeForm.value.origin,
      destination: this.homeForm.value.destination,
      departureDate: this.homeForm.value.departureDate,
      returnDate: this.homeForm.value.returnDate,
      adults: this.adults,
      children: this.children,
      infants: this.infants
    };

    this.apiService.postTrainsRoutes(trenData).subscribe(
      (res) => {
        this.csvInfo = res.data.result;
        console.log(this.csvInfo);
        this.router.navigate(['/results'], { state: { csvInfo: this.csvInfo } });
      },
      (error) => {
        console.error(error);
      }
    );
  }
}