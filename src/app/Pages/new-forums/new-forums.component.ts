import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { CreateForumComponent } from 'src/app/components/create-forum/create-forum.component';
import { ForumInfoComponent } from 'src/app/components/forum-info/forum-info.component';
import { ForumService } from 'src/app/services/forum/forum.service';
import verifyError from 'src/app/services/verify-error';
import { ForumData } from 'src/app/models/forum-data';
import { UserData } from 'src/app/models/user-data';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-new-forums',
  templateUrl: './new-forums.component.html',
  styleUrls: ['./new-forums.component.css'],
  standalone: true,
  imports: [ForumInfoComponent, CreateForumComponent, CommonModule, FormsModule]
})
export class NewForumsComponent {
  forums: ForumData[] = []
  data = ''

  constructor(
    private router: Router,
    private service: ForumService
  ) {
    this.getPosts()
  }

  hasCommunities = () => this.forums.length > 0

  search = () => {
    if (this.data === '') {
      return
    }

    this.getPosts()
  }

  getPosts = () => {
    const token = sessionStorage.getItem('token')

    if (token === null) {
      this.router.navigate(['/signin'])
      return
    }

    const tokenData: UserData = {
      token: token
    }

    this.service.getAll(tokenData, this.data).subscribe({
      next: (res) => {
        this.forums = res
      },
      error: (err) => {
        verifyError(err, this.router)
      },
    })
  }
}
