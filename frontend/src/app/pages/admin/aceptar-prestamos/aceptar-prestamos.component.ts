import { Component, inject } from '@angular/core';
import { PedidoService } from '../../../core/services/pedido.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';



@Component({
  selector: 'app-aceptar-prestamos',
  imports: [CommonModule, FormsModule],
  templateUrl: './aceptar-prestamos.component.html',
  styleUrl: './aceptar-prestamos.component.scss'
})
export class AceptarPrestamosComponent {

  pedidoService= inject(PedidoService);
  pedidoId!: number;
codigoBarra: string = '';
  
  confirmar(){
    if (!this.pedidoId || !this.codigoBarra) {
    alert('Ingresa el ID del pedido y el código de barras');
    return;
  }
  this.pedidoService.confirmarPrestamo(this.pedidoId, this.codigoBarra)
  .subscribe(res=>{
    if(res.success){
      alert("Préstamo confirmado");
      this.codigoBarra = '';
        this.pedidoId = undefined!;
    }
  });
}
}
