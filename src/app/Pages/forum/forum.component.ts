import { ActivatedRoute, Router } from '@angular/router';
import { Component } from '@angular/core';

import { ForumResponse } from 'src/app/models/response/forum-response';
import { UserResponse } from 'src/app/models/response/user-response';
import { ForumService } from 'src/app/services/forum/forum.service';
import { PostService } from 'src/app/services/post/post.service';
import verifyError from 'src/app/services/error/verify-error';

@Component({
  selector: 'app-forum',
  templateUrl: './forum.component.html',
  styleUrls: ['./forum.component.css'],
  standalone: true,
})
export class ForumComponent {
  forum!: ForumResponse
  posts!: string[]

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

    const userData: UserResponse = {
      token: token
    }

    this.forumService.get(userData, this.route.snapshot.paramMap.get('title')!).subscribe({
      next: (res) => {
        if (res === null) {
          this.router.navigate(['/newforums'])
        }

        this.forum = res
      },
      error: (err) => {
        verifyError(err, this.router)
      },
    })

    this.postService.getByForum(userData).subscribe({
      next: (res) => {
        console.log(res)
      },
      error: (err) => {
        verifyError(err, this.router)
      },
    })
  }
}