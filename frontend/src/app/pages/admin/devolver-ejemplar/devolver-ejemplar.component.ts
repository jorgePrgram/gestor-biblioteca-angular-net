import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { PedidoService } from '../../../core/services/pedido.service';


@Component({
  selector: 'app-devolver-ejemplar',
  imports: [CommonModule, FormsModule],
  templateUrl: './devolver-ejemplar.component.html',
  styleUrl: './devolver-ejemplar.component.scss'
})
export class DevolverEjemplarComponent {
pedidoService = inject(PedidoService);

  pedidoId!: number;
  codigoBarra: string = '';
  mensaje: string = '';
  error: string = '';

  devolver() {
    if (!this.pedidoId || !this.codigoBarra) {
      this.error = 'Ingresa el ID del pedido y el código de barras';
      return;
    }

    this.mensaje = '';
    this.error = '';

    this.pedidoService.devolverEjemplar(this.pedidoId, this.codigoBarra)
      .subscribe({
        next: (res) => {
          if (res.success) {
            this.mensaje = 'Ejemplar devuelto correctamente';
            this.codigoBarra = '';
            this.pedidoId = undefined!;
          } else {
            this.error = res.errorMessage ?? 'Error al devolver el ejemplar';
          }
        },
        error: (err) => {
          this.error = err.error?.errorMessage ?? 'Error al devolver el ejemplar';
        }
      });
  }
}
