import { Component, Input } from '@angular/core';
import { Libro } from '../../../core/models/libro.model';

@Component({
  selector: 'app-libro-card',
  imports: [],
  templateUrl: './libro-card.component.html',
  styleUrl: './libro-card.component.scss'
})
export class LibroCardComponent {
  @Input() data!: Libro;

}
