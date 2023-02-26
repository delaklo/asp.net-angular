import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Product } from 'src/app/models/product.model';
import { ProductsService } from 'src/app/services/products.service';

@Component({
  selector: 'app-edit-product',
  templateUrl: './edit-product.component.html',
  styleUrls: ['./edit-product.component.css']
})
export class EditProductComponent implements OnInit {

  productDetails: Product = {
    id: '',
    name: '',
    description: '',
    category: '',
    price: 0
  } 

  constructor(private route: ActivatedRoute, private productServise: ProductsService, 
    private router: Router){}

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next:(params)=>{
       const id = params.get('id');

       if(id){
        this.productServise.getProduct(id).subscribe({
          next:(response)=>{
            this.productDetails = response;
          }
        })
       }
      }
    })
  }

  updateProduct(){
    this.productServise.updateProduct(this.productDetails.id, this.productDetails)
    .subscribe({
      next:(response)=>{
        this.router.navigate(['products']);
      },
      error:(response)=>{
        console.log(response);
      }
    });
  }

  deleteProduct(id:string){
    this.productServise.deleteProduct(id)
    .subscribe({
      next:(response)=>{
        this.router.navigate(['products']);
      },
      error:(response)=>{
        console.log(response);
      }
    })
  }


}
