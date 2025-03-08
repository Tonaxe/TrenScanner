import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ApiService } from '../services/api.service';

@Component({
  standalone: false,
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;

  constructor(private fb: FormBuilder, private router: Router, private apiService: ApiService) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  ngOnInit(): void {}

  onSubmit(): void {
    if (this.loginForm.valid) {
      const userEmail = this.loginForm.value.email;
      console.log(userEmail)
      this.apiService.getUser(userEmail).subscribe(
        (res) => {
          if (res.message === 'true') {
            sessionStorage.setItem('token', res.token);
            this.router.navigate(['/home']);
          } else {
            console.log('El usuario no existe');
          }
        },
        (error) => {
          console.log('Hubo un error al verificar el usuario');
        }
      );
    }
  }
}