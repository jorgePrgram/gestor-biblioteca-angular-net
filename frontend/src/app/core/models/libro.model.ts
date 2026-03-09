export interface Libro{
      id: number;
  nombre: string;
  autor: string;
  isbn: string;
  imageUrl?: string; 
  stockDisponible: number;
  genreId: number;
  genreNombre: string;
  estadoDescripcion: string;
}

export interface EjemplarResponse{
  id: number,
  codigoBarra: string
}