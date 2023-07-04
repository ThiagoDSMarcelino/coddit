import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { CreateForumComponent } from 'src/app/Components/create-forum/create-forum.component';
import { ForumInfoComponent } from 'src/app/Components/forum-info/forum-info.component';
import { ForumService } from 'src/app/Services/ForumService/forum.service';
import { UserResponse } from 'src/app/DTO/Response/user-response';
import verifyError from 'src/app/Services/ErrorService/verifyError';

@Component({
  selector: 'app-feed',
  templateUrl: './feed.component.html',
  styleUrls: ['./feed.component.css'],
  standalone: true,
  imports: [ForumInfoComponent, CreateForumComponent, CommonModule, FormsModule]
})
export class FeedComponent {
  
  constructor(
    private router: Router,
    private service: ForumService) { }
  
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
    this.service.get(tokenData, this.Data).subscribe({
      next: (value) => {
        console.log(value)
      },
      error: (err) => {
        verifyError(err, this.router)
      },
    })
  }
}