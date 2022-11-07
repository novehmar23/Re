import { HttpErrorResponse } from "@angular/common/http";
import { throwError } from "rxjs";

export class HttpErrorHandler {

  public static handleError(error: HttpErrorResponse) {
    if (error.status === 0)
      return throwError("Server is shut down")

    const possibleErrorCodes = [404, 400, 401, 500]
    if (possibleErrorCodes.includes(error.status))
      return throwError(error.error.responseMessage)

    return throwError("Server with problems");
  }
}