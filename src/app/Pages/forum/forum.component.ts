import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';

import { PostInfoComponent } from 'src/app/components/post-info/post-info.component';
import { ForumService } from 'src/app/services/forum/forum.service';
import { PostService } from 'src/app/services/post/post.service';
import verifyError from 'src/app/services/verify-error';
import { ForumData } from 'src/app/models/forum-data';
import { UserData } from 'src/app/models/user-data';
import { PostData } from 'src/app/models/post-data';

@Component({
  selector: 'app-forum',
  templateUrl: './forum.component.html',
  styleUrls: ['./forum.component.css'],
  standalone: true,
  imports: [CommonModule, PostInfoComponent]
})
export class ForumComponent {
  forum!: ForumData
  posts!: PostData[]

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private forumService: ForumService,
    private postService: PostService
  ) {
    const token = sessionStorage.getItem('token')

    if (token === null) {
      this.router.navigate(['/signin'])
      return
    }

    const userData: UserData = {
      token: token
    }

    this.forumService.get(userData, this.route.snapshot.paramMap.get('title')!).subscribe({
      next: (res) => {
        if (res === null) {
          this.router.navigate(['/newforums'])
        }

        this.forum = res.forum
        this.posts = res.posts
      },
      error: (err) => {
        verifyError(err, this.router)
      },
    })
  }
}