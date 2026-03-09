import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { Genre } from '../models/genre.model';
import { ApiResponse } from '../models/api-response.model';

@Injectable({
  providedIn: 'root'
})
export class GenreService {

  private http= inject(HttpClient);
  private baseUrl= environment.baseUrl;

  getAll(){
    return this.http.get<ApiResponse<Genre[]>>(`${this.baseUrl}/genres`);
  }
}
