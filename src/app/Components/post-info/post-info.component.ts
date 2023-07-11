import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import dateFormat from 'dateformat';


import { PostService } from 'src/app/services/post/post.service';
import { CreateVoteData } from 'src/app/models/create-vote-data';
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
    private service: PostService,
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

  vote = (value: any) => {
    this.disabledButton = true
    
    const token = sessionStorage.getItem('token')

    if (token === null) {
      this.router.navigate(['/signin'])
      return
    }

    // const data: CreateVoteData = {
    //   token: token,
    //   vote: value == 'upvote',
    //   id: this.post.id
    // }

    // this.service.vote(data).subscribe({
    //   next: (value) => {
    //     console.log(value)
    //   },
    //   error: (err) => {
    //     verifyError(err, this.router)
    //   },
    // })

    this.disabledButton = false
  }
}