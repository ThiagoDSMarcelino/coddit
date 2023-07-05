import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ForumResponse } from 'src/app/models/response/forum-response';

@Component({
  selector: 'app-forum-info',
  templateUrl: './forum-info.component.html',
  styleUrls: ['./forum-info.component.css'],
  standalone: true,
  imports: [CommonModule]
})
export class ForumInfoComponent {
  @Input() community!: ForumResponse
  @Input() isMyForumsPage!: boolean
  
  buttonText = () => this.community.isMember ? 'You already joined in this forum' : 'Join this forum'
}