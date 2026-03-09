import { Component } from '@angular/core';
import { FooterComponent } from "../../shared/components/footer/footer.component";
import { HomeHeaderComponent } from "../home/home-header/home-header.component";
import { RouterOutlet, RouterLink } from '@angular/router';
import { RouterLinkWithHref } from "@angular/router";

@Component({
  selector: 'app-customer',
  imports: [FooterComponent, HomeHeaderComponent, RouterOutlet, RouterLinkWithHref, RouterLink],
  templateUrl: './customer.component.html',
  styleUrl: './customer.component.scss'
})
export class CustomerComponent {

}
