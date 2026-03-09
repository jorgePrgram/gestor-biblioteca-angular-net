
import { Component, inject } from '@angular/core';
import { HomeHeaderComponent } from "./home-header/home-header.component";
import { FooterComponent } from "../../shared/components/footer/footer.component";
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { Genre } from '../../core/models/genre.model';
import { LibroCardComponent } from "../../shared/components/libro-card/libro-card.component";
import { Libro } from '../../core/models/libro.model';
import { GenreService } from '../../core/services/genre.service';
import { LibroService } from '../../core/services/libro.service';
import { RouterLink } from "@angular/router";

@Component({
  selector: 'app-home',
  imports: [HomeHeaderComponent, FooterComponent, ReactiveFormsModule, LibroCardComponent, RouterLink],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  currentGenre= new FormControl(0);
  genres: Genre[]=[];
  libros: Libro[]=[];
  librosTodos: Libro[] = [];
  searchText: string='';


  private genreService=inject(GenreService);
  private libroService=inject(LibroService);

  ngOnInit(): void{
    this.genreService.getAll().subscribe(res=>{
      this.genres=res.data;
    });

    this.loadLibros();

    this.currentGenre.valueChanges.subscribe(genreId=>{
      this.loadLibros(genreId?.toString());
    })
  }

  loadLibros(genreId?: string){
    this.libroService.getLibros(genreId).subscribe(res=>{
      this.libros= res.data;
       this.librosTodos = res.data;
    })
  }
  onSearch(value: string) {
  if (!value.trim()) {
    this.libros = this.librosTodos; // ← si está vacío muestra todos
    return;
  }
  this.libros = this.librosTodos.filter(l =>
    l.nombre.toLowerCase().includes(value.toLowerCase())
  );
}

}
