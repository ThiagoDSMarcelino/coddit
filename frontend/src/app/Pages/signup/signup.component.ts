import { UserService } from 'src/app/Services/UserService/user.service';
import { FormsModule } from "@angular/forms";
import { Component, runInInjectionContext } from '@angular/core';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css'],
  standalone: true,
  imports: [FormsModule],
})
export class SignupComponent {

  constructor(private service: UserService) { }

  Username: string = "";
  Password: string = "";
  Email: string = "";
  BirthDate: Date = new Date;
  Agreed: boolean = false;

  SubmitForm() {
    if (!this.Agreed)
      return;

    let user = {
      Email: this.Email,
      Username: this.Username,
      Password: this.Password,
      BirthDate: this.BirthDate
    };
    
    this.service.create(user);
  }
}