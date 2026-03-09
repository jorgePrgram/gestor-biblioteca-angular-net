import { CartService } from './../../../core/services/cart.service';
import { LibroService } from './../../../core/services/libro.service';
import { Libro } from './../../../core/models/libro.model';
import { Component, inject, OnInit } from '@angular/core';
import { HomeHeaderComponent } from "../../home/home-header/home-header.component";
import { ActivatedRoute, Router } from '@angular/router';
import { LibroCardComponent } from "../../../shared/components/libro-card/libro-card.component";
import { ToastrService } from 'ngx-toastr';
import { PedidoService } from '../../../core/services/pedido.service';
import { AsyncPipe } from '@angular/common';
import { FooterComponent } from "../../../shared/components/footer/footer.component";
import { AuthService } from '../../../core/services/auth.service';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { EjemplaresComponent } from '../ejemplares/ejemplares.component';


@Component({
  selector: 'app-book-detail',
  imports: [HomeHeaderComponent, LibroCardComponent, FooterComponent, MatDialogModule],
  templateUrl: './book-detail.component.html',
  styleUrl: './book-detail.component.scss'
})
export class BookDetailComponent implements OnInit {
  libro!: Libro;

  libroId='';
  libroService= inject(LibroService);
  router=inject(Router);
  activatedRouter=inject(ActivatedRoute);
  toastr = inject(ToastrService);
  cartService =inject(CartService);
  pedidoService=inject(PedidoService);
  librosPrestados: number[]=[];
  authService=inject(AuthService);
  dialog=inject(MatDialog);




  //cart: number[]=[];
  cart$ = this.cartService.cart$;
  codigoSeleccionado: string | null = null;

  ngOnInit(){
    this.libroId=this.activatedRouter.snapshot.params['id'];
    this.libroService.getLibroById(this.libroId)
    .subscribe((res)=>{
      this.libro=res.data;
    });

  

   this.cargarPrestados();

  }


  cargarPrestados(){
  this.pedidoService.getMisLibrosPrestados()
    .subscribe(res=>{
      this.librosPrestados = [...res.data];
    });
}


agregarAlCarrito() {

  if (!this.codigoSeleccionado) return;

  this.cartService.addItem({
    libro: this.libro,
    codigoBarra: this.codigoSeleccionado
  });

  this.toastr.success("Ejemplar agregado al carrito");

  this.codigoSeleccionado = null;
}


  yaPrestado(id: number): boolean{
  return this.librosPrestados.some(x=>Number(x)===Number(id));
  }

abrirEjemplares() {

  const dialogRef = this.dialog.open(EjemplaresComponent, {
    width: '600px',
    data: { libroId: this.libro.id }
  });

  dialogRef.afterClosed().subscribe((codigoBarraSeleccionado) => {

    if (!codigoBarraSeleccionado) return;

    // Guardamos el código seleccionado
    this.codigoSeleccionado = codigoBarraSeleccionado;

  });
}

}
