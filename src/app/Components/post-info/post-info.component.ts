import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'app-post-info',
  templateUrl: './post-info.component.html',
  styleUrls: ['./post-info.component.css'],
  standalone: true,
  imports: [CommonModule]
})
export class PostInfoComponent {
  isHidden = true
  buttonText = 'more'


  placeholder = 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed rhoncus sed diam in rhoncus. fringilla sapien porta nulla dignissim, et tristique nisl sollicitudin. Quisque gravida leo augue, quis dignissim libero interdum sit amet. Sed at nisl ac est'

  numberOfCharacters = 100

  hiddenText = () => this.placeholder.substring(this.numberOfCharacters)
  mainText = () => this.placeholder.substring(0, this.numberOfCharacters) + (this.hasHiddenText() ? '...' : '')
  hasHiddenText = () => this.placeholder.length > 100

  ToggleText() {
    this.isHidden = !this.isHidden
    this.buttonText = this.isHidden ? 'more' : 'less'
  }
}