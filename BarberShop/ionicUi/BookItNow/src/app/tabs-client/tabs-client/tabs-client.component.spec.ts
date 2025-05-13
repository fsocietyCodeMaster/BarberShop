import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { TabsClientComponent } from './tabs-client.component';

describe('TabsClientComponent', () => {
  let component: TabsClientComponent;
  let fixture: ComponentFixture<TabsClientComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [TabsClientComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(TabsClientComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
