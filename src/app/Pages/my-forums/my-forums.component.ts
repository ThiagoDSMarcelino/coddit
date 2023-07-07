import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { ForumInfoComponent } from 'src/app/components/forum-info/forum-info.component';
import { ForumService } from 'src/app/services/forum/forum.service';
import verifyError from 'src/app/services/verify-error';
import { ForumData } from 'src/app/models/forum-data';
import { UserData } from 'src/app/models/user-data';

@Component({
  selector: 'app-my-forums',
  templateUrl: './my-forums.component.html',
  styleUrls: ['./my-forums.component.css'],
  standalone: true,
  imports: [ForumInfoComponent, CommonModule]
})
export class MyForumsComponent {
  forums: ForumData[] = []

  constructor(
    private router: Router,
    private service: ForumService
  ) {
    const token = sessionStorage.getItem('token')

    if (token === null) {
      this.router.navigate(['/signin'])
      return
    }

    const tokenData: UserData = {
      token: token
    }

    this.service.getUserForums(tokenData).subscribe({
      next: (res) => {
        this.forums = res
      },
      error: (err) => {
        verifyError(err, this.router)
      },
    })
  }

  hasCommunities = () => this.forums.length > 0
}