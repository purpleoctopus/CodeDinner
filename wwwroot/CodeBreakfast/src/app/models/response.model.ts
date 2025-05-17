export interface ApiResponse<T> {
  success: boolean;
  message: string | null;
  data: T | null;
  errors: ResponseError[] | null;
}

export enum ResponseError {
  InvalidCredentials = 1,
}
