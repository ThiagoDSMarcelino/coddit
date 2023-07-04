import { Component } from '@angular/core';

@Component({
  selector: 'app-post-info',
  templateUrl: './post-info.component.html',
  styleUrls: ['./post-info.component.css'],
  standalone: true,
})
export class PostInfoComponent {
  isHidden = true
  buttonText = 'more'
  
  
  placeholder = 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed rhoncus sed diam in rhoncus. fringilla sapien porta nulla dignissim, et tristique nisl sollicitudin. Quisque gravida leo augue, quis dignissim libero interdum sit amet. Sed at nisl ac est'
  
  hiddenText = () => "test"
  mainText = () => "hidden test"

  ToggleText() {
    this.isHidden = !this.isHidden
    this.buttonText = this.isHidden ? 'more' : 'less'
  }
}