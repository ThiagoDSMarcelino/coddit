import * as passwordValidator from 'password-validator'
import * as EmailValidator from 'email-validator';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { UserService } from 'src/app/Services/UserService/user.service';
import verifyError from 'src/app/Services/ErrorService/verifyError';
import { UserData } from 'src/app/DTO/Data/user-data';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css'],
  standalone: true,
  imports: [FormsModule, CommonModule]
})
export class SignUpComponent {
  Username = ''
  Password = ''
  RepeatedPassword = ''
  Email = ''
  BirthDate = ''
  Agreed = false
  Errors: string[] = []
  Max: string
  Min: string

  constructor(
    private service: UserService,
    private router: Router) {
      this.Max = new Date().toISOString().substring(0, 10)
      
      const minDate = new Date()
      minDate.setFullYear(minDate.getFullYear() - 100)
      this.Min = minDate.toISOString().substring(0, 10)
    }

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

  ValidateAge = () => {
    const diff = Date.now() - new Date(this.BirthDate).getTime();
    const year = new Date(diff).getFullYear();

    if (Math.abs(year - 1970) < 18 || Number.isNaN(year)) {
      this.AddError("You must be over than 18 years old")
    }
  }

  SubmitForm = () => {
    this.Errors = []

    this.ValidatePassword()
    this.ValidateAge()
    
    if (this.Password !== this.RepeatedPassword) {
      this.AddError("Both passwords must be equals")
    }
    
    if (!EmailValidator.validate(this.Email)) {
      this.AddError("Invalid e-mail")
    }
    
    if (!this.Agreed) {
      this.AddError("You must agree with to terms and conditions")
    }

    if (this.Errors.length > 0) {
      return
    }

    const user: UserData = {
      login: '',
      email: this.Email,
      username: this.Username,
      password: this.Password,
      birthDate: new Date(this.BirthDate)
    }

    this.service.create(user).subscribe({
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