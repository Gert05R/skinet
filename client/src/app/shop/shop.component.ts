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
  shopParams = new shopParams();

  sortOptions = [
    {name: 'Alphabetical', value: 'name'},
    {name: 'Price low to High', value: 'priceAsc',},
    {name: 'Price: High to low', value: 'priceDesc'}
  ];



  constructor(private shopService: ShopService) { }

  ngOnInit(): void {

    this.getProducts();
    this.getBrands();
    this.getTypes();

  }

  //we are only receiving the IProduct type(data) from IPagination into procuts array, not the other fields from IPagination
  getProducts() {
    this.shopService.getProducts(this.shopParams).subscribe((response => {
      this.products = response.data;
      this.shopParams.pageNumber = response.pageIndex;
      this.shopParams.pageSize = response.pageSize;
      this.shopParams.totalCount = response.count;
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
    this.shopParams.brandId = brandId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onTypeSelected(typeId: number) {
    this.shopParams.typeId = typeId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onSortSelected(sort: string) {
    this.shopParams.sort = sort;
    this.getProducts();
  }

  onPageChanged(event: any) {
    if (this.shopParams.pageNumber != event) {
      this.shopParams.pageNumber = event;
      this.getProducts();
    }

  }

  onSearch() {
    this.shopParams.search = this.searchTerm.nativeElement.value;
    this.shopParams.pageNumber = 1
    this.getProducts();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.shopParams = new shopParams();
    this.getProducts();

  }

}
