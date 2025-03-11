import {Component, OnDestroy, OnInit} from '@angular/core';
import {FormBuilder, FormGroup} from '@angular/forms';
import {BehaviorSubject, Subject, combineLatest, filter, finalize, switchMap, takeUntil, tap} from 'rxjs';
import {ChartType} from 'angular-google-charts';
import {ActivatedRoute, Router} from '@angular/router';
import {ShopAnalyticsService} from './shop-analytics.service';
import {AnalyticsService, DayAnalyticsDto, ShopDto, ShopsService} from '../../../../../shop-analytics-service';

@Component({
  selector: 'app-shop-analytics',
  standalone: false,
  templateUrl: './shop-analytics.component.html',
  styleUrl: './shop-analytics.component.scss'
})
export class ShopAnalyticsComponent implements OnInit, OnDestroy {
  shops: ShopDto[] = [];
  analytics: DayAnalyticsDto[] = [];
  filterForm: FormGroup;
  isLoading = false;

  // Chart properties
  chartData: any[] = [];
  chartColumns: string[] = ['Date', 'Income', 'Outcome', 'Revenue'];
  chartType = ChartType.LineChart;
  chartOptions: any;

  private destroy$ = new Subject<void>();
  private shopsLoaded$ = new BehaviorSubject<boolean>(false);

  // Default date range
  private readonly defaultFromDate = '2021-06-01';
  private readonly defaultToDate = '2021-12-31';

  constructor(
    private analyticsService: AnalyticsService,
    private shopService: ShopsService,
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private shopAnalyticsService: ShopAnalyticsService
  ) {
    this.filterForm = this.fb.group({
      shopId: [''],
      fromDate: [this.defaultFromDate],
      toDate: [this.defaultToDate]
    });

    this.setupChartOptions();
  }

  ngOnInit(): void {
    // Load shops first and signal when complete
    this.loadShops();

    // Setup a combined observable that waits for both shops to load and route params
    combineLatest([
      this.shopsLoaded$.pipe(filter(loaded => loaded)), // Only proceed when shops are loaded
      this.route.queryParams
    ])
      .pipe(
        takeUntil(this.destroy$),
        tap(([_, params]) => {
          // Extract params with defaults
          const shopId = params['shopId'] || '';
          const fromDate = params['fromDate'] || this.defaultFromDate;
          const toDate = params['toDate'] || this.defaultToDate;

          // Update form with URL values
          this.filterForm.patchValue({ shopId, fromDate, toDate }, { emitEvent: false });

          // If no shopId in URL but we have shops, use the first one and update URL
          if (!shopId && this.shops.length > 0) {
            const defaultShopId = this.shops[0].id;
            this.updateUrlWithoutReload(defaultShopId!, fromDate, toDate);
            this.filterForm.patchValue({ shopId: defaultShopId }, { emitEvent: false });
          }
        }),
        // Process shop selection after params are set
        switchMap(() => {
          const { shopId, fromDate, toDate } = this.filterForm.value;

          if (!shopId || this.shops.length === 0) {
            return []; // Return empty array if no valid shop
          }

          const selectedShop = this.shops.find(shop => shop.id === shopId);
          if (!selectedShop) {
            console.warn(`Shop with ID ${shopId} not found`);
            return []; // Return empty array if shop not found
          }

          // Update shop analytics service
          this.shopAnalyticsService.setShopDto(selectedShop);
          this.shopAnalyticsService.setDateRangeFilter({
            startDate: new Date(fromDate),
            endDate: new Date(toDate)
          });

          // Start loading and make sure we handle the loading state properly
          this.isLoading = true;
          return this.analyticsService.getAnalytics(shopId, fromDate, toDate)
            .pipe(
              // Always turn off loading when this observable completes
              finalize(() => {
                this.isLoading = false;
              })
            );
        })
      )
      .subscribe({
        next: (analytics) => {
          if (analytics && analytics.length) {
            this.analytics = analytics;
            this.updateChartData();
          }
        },
        error: (err) => {
          console.error('Error loading analytics:', err);
          // isLoading is handled by finalize now
        }
        // No complete handler needed since finalize handles it
      });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  loadShops(): void {
    this.shopService.getShops()
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: (shops) => {
          this.shops = shops;
          this.shopsLoaded$.next(true);
        },
        error: (err) => {
          console.error('Error loading shops:', err);
          this.shopsLoaded$.next(true); // Signal loaded even on error to not block the flow
        }
      });
  }

  loadAnalytics(): void {
    const { shopId, fromDate, toDate } = this.filterForm.value;

    if (!shopId || this.shops.length === 0) {
      return;
    }

    this.isLoading = true;

    this.analyticsService.getAnalytics(shopId, fromDate, toDate)
      .pipe(
        takeUntil(this.destroy$),
        finalize(() => {
          this.isLoading = false; // Always ensure loading is turned off
        })
      )
      .subscribe({
        next: (analytics) => {
          this.analytics = analytics;
          this.updateChartData();
        },
        error: (err) => {
          console.error('Error loading analytics:', err);
          // isLoading is handled by finalize now
        }
        // No complete handler needed since finalize handles it
      });
  }

  onFilter(): void {
    const { shopId, fromDate, toDate } = this.filterForm.value;

    if (!shopId || this.shops.length === 0) {
      return;
    }

    // Update URL params
    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: { shopId, fromDate, toDate },
      queryParamsHandling: 'merge',
    });

    // Find the selected shop
    const selectedShop = this.shops.find(shop => shop.id === shopId);
    if (selectedShop) {
      this.shopAnalyticsService.setShopDto(selectedShop);
      this.shopAnalyticsService.setDateRangeFilter({
        startDate: new Date(fromDate),
        endDate: new Date(toDate)
      });
      this.loadAnalytics();
    }
  }

  private updateChartData(): void {
    // Transform analytics data for chart
    this.chartData = this.analytics.map(day => [
      new Date(day.date || ''),
      Number(day.income),
      Number(day.outcome),
      Number(day.revenue)
    ]);
  }

  private setupChartOptions(): void {
    this.chartOptions = {
      title: 'Shop Analytics Overview',
      curveType: 'function',
      backgroundColor: {
        fill: 'transparent' // Removes harsh white background
      },
      chartArea: {
        width: '80%',
        height: '70%'
      },
      tooltip: {
        isHtml: true,
        textStyle: { fontSize: 14 }
      },
      legend: {
        position: 'top',
        alignment: 'center',
        textStyle: { fontSize: 14, color: '#555' }
      },
      hAxis: {
        title: 'Date',
        titleTextStyle: { fontSize: 16, bold: true, color: '#555' },
        textStyle: { fontSize: 12, color: '#666' },
        format: 'MMM d, y',
        gridlines: { color: '#e5e5e5' }
      },
      vAxis: {
        title: 'Amount',
        titleTextStyle: { fontSize: 16, bold: true, color: '#555' },
        textStyle: { fontSize: 12, color: '#666' },
        gridlines: { color: '#e5e5e5' },
        minValue: 0
      },
      series: {
        0: { color: '#ff4c4c', lineWidth: 3 }, // Income - Red
        1: { color: '#4c79ff', lineWidth: 3 }, // Outcome - Blue
        2: { color: '#2ecc71', lineWidth: 3 }  // Revenue - Green
      },
      animation: {
        startup: true,
        duration: 1000,
        easing: 'out'
      }
    };
  }

  /**
   * Updates URL parameters without triggering a navigation event
   */
  private updateUrlWithoutReload(shopId: string, fromDate: string, toDate: string): void {
    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: { shopId, fromDate, toDate },
      queryParamsHandling: 'merge',
      replaceUrl: true // Replaces the current URL instead of creating a new history entry
    });
  }
}
