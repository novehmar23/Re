export interface ButtonAction {
  text(element: any): string;
  color(element: any): "primary" | "accent" | "warn";
  onClick(element: any): void;
}