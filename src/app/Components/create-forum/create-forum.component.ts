import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

import { ForumService } from 'src/app/Services/ForumService/forum.service';
import { ForumData } from 'src/app/DTO/forum-data';

@Component({
  selector: 'app-create-forum',
  templateUrl: './create-forum.component.html',
  styleUrls: ['./create-forum.component.css'],
  standalone: true,
  imports: [FormsModule, CommonModule]
})
export class CreateForumComponent {
  
  constructor(
    private router: Router,
    private service: ForumService) { }

  Title: string = ''
  Description: string = ''
  Errors: string[] = []
  
  HasError = () => this.Errors.length > 0

  AddError = (message: string) => {
    if (!this.Errors.includes(message)) {
      this.Errors.push(message)
    }
  }

  CloseModal = () => 
    document.getElementById('close-modal')?.click()

  SubmitForm = () => {
    this.Errors = []
    
    var token = sessionStorage.getItem('token')
    
    if (token === null) {
      this.router.navigate(['/signup'])
      this.CloseModal()
      return
    }

    if (this.Title === '') {
      this.AddError('Forum title cannot be null')
    }

    if (this.Description === '') {
      this.AddError('Forum description cannot be null')
    }
    
    if (this.Errors.length > 0) {
      return
    }

    const forum: ForumData  = {
      UserToken: token,
      Title: this.Title,
      Description: this.Description
    }

    this.service.create(forum).subscribe({
      next: () => {
        this.CloseModal()
      },
      error: (err) => {
        if (err.status === 400) {
          this.Errors = [...this.Errors, ...err.error]
          this.Title = ''
          this.Description = ''
          return;
        }

        console.log(err)
      }
    });    
  }
}