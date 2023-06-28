import { UserService } from 'src/app/Services/UserService/user.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserData } from 'src/app/Services/UserService/user-data';

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

  SubmitForm = () => {
    const user: UserData = {
      login: this.Login,
      email: '',
      username: '',
      password: this.Password,
      birthDate: new Date
    }

    this.service.login(user).subscribe({
      next(value) {
        console.log(value)
      },
      error(err) {
        console.log(err.error)
      },
    })
  }
}