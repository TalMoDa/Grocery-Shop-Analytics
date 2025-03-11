import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {ShopAnalyticsComponent} from './components/shop-analytics/shop-analytics.component';

const routes: Routes = [
  { path: '', component: ShopAnalyticsComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ShopAnalyticsRoutingModule { }
