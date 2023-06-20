import { Component } from '@angular/core';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css'],
  standalone: true
})
export class PostComponent {
  isHidden: boolean = true;
  ButtonText: string = this.isHidden ? "more" : "less";
  ToggleText() {
    console.log(this.isHidden)
    this.isHidden = !this.isHidden;
  }
}