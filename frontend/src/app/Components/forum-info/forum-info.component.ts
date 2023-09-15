import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ForumData } from 'src/app/models/forum-data';


@Component({
  selector: 'app-forum-info',
  templateUrl: './forum-info.component.html',
  styleUrls: ['./forum-info.component.css'],
  standalone: true,
  imports: [CommonModule]
})
export class ForumInfoComponent {
  @Input() forum!: ForumData
  @Input() isMyForumsPage!: boolean

  buttonText = () => this.forum.isMember ? 'You already joined in this forum' : 'Join this forum'
}