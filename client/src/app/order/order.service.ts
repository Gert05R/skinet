import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IOrder, IOrderToCreate } from '../shared/models/order';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getOrdersForUser() {
    return this.http.get(this.baseUrl + 'orders');
  }

  getOrderDetailed(id:number) {
    return this.http.get<IOrder>(this.baseUrl + 'orders/' + id);
  }
}
