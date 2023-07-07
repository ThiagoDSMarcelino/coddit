import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

import { ForumService } from 'src/app/services/forum/forum.service';
import { CreateForumData } from 'src/app/models/create-forum-data';
import verifyError from 'src/app/services/verify-error';

@Component({
  selector: 'app-create-forum',
  templateUrl: './create-forum.component.html',
  styleUrls: ['./create-forum.component.css', '../../styles/form.css'],
  standalone: true,
  imports: [FormsModule, CommonModule]
})
export class CreateForumComponent {
  
  constructor(
    private router: Router,
    private service: ForumService) { }

  Title = ''
  Description = ''
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
      this.router.navigate(['/signin'])
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

    const forum: CreateForumData  = {
      token: token,
      title: this.Title,
      description: this.Description
    }

    this.service.create(forum).subscribe({
      next: () => {
        this.CloseModal()
      },
      error: (err) => {
        this.Errors = [...this.Errors, ...verifyError(err, this.router)]
      }
    });
    
    this.Title = ''
    this.Description = ''  
  }
}