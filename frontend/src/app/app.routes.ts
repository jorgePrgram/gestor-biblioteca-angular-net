import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/auth/login/login.component';
import { AdminComponent } from './pages/admin/admin.component';
import { CustomerComponent } from './pages/customer/customer.component';
import { RegisterComponent } from './pages/auth/register/register.component';
import { BookDetailComponent } from './pages/books/book-detail/book-detail.component';
import { ForgotPasswordComponent } from './pages/auth/forgot-password/forgot-password.component';
import { MisPrestamosComponent } from './pages/customer/mis-prestamos/mis-prestamos.component';
import { ChangePasswordComponent } from './pages/customer/change-password/change-password.component';
import { PedidosComponent } from './pages/admin/pedidos/pedidos.component';
import { AceptarPrestamosComponent } from './pages/admin/aceptar-prestamos/aceptar-prestamos.component';
import { DevolverEjemplarComponent } from './pages/admin/devolver-ejemplar/devolver-ejemplar.component';

export const routes: Routes = [
    {path:'',
        component: HomeComponent
    },
    {
        path:'login',
        component: LoginComponent
    },
    {
        path:'book-detail/:id',
        pathMatch:'full',
        component: BookDetailComponent
    },
    {
        path:'admin',
        component:AdminComponent,
        children:[
            { path: 'pedidos', component: PedidosComponent },
    { path: 'aceptar-prestamo', component: AceptarPrestamosComponent },
    { path: 'devolver-ejemplar', component: DevolverEjemplarComponent }
        ]
    },
    {
        path:'customer',
        component: CustomerComponent,
        children:[
            {
                path:'',
                redirectTo:'mis-prestamos',
                pathMatch:'full'
            },
            {
                path:'mis-prestamos',
                component:MisPrestamosComponent
            },
            {
                path:'change-password',
                component:ChangePasswordComponent
            }
        ]
    },
    {
        path:'register',
        component:RegisterComponent
    },
    {
        path:'forgot-password',
        component:ForgotPasswordComponent
    }





];
