import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ForumInfoComponent } from './forum-info.component';

describe('ForumInfoComponent', () => {
  let component: ForumInfoComponent;
  let fixture: ComponentFixture<ForumInfoComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ForumInfoComponent]
    });
    fixture = TestBed.createComponent(ForumInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
