import { Router } from "@angular/router";

const verifyError = (err: any, router: Router) => {
  if (err.status === 401) {
    sessionStorage.removeItem('token')
    router.navigate(['/signup'])
  }

  if (err.status === 400) {
    let error = err.error as string[]
    
    return error;
  }

  return [];
}

export default verifyError