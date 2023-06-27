import { FormsModule } from "@angular/forms";
import { Component } from '@angular/core';

import { UserService } from 'src/app/Services/UserService/user.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css'],
  standalone: true,
  imports: [FormsModule],
})
export class SignUpComponent {

  constructor(private service: UserService) { }

  Username: string = "";
  Password: string = "";
  Email: string = "";
  BirthDate: Date = new Date;
  Agreed: boolean = false;

  SubmitForm() {
    if (!this.Agreed)
      return

    let user = {
      Email: this.Email,
      Username: this.Username,
      Password: this.Password,
      BirthDate: this.BirthDate
    }

    this.service.create(user).subscribe({
      error: (e) => console.log(e),
      next: (n) => console.log(n),
      complete: () => console.info('complete')
    })
  }
}