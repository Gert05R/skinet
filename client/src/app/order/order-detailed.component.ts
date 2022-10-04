import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';
import { IOrder } from '../shared/models/order';
import { OrderService } from './order.service';

@Component({
  selector: 'app-order-detailed',
  templateUrl: './order-detailed.component.html',
  styleUrls: ['./order-detailed.component.scss']
})
export class OrderDetailedComponent implements OnInit {
  order: IOrder;

  constructor(private route: ActivatedRoute,private breadcrumService: BreadcrumbService ,
    private orderService: OrderService) {
      this.breadcrumService.set('@OrderDetailed', '');
     }

  ngOnInit(): void {
    this.loadOrder();
  }

  loadOrder() {
    this.orderService.getOrderDetailed(+this.route.snapshot.paramMap.get('id')).
    subscribe((order: IOrder )=> {
      this.order = order;
      this.breadcrumService.set('@OrderDetailed', `Order# ${order.id} - ${order.status}`);
    }, error => {
      console.log(error);
    });
  }

}
