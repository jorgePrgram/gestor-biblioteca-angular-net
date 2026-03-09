
import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { ApiResponse } from '../models/api-response.model';
import { EjemplarResponse, Libro } from '../models/libro.model';

@Injectable({
  providedIn: 'root'
})
export class LibroService {

  private baseUrl=environment.baseUrl;
  private http= inject(HttpClient);


  getLibroById(id:string){
    return this.http.get<ApiResponse<Libro>>(`${this.baseUrl}/libros/${id}`);
  }

  getLibros(genreId?: string){
      if (!genreId || genreId === '0') {
    // No hay genreId o es "0" → todos
    return this.http.get<ApiResponse<Libro[]>>(`${this.baseUrl}/libros`);
  } else {
    // genreId válido → filtrar por género
    return this.http.get<ApiResponse<Libro[]>>(`${this.baseUrl}/libros/genre/${genreId}`);
  }
  }


  getEjemplarDisponibleByBook(libroId: number){
    return this.http.get<ApiResponse<EjemplarResponse[]>>(`${this.baseUrl}/libros/${libroId}/ejemplares-disponibles`);
  }



}
