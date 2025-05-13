import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { TabsBarbershopComponent } from './tabs-barbershop.component';

describe('TabsBarbershopComponent', () => {
  let component: TabsBarbershopComponent;
  let fixture: ComponentFixture<TabsBarbershopComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [TabsBarbershopComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(TabsBarbershopComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
