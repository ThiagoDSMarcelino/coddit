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
  Data = ''

  constructor(
    private router: Router,
    private service: ForumService) {
    const token = sessionStorage.getItem('token')

    if (token === null) {
      this.router.navigate(['/signin'])
      return
    }

    const tokenData: UserData = {
      token: token
    }

    this.service.getNewForums(tokenData).subscribe({
      next: (res) => {
        this.forums = res
      },
      error: (err) => {
        verifyError(err, this.router)
      },
    })
  }

  hasCommunities = () => this.forums.length > 0

  Search = () => {
    const token = sessionStorage.getItem('token')
    
    if (token === null) {
      this.router.navigate(['/signin'])
      return
    }
    
    if (this.Data === '') {
      return
    }
  }
}
