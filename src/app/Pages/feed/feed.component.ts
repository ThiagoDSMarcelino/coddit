import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { PostInfoComponent } from 'src/app/components/post-info/post-info.component';
import { PostService } from 'src/app/services/post/post.service';
import { PostData } from 'src/app/models/post-data';
import verifyError from 'src/app/services/verify-error';
import { UserData } from 'src/app/models/user-data';

@Component({
  selector: 'app-feed',
  templateUrl: './feed.component.html',
  styleUrls: ['./feed.component.css'],
  standalone: true,
  imports: [PostInfoComponent, CommonModule]
})
export class FeedComponent {
  posts: PostData[] = []

  constructor(
    private router: Router,
    private service: PostService
  ) {
    const token = sessionStorage.getItem('token')

    if (token === null) {
      this.router.navigate(['/signin'])
      return
    }

    const data: UserData = {
      token: token
    }

    this.service.getByUser(data, '').subscribe({
      next: (res) => {
        this.posts = res
      },
      error: (err) => {
        verifyError(err, this.router)
      },
    })
  }

  hasPosts = () => this.posts.length > 0
}