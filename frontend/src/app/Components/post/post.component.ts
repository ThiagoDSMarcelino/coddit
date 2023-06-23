import { Component } from '@angular/core';
import {MatCardModule} from '@angular/material/card';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css'],
  standalone: true,
  imports: [MatCardModule],
})
export class PostComponent {
  isHidden: boolean = true;
  ButtonText: string = "more";

  ToggleText() {
    this.isHidden = !this.isHidden;
    this.ButtonText = this.isHidden ? "more" : "less";
  }
}