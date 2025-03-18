import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegistrationComponent } from './registration/registration.component';
import { ResultsComponent } from './results/results.component';
import { AdministracionComponent } from './administracion/administracion.component';
import { EditarViajeComponent } from './editar-viaje/editar-viaje.component';

export const routes: Routes = [
    { path: 'home', component: HomeComponent },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegistrationComponent },
    { path: 'results', component: ResultsComponent },
    { path: 'administracion', component: AdministracionComponent },
    { path: 'administracion/editar-viaje/:id', component: EditarViajeComponent },
    { path: '', redirectTo: 'login', pathMatch: 'full' }
];
