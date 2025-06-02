import { ComponentFixture, TestBed } from '@angular/core/testing';
import { SalonBarbersPage } from './salon-barbers.page';

describe('SalonBarbersPage', () => {
  let component: SalonBarbersPage;
  let fixture: ComponentFixture<SalonBarbersPage>;

  beforeEach(() => {
    fixture = TestBed.createComponent(SalonBarbersPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
