import { Component, inject, OnInit } from '@angular/core';
import { LibroService } from '../../../core/services/libro.service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { EjemplarResponse } from '../../../core/models/libro.model';

@Component({
  selector: 'app-ejemplares',
  imports: [],
  templateUrl: './ejemplares.component.html',
  styleUrl: './ejemplares.component.scss'
})
export class EjemplaresComponent implements OnInit {


  ejemplarService= inject(LibroService);
  data= inject(MAT_DIALOG_DATA);
  dialogRef=inject(MatDialogRef<EjemplaresComponent>);

  libroId!: number;
  ejemplares: EjemplarResponse[] = [];


  ngOnInit(): void {
    this.libroId=this.data.libroId;
    this.loadEjemplares();
  }


  loadEjemplares(){
    this.ejemplarService.getEjemplarDisponibleByBook(this.libroId)
    .subscribe(res=>{
      this.ejemplares= res.data;
    })
  }

  cerrar(){
    this.dialogRef.close();
  }

  seleccionarEjemplar(codigoBarra: string){
  this.dialogRef.close(codigoBarra);
}

}
