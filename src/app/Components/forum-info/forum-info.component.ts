import { Component, Input } from '@angular/core';
import { ForumResponse } from 'src/app/models/response/forum-response';

@Component({
  selector: 'app-forum-info',
  templateUrl: './forum-info.component.html',
  styleUrls: ['./forum-info.component.css'],
  standalone: true
})
export class ForumInfoComponent {
  @Input() community!: ForumResponse
}