import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegistrationComponent } from './registration/registration.component';
import { ResultsComponent } from './results/results.component';
import { AdministracionComponent } from './administracion/administracion.component';
import { EditarViajeComponent } from './editar-viaje/editar-viaje.component';
import { adminGuard } from './auth.guard';


//las rutas definidas con sus verificaciones
export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegistrationComponent },
  { path: 'home', component: HomeComponent, canActivate: [adminGuard] },
  { path: 'results', component: ResultsComponent, canActivate: [adminGuard] },
  { path: 'administracion', component: AdministracionComponent, canActivate: [adminGuard] },
  { path: 'administracion/editar-viaje/:id', component: EditarViajeComponent, canActivate: [adminGuard] },
  { path: '', redirectTo: 'login', pathMatch: 'full' }
];
