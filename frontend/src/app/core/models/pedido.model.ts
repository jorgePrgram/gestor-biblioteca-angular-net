export interface Pedido{

}

export interface PedidoRequest{

    librosIds: number[];
    codigosBarra: string[];
}


export interface PedidoResponse{
  id: number,
  fechaPedido: string,
  clienteId: number,
  clienteNombre:string,
  estadoPedido:string,
  ejemplares: PedidoEjemplarResponse[]
}


export interface PedidoEjemplarResponse{
  ejemplarId: string,
  codigoBarra:string,
  nombreLibro: string,
  estado: number,
   fechaReserva: string;
  fechaPrestamo?: string;
  fechaDevolucion?: string;
  
}


