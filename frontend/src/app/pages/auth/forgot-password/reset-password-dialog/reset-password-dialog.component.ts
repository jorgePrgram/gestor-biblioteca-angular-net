import { Component, inject, Inject } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { AuthService } from '../../../../core/services/auth.service';
import { ResetPassword } from '../../../../core/models/auth.models';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-reset-password-dialog',
  imports: [FormsModule, MatFormFieldModule, MatInputModule],
  templateUrl: './reset-password-dialog.component.html',
  styleUrl: './reset-password-dialog.component.scss'
})
export class ResetPasswordDialogComponent {
 token: string = '';
  newPassword: string = '';
  confirmNewPassword: string= '';

  dialogRef = inject(MatDialogRef<ResetPasswordDialogComponent>);
  authService=inject(AuthService);
  data = inject(MAT_DIALOG_DATA);

   cerrar() {
    this.dialogRef.close();
  }

    confirmar() {
      if (this.newPassword !== this.confirmNewPassword) {
  alert('Las contraseñas no coinciden');
  return;
}
  const body: ResetPassword={
    email:this.data.email,
        token: this.token,
    newPassword: this.newPassword,
    confirmNewPassword: this.confirmNewPassword
  };

  this.authService.resetPassword(body).subscribe({
     next: () => {
        alert('Contraseña actualizada');
        this.dialogRef.close(true);
      },
      error: err => console.error(err)
    });
  }
    

}
