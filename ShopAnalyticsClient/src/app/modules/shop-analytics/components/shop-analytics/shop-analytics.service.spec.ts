import { TestBed } from '@angular/core/testing';

import { ShopAnalyticsService } from './shop-analytics.service';

describe('ShopAnalyticsService', () => {
  let service: ShopAnalyticsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ShopAnalyticsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
