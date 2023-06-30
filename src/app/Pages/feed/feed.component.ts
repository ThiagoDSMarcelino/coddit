import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';

import { ForumInfoComponent } from 'src/app/Components/forum-info/forum-info.component';
import { CreateForumComponent } from 'src/app/Components/create-forum/create-forum.component';

@Component({
  selector: 'app-feed',
  templateUrl: './feed.component.html',
  styleUrls: ['./feed.component.css'],
  standalone: true,
  imports: [ForumInfoComponent, CreateForumComponent, CommonModule]
})
export class FeedComponent {
  Posts = Array(5)
  Sla: boolean = false

  Toggle = () => this.Sla = !this.Sla;
}