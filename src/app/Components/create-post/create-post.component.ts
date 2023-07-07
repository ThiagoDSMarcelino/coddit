import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

import { PostService } from 'src/app/services/post/post.service';
import verifyError from 'src/app/services/verify-error';
import { CreatePostData } from 'src/app/models/create-post';

@Component({
  selector: 'app-create-post',
  templateUrl: './create-post.component.html',
  styleUrls: ['./create-post.component.css', '../../styles/form.css'],
  standalone: true,
  imports: [FormsModule, CommonModule]
})
export class CreatePostComponent {
  @Input() forumTitle!: string
  title = ''
  content = ''
  Errors: string[] = []
  
  constructor(
    private router: Router,
    private service: PostService) { }

  
  hasError = () => this.Errors.length > 0

  addError = (message: string) => {
    if (!this.Errors.includes(message)) {
      this.Errors.push(message)
    }
  }

  closeModal = () => 
    document.getElementById('close-modal')?.click()

  submitForm = () => {
    this.Errors = []
    
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
    
    if (this.Errors.length > 0) {
      return
    }

    const createPostData: CreatePostData  = {
      token: token,
      forumTile: this.forumTitle,
      title: this.title,
      content: this.content
    }

    this.service.create(createPostData).subscribe({
      next: () => {
        this.closeModal()
      },
      error: (err) => {
        verifyError(err, this.router)
      }
    });
    
    // this.title = ''
    // this.content = ''  
  }
}