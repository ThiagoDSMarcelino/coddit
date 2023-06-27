import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';

import { PostComponent } from '../../Components/post/post.component';

@Component({
  selector: 'app-feed',
  templateUrl: './feed.component.html',
  styleUrls: ['./feed.component.css'],
  standalone: true,
  imports: [PostComponent, CommonModule]
})
export class FeedComponent {
  Posts = Array(5);
}