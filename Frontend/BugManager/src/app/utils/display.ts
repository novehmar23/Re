export class Display {

  public static id(value: any) {
    return value;
  }
  public static IsActiveAsResolve(value: boolean) {
    return value ? "Unresolved" : "Resolved"
  }

  public static NullableString(value: string) {
    return value == null ? "-" : value
  }

  public static CostPerHour(value: number) {
    return `$${value}/hr`
  }

  public static TotalCost(value: number) {
    return `$${value}`
  }
}