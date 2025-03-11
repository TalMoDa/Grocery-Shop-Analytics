import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShopAnalyticsFooterComponent } from './shop-analytics-footer.component';

describe('ShopAnalyticsFooterComponent', () => {
  let component: ShopAnalyticsFooterComponent;
  let fixture: ComponentFixture<ShopAnalyticsFooterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ShopAnalyticsFooterComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ShopAnalyticsFooterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
