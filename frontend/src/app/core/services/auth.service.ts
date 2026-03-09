import { inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from '../../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { ApiResponse } from '../models/api-response.model';
import { ChangePasswordRequestBody, Login, RegisterRequestBody, RequestTokenToResetPassword, ResetPassword } from '../models/auth.models';
import { jwtDecode } from 'jwt-decode';
import { tap } from 'rxjs/operators'; 


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private baseUrl=environment.baseUrl;
  private http=inject(HttpClient);
  private role='';
  private email='';
  private name='';
  private tokenExpiration= new Date();
  private isLoggedIn=false;

  router=inject(Router);


  getEmail(){
    return this.email;
  }
  getName(){
    return this.name;
  }
  getRole(){
    return this.role;
  }

  getIsLoggedIn(){
    return this.isLoggedIn;
  }

  login(email:string, password:string){
    return this.http.post<ApiResponse<Login>>(`${this.baseUrl}/Users/Login`,{
      username: email,
      password:password
    }).pipe(tap(res=>{
      if(res.success && res.data){
        localStorage.setItem('token',res.data.token);
       
        localStorage.setItem('tokenExpiration', res.data.expirationDate);


          const expirationDate = new Date(res.data.expirationDate);
           localStorage.setItem('tokenExpiration', expirationDate.toISOString());
        this.decodeToken();
      }
    }))
  }

  decodeToken(){
    const token=localStorage.getItem('token');
    const tokenExpiration=localStorage.getItem('tokenExpiration');

    if(!token || !tokenExpiration){
      console.log('❌ No hay token o expiración');
      return;
    } 

    const expirationDate= new Date(tokenExpiration);
console.log('tokenExpiration raw:', tokenExpiration);
console.log('expirationDate parsed:', expirationDate);
console.log('now:', new Date());
    if(new Date() > expirationDate){
      this.logout();
        console.log('❌ Token expirado');
      return;
    }

    const jwtDecoded=jwtDecode<any>(token);

    this.role=jwtDecoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
    this.email=jwtDecoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'];
    this.name = jwtDecoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];

    this.tokenExpiration=expirationDate;
    this.isLoggedIn=true;
console.log('NAME CLAIM:', this.name);
console.log(jwtDecoded);
  }

  logout(){
    this.role='';
    this.email='';
    this.name='';
    this.isLoggedIn=false;
    this.tokenExpiration= new Date();

    localStorage.removeItem('token');
    localStorage.removeItem('tokenExpiration');
    localStorage.removeItem('cart');

    this.router.navigateByUrl('/login');

  }

  register(body:RegisterRequestBody){
    return this.http.post(`${this.baseUrl}/Users/Register`,body);
  }


  changePassword(body:ChangePasswordRequestBody){
    return this.http.post(`${this.baseUrl}/Users/ChangePassword`,body);
  }

  requestTokenToResetPassword(body: RequestTokenToResetPassword){
    return this.http.post(`${this.baseUrl}/Users/RequestTokenToResetPassword`,body);
  }

  resetPassword(body:ResetPassword ){
    return this.http.post(`${this.baseUrl}/Users/ResetPassword`,body)
  }


}
