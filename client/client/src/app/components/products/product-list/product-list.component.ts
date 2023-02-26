import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/models/product.model';
import { ProductsService } from 'src/app/services/products.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {

  products: Product[] = [];

  constructor(private productsService: ProductsService){}

  ngOnInit(): void {
    this.productsService.getAllProducts().subscribe({
      next: (products) =>{
        this.products = products;
      },
      error:(response)=>{
        console.log(response);
      }
    })
  }
}
