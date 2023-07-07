import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

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

  isHidden = true
  buttonText = 'more'

  numberOfCharacters = 80
  
  mainText = () => {
    if (!this.isHidden) {
      return this.post.content.substring(0, this.numberOfCharacters)
    }
    
    return this.post.content.substring(0, this.numberOfCharacters) + (this.hasHiddenText() ? '...' : '')
  }

  hiddenText = () => this.post.content.substring(this.numberOfCharacters)
  
  hasHiddenText = () => this.post.content.length > this.numberOfCharacters

  ToggleText() {
    this.isHidden = !this.isHidden
    this.buttonText = this.isHidden ? 'more' : 'less'
  }
}