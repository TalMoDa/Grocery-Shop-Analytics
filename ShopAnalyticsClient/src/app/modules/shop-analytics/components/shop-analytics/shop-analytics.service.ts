import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import {ShopDto} from '../../../../../shop-analytics-service';


interface DateRange {
  startDate: Date;
  endDate: Date;
}

@Injectable({
  providedIn: 'root'
})


export class ShopAnalyticsService {

  private dateRangeFilter$ = new BehaviorSubject<DateRange>({startDate: new Date(), endDate: new Date()});
  private shopDto$ = new BehaviorSubject<ShopDto>({} as ShopDto);

  constructor() {
  }

  listenToDateRangeFilter() {
    return this.dateRangeFilter$.asObservable();
  }

  setDateRangeFilter(dateRange: DateRange) {
    this.dateRangeFilter$.next(dateRange);
  }

  listenToShopDto() {
    return this.shopDto$.asObservable();
  }

  setShopDto(shopDto: ShopDto) {
    this.shopDto$.next(shopDto);
  }
}
