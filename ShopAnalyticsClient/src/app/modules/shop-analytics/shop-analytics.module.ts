import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {ShopAnalyticsComponent} from './components/shop-analytics/shop-analytics.component';
import {
  ShopAnalyticsHeaderComponent
} from './components/shop-analytics/shop-analytics-header/shop-analytics-header.component';
import {
  ShopAnalyticsFooterComponent
} from './components/shop-analytics/shop-analytics-footer/shop-analytics-footer.component';
import {ShopAnalyticsRoutingModule} from './shop-analytics-routing.module';
import {GoogleChartsModule} from 'angular-google-charts';
import {ReactiveFormsModule} from '@angular/forms';


@NgModule({
  declarations: [
    ShopAnalyticsComponent,
    ShopAnalyticsHeaderComponent,
    ShopAnalyticsFooterComponent
  ],
  imports: [
    CommonModule,
    ShopAnalyticsRoutingModule,
    GoogleChartsModule,
    ReactiveFormsModule
  ],
  exports: [
    ShopAnalyticsComponent
  ]
})
export class ShopAnalyticsModule { }
