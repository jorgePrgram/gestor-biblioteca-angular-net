import { Component, inject } from '@angular/core';
import { PedidoResponse } from '../../../core/models/pedido.model';
import { PedidoService } from '../../../core/services/pedido.service';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-pedidos',
  imports: [FormsModule],
  templateUrl: './pedidos.component.html',
  styleUrl: './pedidos.component.scss'
})
export class PedidosComponent {

pedidoService=inject(PedidoService);  
router=inject(Router);
pedidos: PedidoResponse[] = [];
clienteIdFiltro?: number;
pedidoSeleccionado?: PedidoResponse;

ngOnInit(){
  this.cargarPedidos();
}

cargarPedidos(){
  this.pedidoService.getPedidos().subscribe(res=>{
    if(res.success){
      this.pedidos = res.data;
    }
  });
}

buscar(){
  this.pedidoService.getPedidos(this.clienteIdFiltro).subscribe(res=>{
    if(res.success){
      this.pedidos = res.data;
    }
  });
}

verDetalle(pedido: PedidoResponse) {
  this.pedidoSeleccionado =pedido;
}
getEstadoEjemplar(estado: number): string {
  switch(estado) {
    case 0: return 'Disponible';
    case 1: return 'Reservado';
    case 2: return 'Prestado';
    default: return 'Desconocido';
  }
}
  
}
