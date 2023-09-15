import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

import { ForumService } from 'src/app/services/forum/forum.service';
import { CreateForumData } from 'src/app/models/create-forum-data';
import verifyError from 'src/app/services/verify-error';
import { ForumData } from 'src/app/models/forum-data';

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
    private service: ForumService
  ) { }

  title = ''
  description = ''
  errors: string[] = []
  @Input() forums!: ForumData[]

  HasError = () => this.errors.length > 0

  AddError = (message: string) => {
    if (!this.errors.includes(message)) {
      this.errors.push(message)
    }
  }

  CloseModal = () =>
    document.getElementById('close-modal')?.click()

  SubmitForm = () => {
    this.errors = []

    const token = sessionStorage.getItem('token')

    if (token === null) {
      this.router.navigate(['/signin'])
      this.CloseModal()
      return
    }

    if (this.title === '') {
      this.AddError('Forum title cannot be null')
    }

    if (this.description === '') {
      this.AddError('Forum description cannot be null')
    }

    if (this.errors.length > 0) {
      return
    }

    const forum: CreateForumData = {
      token: token,
      title: this.title,
      description: this.description
    }

    this.service.create(forum).subscribe({
      next: (res) => {
        this.forums.push(res)
        this.CloseModal()
      },
      error: (err) => {
        this.errors = [...this.errors, ...verifyError(err, this.router)]
      }
    });

    this.title = ''
    this.description = ''
  }
}