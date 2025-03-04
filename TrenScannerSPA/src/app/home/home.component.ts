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

    constructor(private fb: FormBuilder, private apiService: ApiService) {
      this.homeForm = this.fb.group({
        origin: ['', Validators.required],
        destination: ['', Validators.required],
        departureDate: ['', Validators.required],
        returnDate: ['', Validators.required],
        adults: [1, [Validators.required, Validators.min(1)]],
        children: [0, [Validators.required, Validators.min(0)]],
        infants: [0, [Validators.required, Validators.min(0)]],
      });
    }

    ngOnInit(): void {}

    onSubmit(): void {
      
      const trenData: TrenData = {
        origin: this.homeForm.value.origin,
        destination: this.homeForm.value.destination,
        departureDate: this.homeForm.value.departureDate,
        returnDate: this.homeForm.value.returnDate,
        adults: this.homeForm.value.adults,
        children: this.homeForm.value.children,
        infants: this.homeForm.value.infants
      };

      console.log(trenData);
      
      this.apiService.getTrainsOfers(trenData).subscribe(
        (res) => {
        },
        (error) => {
        }
      );
    }
  }