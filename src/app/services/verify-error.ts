import { Router } from "@angular/router";

import { ErrorData } from "src/app/models/error-data";

const verifyError = (err: any, router: Router) => {
  if (err.status === 400) {
    let error = err.error as ErrorData
    
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