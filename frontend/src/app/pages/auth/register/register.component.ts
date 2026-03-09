import { RegisterRequestBody } from './../../../core/models/auth.models';
import { Component, inject } from '@angular/core';
import { SimpleHeaderComponent } from "../../../shared/components/simple-header/simple-header.component";
import { FooterComponent } from "../../../shared/components/footer/footer.component";
import { FormGroup, FormControl, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../../core/services/auth.service';
@Component({
  selector: 'app-register',
  imports: [SimpleHeaderComponent, 
    FooterComponent, ReactiveFormsModule, CommonModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {

  registerForm= new FormGroup({
   
    name: new FormControl('',[Validators.required]),
    lastName: new FormControl('', [Validators.required]),
    email:new FormControl('',[Validators.required, Validators.email]),
    age:new FormControl('',[Validators.required]),
    password: new FormControl('',[
      Validators.required,
      Validators.minLength(8),
       Validators.pattern(/(?=.*[A-Z])(?=.*[a-z])(?=.*\d)/)
    ]),
    confirmPassword: new FormControl('',Validators.required),
    documentType: new FormControl('',[Validators.required]),
    documentNumber: new FormControl('',[Validators.required])
  })

private authService=inject(AuthService);

  register(){
    if(this.registerForm.invalid){
      this.registerForm.markAllAsTouched();
      return;
    }

    const password = this.registerForm.value.password;
  const confirm = this.registerForm.value.confirmPassword;

  if(password !== confirm){
    alert('Las contraseñas no coinciden');
    return;
  };

  const body:RegisterRequestBody ={
    firstName: this.registerForm.controls.name.value!,
    lastName: this.registerForm.controls.lastName.value!,
    email: this.registerForm.controls.email.value!,
    documentNumber: Number(this.registerForm.controls.documentNumber.value!),
    edad: Number(this.registerForm.controls.age.value!) ,
    password: this.registerForm.controls.password.value!,
    confirmPassword: this.registerForm.controls.confirmPassword.value!

  }

  this.authService.register(body).subscribe({
  next: (res) => {
    console.log('OK', res);
  },
  error: (err) => {
    console.error('ERROR', err);
  }
});


  }
  

}
