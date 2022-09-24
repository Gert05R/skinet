import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ShopComponent } from './shop.component';
import { PrductDetailsComponent } from './prduct-details/prduct-details.component';


const routes: Routes = [
  {path:'', component: ShopComponent},
  {path: ':id', component: PrductDetailsComponent},
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class ShopRoutingModule { }
