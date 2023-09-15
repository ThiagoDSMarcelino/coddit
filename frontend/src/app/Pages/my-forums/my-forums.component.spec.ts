import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyForumsComponent } from './my-forums.component';

describe('MyCommunitiesComponent', () => {
  let component: MyForumsComponent;
  let fixture: ComponentFixture<MyForumsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MyForumsComponent]
    });
    fixture = TestBed.createComponent(MyForumsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
