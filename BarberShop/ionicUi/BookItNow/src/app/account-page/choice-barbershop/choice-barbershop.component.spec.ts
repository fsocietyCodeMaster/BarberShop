import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { ChoiceBarbershopComponent } from './choice-barbershop.component';

describe('ChoiceBarbershopComponent', () => {
  let component: ChoiceBarbershopComponent;
  let fixture: ComponentFixture<ChoiceBarbershopComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [ChoiceBarbershopComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(ChoiceBarbershopComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
