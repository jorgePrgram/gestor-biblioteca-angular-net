import { AuthService } from './core/services/auth.service';
import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'gestor-biblioteca';
  authService=inject(AuthService);


constructor() {
    this.authService.decodeToken();

  }

}
