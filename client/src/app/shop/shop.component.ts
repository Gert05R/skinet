import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IBrand } from '../shared/models/brand';
import { IProduct } from '../shared/models/product';
import { IType } from '../shared/models/productType';
import { ShopService } from './shop.service';
import { shopParams} from '../shared/models/shopParams'


@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  @ViewChild('search', {static: false}) searchTerm: ElementRef;
  products: IProduct[];
  brands: IBrand[];
  types: IType[];
  shopParams: shopParams;
  totalCount: number;
  sortOptions = [
    {name: 'Alphabetical', value: 'name'},
    {name: 'Price low to High', value: 'priceAsc',},
    {name: 'Price: High to low', value: 'priceDesc'}
  ];



  constructor(private shopService: ShopService) {
    this.shopParams = this.shopService.getShopParams();
   }

  ngOnInit(): void {

    this.getProducts(true);
    this.getBrands();
    this.getTypes();

  }

  //we are only receiving the IProduct type(data) from IPagination into procuts array, not the other fields from IPagination
  getProducts(useCache = false) {
    this.shopService.getProducts(useCache).subscribe((response => {
      this.products = response.data;
      this.totalCount = response.count;
      console.log("Productslogged" + this.products);
    }), error => {
      console.log(error);
    })
  }

  getBrands() {
    this.shopService.getBrands().subscribe((response => {
      //reponse is an array of brandobjects, we create another object to add to this array
      //spreads all the repsonses in the array, but ads an object ALL in front
      this.brands = [{id: 0, name: 'All'}, ...response];
    }), error => {
      console.log(error);
    })
  }

  getTypes() {
    this.shopService.getTypes().subscribe((response => {
      this.types = [{id: 0, name: 'All'}, ...response];
    }), error => {
      console.log(error);
    })
  }

  onBrandSelected(brandId: number) {
    const params = this.shopService.getShopParams();
    params.brandId = brandId;
    params.pageNumber = 1;
    this.shopService.setShopParams(params);
    this.getProducts();
  }

  onTypeSelected(typeId: number) {
    const params = this.shopService.getShopParams();
    params.typeId = typeId;
    params.pageNumber = 1;
    this.shopService.setShopParams(params);
    this.getProducts();
  }

  onSortSelected(sort: string) {
    const params = this.shopService.getShopParams();
    params.sort = sort;
    this.shopService.setShopParams(params);
    this.getProducts();
  }

  onPageChanged(event: any) {
    const params = this.shopService.getShopParams();
    if (params.pageNumber != event) {
      params.pageNumber = event;
      this.shopService.setShopParams(params);
      this.getProducts(true);
    }

  }

  onSearch() {
    const params = this.shopService.getShopParams();
    params.search = this.searchTerm.nativeElement.value;
    params.pageNumber = 1;
    this.shopService.setShopParams(params);
    this.getProducts();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.shopParams = new shopParams();
    this.shopService.setShopParams(this.shopParams);
    this.getProducts();

  }

}
