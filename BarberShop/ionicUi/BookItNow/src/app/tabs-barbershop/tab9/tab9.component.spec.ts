import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { Tab9Component } from './tab9.component';

describe('Tab9Component', () => {
  let component: Tab9Component;
  let fixture: ComponentFixture<Tab9Component>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [Tab9Component],
    }).compileComponents();

    fixture = TestBed.createComponent(Tab9Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
