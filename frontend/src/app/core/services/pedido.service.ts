import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { ApiResponse, BaseResponse } from '../models/api-response.model';
import { PedidoRequest, PedidoResponse } from '../models/pedido.model';

@Injectable({
  providedIn: 'root'
})
export class PedidoService {

    private http = inject(HttpClient);
    private baseUrl= environment.baseUrl; 

  createPedido(request: PedidoRequest){
    return this.http.post<ApiResponse<number>>(`${this.baseUrl}/pedidos`,
      request);
  }

  getMisLibrosPrestados(){
    return this.http.get<ApiResponse<number[]>>(`${this.baseUrl}/pedidos/libros-prestados`);
   
  }

  getPedidos(clienteId?: number){
    let params: any={};

    if(clienteId){
      params.clienteId=clienteId;
    }
    return this.http.get<ApiResponse<PedidoResponse[]>>(`${this.baseUrl}/pedidos`, {params})
  }


  confirmarPrestamo(pedidoId:number, codigoBarra:string){
  return this.http.put<BaseResponse>(
    `${this.baseUrl}/pedidos/${pedidoId}/confirmar-prestamo`,
    {},
    { params: { codigoBarra } }
  );
}


  devolverEjemplar(pedidoId:number, codigoBarra:string){
    return this.http.put<BaseResponse>(`
      ${this.baseUrl}/pedidos/${pedidoId}/libros/${codigoBarra}/devolver/`,{})
  }

}
