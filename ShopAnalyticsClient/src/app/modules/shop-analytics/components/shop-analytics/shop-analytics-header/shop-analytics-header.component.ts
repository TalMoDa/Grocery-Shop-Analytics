import {Component, OnDestroy, OnInit} from '@angular/core';
import {ShopAnalyticsService} from '../shop-analytics.service';
import {Subject, takeUntil} from 'rxjs';
import {ShopDto} from '../../../../../../shop-analytics-service';

@Component({
  selector: 'app-shop-analytics-header',
  standalone: false,
  templateUrl: './shop-analytics-header.component.html',
  styleUrl: './shop-analytics-header.component.scss'
})
export class ShopAnalyticsHeaderComponent implements OnInit , OnDestroy {
  private destroy$ = new Subject<void>();
  fromDate: Date = new Date();
  toDate: Date = new Date();
  shopDto: ShopDto = {} as ShopDto;


  constructor(private shopAnalyticsService: ShopAnalyticsService) { }

  ngOnInit(): void {
    this.listenToDateRangeFilter();
    this.listenToShopDto();
  }

  private listenToDateRangeFilter(): void {
    this.shopAnalyticsService.listenToDateRangeFilter()
      .pipe(takeUntil(this.destroy$))
      .subscribe(dateRange => {
        this.fromDate = dateRange.startDate;
        this.toDate = dateRange.endDate;
      });
  }

  private listenToShopDto(): void {
    this.shopAnalyticsService.listenToShopDto()
      .pipe(takeUntil(this.destroy$))
      .subscribe(shopDto => {
        this.shopDto = shopDto;
      });
  }

  ngOnDestroy(): void {
  }



}
