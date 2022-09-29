
import { Component, OnInit } from '@angular/core';
import { AccountService } from './account/account.service';
import { BasketService } from './basket/basket.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})

export class AppComponent implements OnInit {
  title = 'Skinet';


  constructor(private basketService: BasketService, private accountService: AccountService)
  {

  }

  ngOnInit(): void {
    this.loadBasket();
    this.loadCurrentUser();

  }

  loadCurrentUser () {
    const token = localStorage.getItem('token');

      this.accountService.loadCurrentUser(token).subscribe(() =>
      {
        console.log('Loaded user');
      }, error => {
        console.log(error);
      })
    
  }

  loadBasket() {
     //https://www.udemy.com/course/learn-to-build-an-e-commerce-app-with-net-core-and-angular/learn/lecture/18137750#questions
     const basketId = localStorage.getItem('basket_id');
     if (basketId) {
       this.basketService.getBasket(basketId).subscribe(() => {
         console.log('initialised basket');
       }, error => {
         console.log(error);
       })
     }
  }



}
