export interface User {
  id?: number;
  username: string;
  name: string;
  lastname: string;
  email: string;
  password: string;
  cost?: number;
}