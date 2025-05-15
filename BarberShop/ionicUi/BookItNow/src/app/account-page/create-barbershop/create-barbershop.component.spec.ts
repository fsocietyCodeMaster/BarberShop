import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { CreateBarbershopComponent } from './create-barbershop.component';

describe('CreateBarbershopComponent', () => {
  let component: CreateBarbershopComponent;
  let fixture: ComponentFixture<CreateBarbershopComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [CreateBarbershopComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(CreateBarbershopComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
