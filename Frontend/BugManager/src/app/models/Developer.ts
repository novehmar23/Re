import { User } from "./User";

export interface Developer extends User {
  cost: number;
  bugsResolved: number;
}