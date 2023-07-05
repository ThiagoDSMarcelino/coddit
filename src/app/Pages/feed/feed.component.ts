import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { CreateForumComponent } from 'src/app/components/create-forum/create-forum.component';
import { PostInfoComponent } from 'src/app/components/post-info/post-info.component';
import { UserResponse } from 'src/app/models/response/user-response';
import { PostService } from 'src/app/services/post/post.service';
import verifyError from 'src/app/services/error/verify-error';

@Component({
  selector: 'app-feed',
  templateUrl: './feed.component.html',
  styleUrls: ['./feed.component.css'],
  standalone: true,
  imports: [PostInfoComponent, CreateForumComponent, CommonModule, FormsModule]
})
export class FeedComponent {
  
  constructor(
    private router: Router,
    private service: PostService) { }
  
  Posts = Array(5)
  Data = ''

  Search = () => {
    const token = sessionStorage.getItem('token')
    
    if (token === null) {
      this.router.navigate(['/signin'])
      return
    }
    
    if (this.Data === "") {
      return
    }

    const tokenData: UserResponse = {
      token: token
    }
    this.service.getPostByUser(tokenData, this.Data).subscribe({
      next: (value) => {
        console.log(value)
      },
      error: (err) => {
        verifyError(err, this.router)
      },
    })
  }
}