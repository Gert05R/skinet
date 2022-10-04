import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrderDetailedComponent } from './order-detailed.component';
import { RouterModule, Routes } from '@angular/router';
import { OrdersComponent } from './orders.component';

const routes: Routes = [
  {path:'', component: OrdersComponent},
  {path:':id', component: OrderDetailedComponent, data: {breadcrumb: {alias: 'OrderDetailed'}}},
]

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class OrderRoutingModule { }
