import { Libro } from "./libro.model";



export interface CartItem {
  libro: Libro;
  codigoBarra: string;
}