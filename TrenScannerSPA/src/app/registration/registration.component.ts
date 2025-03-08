import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ApiService } from '../services/api.service';
import { UserRegister } from '../models/userRegister.model';

@Component({
  standalone: false,
  selector: 'app-register',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  registerForm: FormGroup;

  constructor(private fb: FormBuilder, private router: Router, private apiService: ApiService) {
    this.registerForm = this.fb.group({
      name: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required, this.passwordMatchValidator.bind(this)]]
    });
  }

  ngOnInit(): void {}

  passwordMatchValidator(control: any) {
    if (this.registerForm && control.value !== this.registerForm.get('password')?.value) {
      return { 'passwordMismatch': true };
    }
    return null;
  }

  
  onSubmit(): void {

    const userRegister: UserRegister = {
            nombre : this.registerForm.value.name,
            correo : this.registerForm.value.email,
            contraseña : this.registerForm.value.password,
          };

    if (this.registerForm.valid) {
      this.apiService.postUserRegister(userRegister).subscribe(
        (res) => {
          this.router.navigate(['/login']);
        },
        (error) => {
          console.log('Form is invalid');
        }
      );
    }
  }
}