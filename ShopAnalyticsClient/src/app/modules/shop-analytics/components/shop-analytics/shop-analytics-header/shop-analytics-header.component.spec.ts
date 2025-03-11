import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShopAnalyticsHeaderComponent } from './shop-analytics-header.component';

describe('ShopAnalyticsHeaderComponent', () => {
  let component: ShopAnalyticsHeaderComponent;
  let fixture: ComponentFixture<ShopAnalyticsHeaderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ShopAnalyticsHeaderComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ShopAnalyticsHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
