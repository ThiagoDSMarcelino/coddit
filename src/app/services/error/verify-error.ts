import { Router } from "@angular/router";
import { ErrorResponse } from "src/app/models/response/error-response";

const verifyError = (err: any, router: Router) => {
  if (err.status === 400) {
    let error = err.error as ErrorResponse
    
    if (error.reason === 'Invalid Token') {
      sessionStorage.removeItem('token')
      router.navigate(['/sign up'])
    }

    return error.messages;
  }

  console.error(err)
  return Array();
}

export default verifyError