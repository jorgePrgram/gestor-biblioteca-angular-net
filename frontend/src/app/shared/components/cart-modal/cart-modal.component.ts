import { CartService } from './../../../core/services/cart.service';
import { Component, EventEmitter, inject, Input, OnInit, Output } from '@angular/core';

import { Libro } from '../../../core/models/libro.model';
import { LibroService } from '../../../core/services/libro.service';
import { AuthService } from '../../../core/services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { PedidoService } from '../../../core/services/pedido.service';
import { PedidoRequest } from '../../../core/models/pedido.model';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CartItem } from '../../../core/models/cart-item.model';





@Component({
  selector: 'app-cart-modal',
  templateUrl: './cart-modal.component.html',
  imports: [FormsModule, CommonModule],
  styleUrl: './cart-modal.component.scss'
})
export class CartModalComponent implements OnInit {
  private cartService=inject(CartService);
  private libroService=inject(LibroService);
  private authService=inject(AuthService);
  private toastr=inject(ToastrService);
  private router=inject(Router);
  private pedidoService=inject(PedidoService);


 

  cartItems: CartItem[]=[];

  @Input() isOpen=false;
  @Output() close=new EventEmitter<void>();

  ngOnInit(){
   this.cartService.cart$.subscribe(items => {
  this.cartItems = items;
});
  }

  remove(codigoBarra: string){
    this.cartService.removeItem(codigoBarra);
  }
  clear(){
    this.cartService.clear();
  }

  solicitarPrestamo(){
    if(!this.authService.getIsLoggedIn()){
      this.toastr.warning('Debes iniciar sesion');
      this.router.navigateByUrl('/login');
      return;
    };
    if(this.authService.getRole()==='Adminsitrator'){
      this.toastr.warning('Admin no puede perdir prestamos');
      return
    }

    if(this.cartItems.length===0){
      this.toastr.warning('Carrito vacio');
      return;
    }

    const pedido: PedidoRequest={
      librosIds: this.cartItems.map(i => i.libro.id),
       codigosBarra: this.cartItems.map(i => i.codigoBarra)
    };

    this.pedidoService.createPedido(pedido).subscribe({
     next:()=>{
      this.toastr.success('Prestamo solicitado correctamente');
      this.cartService.clear();
      this.isOpen=false;
     },
     error: (err)=>{
      this.toastr.error('Error al solicitar el prestamo');
      console.log(err);
     }
    });

    this.toastr.success('Prestamo solicitado');
    this.cartService.clear();
    this.isOpen=false;
  }




}
