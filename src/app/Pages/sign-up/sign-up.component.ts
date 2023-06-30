import * as passwordValidator from 'password-validator'
import * as EmailValidator from 'email-validator';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { UserService } from 'src/app/Services/UserService/user.service';
import { UserData } from 'src/app/DTO/user-data';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css'],
  standalone: true,
  imports: [FormsModule, CommonModule]
})
export class SignUpComponent {
  constructor(
    private service: UserService,
    private router: Router) { }

  Username: string = ''
  Password: string = ''
  RepeatedPassword: string = ''
  Email: string = ''
  BirthDate: Date = new Date()
  Agreed: boolean = false
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
    const passwordErrors = passwordResult as PasswordError[]
    const errors = passwordErrors.map((err) => err.message)

    this.Errors = [...this.Errors, ...errors]

    interface PasswordError {
      message: string
    }
  }

  SubmitForm = () => {
    this.Errors = []

    this.ValidatePassword()

    if (this.Password !== this.RepeatedPassword) {
      this.AddError("Both passwords must be equals")
    }

    if (!this.Agreed) {
      this.AddError("You must agree with to terms and conditions")
    }

    if (!EmailValidator.validate(this.Email)) {
      this.AddError("Invalid e-mail")
    }

    if (this.Errors.length > 0) {
      return
    }

    const user: UserData = {
      login: '',
      email: this.Email,
      username: this.Username,
      password: this.Password,
      birthDate: this.BirthDate
    }

    this.service.create(user).subscribe({
      next: (res) => {
        sessionStorage.setItem('token', res.token)
        this.router.navigate(['/'])
      },
      error: (err) => {
        if (err.status === 400) {
          this.Errors = [...this.Errors, ...err.error]
          return;
        }

        console.error(err)
      }
    })
  }
}