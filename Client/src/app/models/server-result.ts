export interface ServerResult<T> {
  data: T;
  message: string;
  success: boolean;
}

export interface ServerRequest<T> {
  data: T;
}


export interface TokenMessage {
  email: string;
  token: string;
  expiration: Date;
}
