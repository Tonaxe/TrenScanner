import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ApiService } from '../services/api.service';
import { TrenData } from '../models/trenData.model';

@Component({
  standalone: false,
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  homeForm: FormGroup;
  isDropdownOpen = false;
  selectedPassengersText = 'Selecciona los pasajeros';
  adults: number = 1;
  children: number = 0;
  infants: number = 0;

  constructor(private fb: FormBuilder, private apiService: ApiService) {
    this.homeForm = this.fb.group({
      origin: ['', Validators.required],
      destination: ['', Validators.required],
      departureDate: ['', Validators.required],
      returnDate: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    const token = sessionStorage.getItem('token');
    console.log(token);
  }

  toggleDropdown(): void {
    this.isDropdownOpen = !this.isDropdownOpen;
  }

  updatePassengers(): void {
    this.selectedPassengersText = `Adultos: ${this.adults}, Niños: ${this.children}, Bebés: ${this.infants}`;
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
        console.log(res);
      },
      (error) => {
        console.error(error);
      }
    );
  }
}
