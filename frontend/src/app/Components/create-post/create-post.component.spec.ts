import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreatePostComponent } from './create-post.component';

describe('CreatePostComponent', () => {
  let component: CreatePostComponent;
  let fixture: ComponentFixture<CreatePostComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CreatePostComponent]
    });
    fixture = TestBed.createComponent(CreatePostComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});