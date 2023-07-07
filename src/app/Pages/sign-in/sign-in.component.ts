import * as passwordValidator from 'password-validator'
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { UserService } from 'src/app/services/user/user.service';
import verifyError from 'src/app/services/verify-error';
import { LoginUserData } from 'src/app/models/login-user-data';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css', '../../styles/form.css'],
  standalone: true,
  imports: [FormsModule, CommonModule]
})
export class SignInComponent {
  constructor(
    private service: UserService,
    private router: Router) { }

  Login: string = ''
  Password: string = ''
  Errors: string[] = []

  HasError = () => this.Errors.length > 0

  AddError = (message: string) => {
    if (!this.Errors.includes(message)) {
      this.Errors.push(message)
    }
  }

  ValidatePassword = () => {
    const schema = new passwordValidator();

    schema
      .is().min(8)
      .is().max(100)
      .has().uppercase()
      .has().lowercase()
      .has().digits(1)

    const passwordResult = schema.validate(this.Password, { details: true })
    const passwordErrors = passwordResult as object[]
    passwordErrors.forEach(err => {
      const passwordError = err as PasswordError
      this.AddError(passwordError.message)
    })

    interface PasswordError {
      message: string
    }
  }

  SubmitForm = () => {
    this.Errors = []

    this.ValidatePassword()
    
    if (this.Errors.length > 0) {
      return
    }

    const user: LoginUserData = {
      login: this.Login,
      password: this.Password,
    }

    this.service.login(user).subscribe({
      next: (res) => {
        sessionStorage.setItem('token', res.token)
        this.router.navigate(['/'])
      },
      error: (err) => {
        this.Errors = [...this.Errors, ...verifyError(err, this.router)]
      },
    })
  }
}