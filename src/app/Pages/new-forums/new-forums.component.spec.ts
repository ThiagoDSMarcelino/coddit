import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewForumsComponent } from './new-forums.component';

describe('NewForumsComponent', () => {
  let component: NewForumsComponent;
  let fixture: ComponentFixture<NewForumsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [NewForumsComponent]
    });
    fixture = TestBed.createComponent(NewForumsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
