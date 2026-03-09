export interface ApiResponse<T>{
    data: T;
    success:boolean;
    errorMessage: string;
}


export interface BaseResponse{
  success: boolean;
  errorMessage?: string;
}