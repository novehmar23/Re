import { ColumnType } from "./columnTypes";

export interface Column {
  header: string;
  property: string;
  type: ColumnType;
  display(value: any): any
}