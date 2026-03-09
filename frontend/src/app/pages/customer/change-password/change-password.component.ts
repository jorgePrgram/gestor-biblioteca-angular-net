import { Component, inject } from '@angular/core';
import { AuthService } from '../../../core/services/auth.service';
import { FormGroup, FormControl, Validators, ReactiveFormsModule } from '@angular/forms';
import { ChangePasswordRequestBody } from '../../../core/models/auth.models';
@Component({
  selector: 'app-change-password',
  imports: [ReactiveFormsModule],
  templateUrl: './change-password.component.html',
  styleUrl: './change-password.component.scss'
})
export class ChangePasswordComponent {

  private authService=inject(AuthService);

  formulario= new FormGroup({
    passwordOld:new FormControl('',[Validators.required]),
    passwordNew: new FormControl('', [Validators.required, Validators.minLength(8)])

  });
  guardarCambios(){

    if(this.formulario.invalid){
      this.formulario.markAllAsTouched();
      return;
    }

    const body:ChangePasswordRequestBody ={
      oldPassword : this.formulario.controls.passwordOld.value!,
      newPassword: this.formulario.controls.passwordNew.value!
    }
    
      
    return this.authService.changePassword(body).subscribe({
      next: res=>{
        console.log('Contraseña cambiada correctamente')
      } 
       ,error:err=>{
        console.log('Error al cambiar contraseña', err)
       }
  });
    

  }
}
