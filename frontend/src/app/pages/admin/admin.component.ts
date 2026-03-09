import { Component } from '@angular/core';
import { SimpleHeaderComponent } from "../../shared/components/simple-header/simple-header.component";
import { FooterComponent } from "../../shared/components/footer/footer.component";
import { HomeHeaderComponent } from "../home/home-header/home-header.component";
import { RouterOutlet, RouterLink } from '@angular/router';

@Component({
  selector: 'app-admin',
  imports: [FooterComponent, HomeHeaderComponent, RouterOutlet, RouterLink],
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.scss'
})
export class AdminComponent {

}
