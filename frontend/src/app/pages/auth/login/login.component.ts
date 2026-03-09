import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormControl, FormGroup, Validators } from '@angular/forms';

import { SimpleHeaderComponent } from "../../../shared/components/simple-header/simple-header.component";
import { FooterComponent } from "../../../shared/components/footer/footer.component";
import { AuthService } from '../../../core/services/auth.service';
import { Router, RouterLink } from '@angular/router';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-login',
   imports: [
    CommonModule,
    ReactiveFormsModule,
    SimpleHeaderComponent,
    FooterComponent,
    RouterLink
],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  authService=inject(AuthService);
  formulario=new FormGroup({
    email: new FormControl('', [
    Validators.required,
    Validators.pattern(/^[a-zA-Z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$/)
  ]),
    password: new FormControl('',[Validators.required, Validators.minLength(8)])
  });
  router=inject(Router);
   toastr = inject(ToastrService);
  

  login(){
    const email=this.formulario.controls.email.value!;
    const password=this.formulario.controls.password.value!;
    console.log('email:',email);
    console.log('password:', password);

    this.authService.login(email,password)
    .subscribe((res)=>{

      this.authService.decodeToken();
       this.toastr.success('Login exitoso', 'Bienvenido');
      this.router.navigateByUrl('/');
    });
    console.log('hola mundo');
  }
}
