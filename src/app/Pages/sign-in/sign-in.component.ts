import * as passwordValidator from 'password-validator'
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { UserService } from 'src/app/Services/UserService/user.service';
import { UserData } from 'src/app/DTO/user-data';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css'],
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

    const user: UserData = {
      login: this.Login,
      email: '',
      username: '',
      password: this.Password,
      birthDate: new Date
    }

    this.service.login(user).subscribe({
      next: (res) => {
        sessionStorage.setItem('token', res.token)
        this.router.navigate(['/'])
      },
      error: (err) => {
        if (err.status === 400) {
          this.Errors = [...this.Errors, err.error]
          return;
        }

        console.log(err)
      },
    })
  }
}