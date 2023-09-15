import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

import { PostService } from 'src/app/services/post/post.service';
import verifyError from 'src/app/services/verify-error';
import { CreatePostData } from 'src/app/models/create-post';
import { PostData } from 'src/app/models/post-data';

@Component({
  selector: 'app-create-post',
  templateUrl: './create-post.component.html',
  styleUrls: ['./create-post.component.css', '../../styles/form.css'],
  standalone: true,
  imports: [FormsModule, CommonModule]
})
export class CreatePostComponent {
  @Input() forumTitle!: string
  @Input() posts!: PostData[]
  title = ''
  content = ''
  errors: string[] = []

  constructor(
    private router: Router,
    private service: PostService
  ) { }


  hasError = () => this.errors.length > 0

  addError = (message: string) => {
    if (!this.errors.includes(message)) {
      this.errors.push(message)
    }
  }

  closeModal = () =>
    document.getElementById('close-modal')?.click()

  submitForm = () => {
    this.errors = []

    var token = sessionStorage.getItem('token')

    if (token === null) {
      this.router.navigate(['/signin'])
      this.closeModal()
      return
    }

    if (this.title === '') {
      this.addError('Post title cannot be null')
    }

    if (this.content === '') {
      this.addError('Post content cannot be null')
    }

    if (this.errors.length > 0) {
      return
    }

    const createPostData: CreatePostData = {
      token: token,
      forumTitle: this.forumTitle,
      title: this.title,
      content: this.content
    }

    this.service.create(createPostData).subscribe({
      next: (res) => {
        this.posts.unshift(res)
        this.closeModal()
      },
      error: (err) => {
        verifyError(err, this.router)
      }
    });

    this.title = ''
    this.content = ''  
  }
}