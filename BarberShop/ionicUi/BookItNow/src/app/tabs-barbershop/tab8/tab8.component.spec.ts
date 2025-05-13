import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { Tab8Component } from './tab8.component';

describe('Tab8Component', () => {
  let component: Tab8Component;
  let fixture: ComponentFixture<Tab8Component>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [Tab8Component],
    }).compileComponents();

    fixture = TestBed.createComponent(Tab8Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
