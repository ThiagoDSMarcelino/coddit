import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';

import { PostInfoComponent } from 'src/app/components/post-info/post-info.component';

@Component({
  selector: 'app-feed',
  templateUrl: './feed.component.html',
  styleUrls: ['./feed.component.css'],
  standalone: true,
  imports: [PostInfoComponent, CommonModule]
})
export class FeedComponent {
  Posts = Array(5)
}