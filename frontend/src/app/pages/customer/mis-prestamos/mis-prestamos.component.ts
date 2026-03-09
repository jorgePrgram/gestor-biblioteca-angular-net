import { Component, inject, OnInit } from '@angular/core';
import { PedidoService } from '../../../core/services/pedido.service';
import { PedidoResponse } from '../../../core/models/pedido.model';
import { NgFor, NgIf } from '@angular/common';

@Component({
  selector: 'app-mis-prestamos',
  imports: [NgFor, NgIf],
  templateUrl: './mis-prestamos.component.html',
  styleUrl: './mis-prestamos.component.scss'
})
export class MisPrestamosComponent implements OnInit {

  private pedidoService=inject(PedidoService);

  pedidos: PedidoResponse[]=[];


  ngOnInit(): void {
    this.cargarPedidos();
  }

  cargarPedidos(){
    this.pedidoService.getPedidos().subscribe({
      next:(res)=>{
        if(res.success){
          this.pedidos= res.data;
        }
        
      },
      error: (err)=>{
        console.log('Error cargando pedidos', err)
      }
    })
  }


  devolverEjemplar(ejemplarId: string){
    console.log('Devolver', ejemplarId)
  }

  obtenerEstado(estado: number): string {
  switch (estado) {
    case 0:
      return 'Disponible';
    case 1:
      return 'Reservado';
    case 2:
      return 'Prestado';
    default:
      return 'Desconocido';
  }
}


}
