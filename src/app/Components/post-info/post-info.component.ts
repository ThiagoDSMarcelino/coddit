import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import dateFormat from 'dateformat';


import { VoteService } from 'src/app/services/vote/vote.service';
import { VoteData } from 'src/app/models/vote-data';
import verifyError from 'src/app/services/verify-error';
import { PostData } from 'src/app/models/post-data';

@Component({
  selector: 'app-post-info',
  templateUrl: './post-info.component.html',
  styleUrls: ['./post-info.component.css'],
  standalone: true,
  imports: [CommonModule]
})
export class PostInfoComponent {
  @Input() post!: PostData

  disabledButton = false
  isHidden = true
  buttonText = 'more'

  numberOfCharacters = 80

  constructor(
    private service: VoteService,
    private router: Router) { }

  mainText = () => {
    if (!this.isHidden) {
      return this.post.content.substring(0, this.numberOfCharacters)
    }

    return this.post.content.substring(0, this.numberOfCharacters) + (this.hasHiddenText() ? '...' : '')
  }

  hiddenText = () =>
    this.post.content.substring(this.numberOfCharacters)

  hasHiddenText = () =>
    this.post.content.length > this.numberOfCharacters

  ToggleText = () => {
    this.isHidden = !this.isHidden
    this.buttonText = this.isHidden ? 'more' : 'less'
  }

  getTime = () =>
    dateFormat(this.post.createAt, 'hh:MM TT - dd/mm/yyyy')
  
  hasUpVote = () =>
    this.post.vote ? "solid" : "regular"

  hasDownVote = () =>
    !this.post.vote && this.post.vote !== null ? "solid" : "regular"

  vote = (value: boolean) => {
    this.disabledButton = true
    
    const token = sessionStorage.getItem('token')

    if (token === null) {
      this.router.navigate(['/signin'])
      return
    }

    const data: VoteData = {
      token: token,
      vote: value,
      postId: this.post.id
    }

    if (this.post.vote === null) {
      this.service.create(data).subscribe({
        next: () => {
          this.post.vote = value
        },
        error: (err) => {
          verifyError(err, this.router)
        },
      })
    } else if (this.post.vote === value) {
      this.service.delete(data).subscribe({
        next: () => {
          this.post.vote = null
        },
        error: (err) => {
          verifyError(err, this.router)
        },
      })
    } else {
      this.service.update(data).subscribe({
        next: () => {
          this.post.vote = value
        },
        error: (err) => {
          verifyError(err, this.router)
        },
      })
    }
    
    

    this.disabledButton = false
  }
}