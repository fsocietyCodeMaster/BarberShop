import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { Tab5Component } from './tab5.component';

describe('Tab5Component', () => {
  let component: Tab5Component;
  let fixture: ComponentFixture<Tab5Component>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [Tab5Component],
    }).compileComponents();

    fixture = TestBed.createComponent(Tab5Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
