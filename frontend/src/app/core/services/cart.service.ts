import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { CartItem } from '../models/cart-item.model';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  private cartSubject= new BehaviorSubject<CartItem[]>(this.loadCart());

  cart$ = this.cartSubject.asObservable();


  private loadCart(): CartItem[]{
   return JSON.parse(localStorage.getItem('cart') || '[]');
  }

  private saveCart(cart: CartItem[]){
    localStorage.setItem('cart', JSON.stringify(cart));
  }



  getCart(): CartItem[] {
    return this.cartSubject.value;
  }

  addItem(item: CartItem){
    const current= this.cartSubject.value;
    const exists = current.some(x => x.codigoBarra === item.codigoBarra);
    if (!exists) {
      const updated = [...current, item];
      this.cartSubject.next(updated);
      this.saveCart(updated);
    }
  }

  removeItem(codigoBarra: string) {
    const updated = this.cartSubject.value.filter(x => x.codigoBarra !== codigoBarra);
    this.cartSubject.next(updated);
    this.saveCart(updated);
  }

  clear() {
    this.cartSubject.next([]);
    localStorage.removeItem('cart');
  }
}
