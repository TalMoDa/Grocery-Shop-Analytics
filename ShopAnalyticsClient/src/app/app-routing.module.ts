import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', redirectTo: 'shop-analytics', pathMatch: 'full' },
  { path: 'shop-analytics', loadChildren: () => import('./modules/shop-analytics/shop-analytics.module').then(m => m.ShopAnalyticsModule) },
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
