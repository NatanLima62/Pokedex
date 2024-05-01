export interface LoginViewModel {
    email: string;
    password: string;
}

export interface TokenViewModel {
    token: string;
}

export interface RegisterViewModel {
  name: string;
  email: string;
  password: string;
  image: File;
}

export interface RegisterResponseViewModel {
    id: number;
    name: string;
    email: string;
    picture: string;
    disabled: boolean;
}
