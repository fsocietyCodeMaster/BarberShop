import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { Tab7Component } from './tab7.component';

describe('Tab7Component', () => {
  let component: Tab7Component;
  let fixture: ComponentFixture<Tab7Component>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [Tab7Component],
    }).compileComponents();

    fixture = TestBed.createComponent(Tab7Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
