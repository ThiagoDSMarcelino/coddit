import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PostInfoComponent } from './post-info.component';

describe('PostComponent', () => {
  let component: PostInfoComponent;
  let fixture: ComponentFixture<PostInfoComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PostInfoComponent]
    });
    fixture = TestBed.createComponent(PostInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});