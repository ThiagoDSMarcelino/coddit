import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { ForumInfoComponent } from 'src/app/components/forum-info/forum-info.component';
import { ForumService } from 'src/app/services/forum/forum.service';
import verifyError from 'src/app/services/error/verify-error';
import { ForumResponse } from 'src/app/models/response/forum-response';
import { UserResponse } from 'src/app/models/response/user-response';

@Component({
  selector: 'app-my-forums',
  templateUrl: './my-forums.component.html',
  styleUrls: ['./my-forums.component.css'],
  standalone: true,
  imports: [ForumInfoComponent, CommonModule]
})
export class MyForumsComponent {
  communities: ForumResponse[] = []
  
  constructor(private router: Router, private service: ForumService) {
    const token = sessionStorage.getItem('token')

    if (token === null) {
      this.router.navigate(['/signin'])
      return
    }

    const tokenData: UserResponse = {
      token: token
    }

    this.service.getNewForums(tokenData).subscribe({
      next: (res) => {
        this.communities = res
      },
      error: (err) => {
        verifyError(err, this.router)
      },
    })
  }

  hasCommunities = () => this.communities.length > 0
}