import { Component, EventEmitter, inject, Input, input, output, Output } from '@angular/core';
import {FormControl }from'@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { AuthService } from '../../../core/services/auth.service';
import { RouterLink } from "@angular/router";
import { CartModalComponent } from "../../../shared/components/cart-modal/cart-modal.component";

@Component({
  selector: 'app-home-header',
  imports: [ReactiveFormsModule, RouterLink, CartModalComponent],
  templateUrl: './home-header.component.html',
  styleUrl: './home-header.component.scss'
})
export class HomeHeaderComponent {

  authService=inject(AuthService);
  @Input() showSearch:boolean = false;
  searchControl=new FormControl('');
  @Output() search = new EventEmitter<string>();


  @Output() clear= new EventEmitter<void>();
  isCartOpen = false;

toggleCart(){
  this.isCartOpen = !this.isCartOpen;
  console.log('click carrito', this.isCartOpen);
}

buscar() {
  this.search.emit(this.searchControl.value || '');
}

 

  borrar(){
    this.searchControl.setValue('');
  }
}
