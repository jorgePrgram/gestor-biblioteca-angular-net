export interface RegisterRequestBody {
  firstName: string;
  lastName: string;
  email: string;
  documentNumber: number;
  edad: number;
  password: string;
  confirmPassword: string;
}

export interface Login {
  token: string;
  expirationDate: string;
}

export interface ChangePasswordRequestBody {
  oldPassword: string;
  newPassword: string;
}

export interface RequestTokenToResetPassword {
  email: string;
}

export interface ResetPassword {
  email: string;
  token: string;
  newPassword: string;
  confirmNewPassword: string;
}
