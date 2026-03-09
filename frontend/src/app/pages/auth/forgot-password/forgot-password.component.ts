import { Component, inject, Inject } from '@angular/core';
import { FooterComponent } from "../../../shared/components/footer/footer.component";
import { SimpleHeaderComponent } from "../../../shared/components/simple-header/simple-header.component";
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { ResetPasswordDialogComponent } from './reset-password-dialog/reset-password-dialog.component';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { AuthService } from '../../../core/services/auth.service';
import { RequestTokenToResetPassword } from '../../../core/models/auth.models';


@Component({
  selector: 'app-forgot-password',
  imports: [FooterComponent, SimpleHeaderComponent, MatFormFieldModule, MatInputModule, FormsModule, RouterLink, MatDialogModule],
  templateUrl: './forgot-password.component.html',
  styleUrl: './forgot-password.component.scss'
})
export class ForgotPasswordComponent {
private dialog = inject(MatDialog);
authService=inject(AuthService);
email: string = '';

abrirDialog() {
 console.log("CLICK FUNCIONA");
  const body: RequestTokenToResetPassword ={
    email:this.email
  }
    // 1️⃣ Primero llamas al endpoint que genera el token
  this.authService.requestTokenToResetPassword(body).subscribe({
    next: () => {

      console.log("Token generado y enviado al correo");

      // 2️⃣ Si todo salió bien → abres el dialog
      const dialogRef = this.dialog.open(ResetPasswordDialogComponent, {
        width: '400px',
        data: { email: this.email }
      });

      // 3️⃣ Cuando el dialog se cierre
      dialogRef.afterClosed().subscribe(result => {

        if (result) {
          console.log(result.token);
          console.log(result.newPassword);

          // 🔥 Aquí llamarás el endpoint de cambiar contraseña
        }

      });

    },
    error: (err) => {
      console.error("Error al generar token", err);
    }
  });
}
}
