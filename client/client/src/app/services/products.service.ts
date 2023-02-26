import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { Product } from '../models/product.model';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  apiUrl: string = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getAllProducts(): Observable<Product[]>{
   return this.http.get<Product[]>(this.apiUrl + '/api/products');
  }

  addProduct(addProductRequest: Product): Observable<Product>{
    addProductRequest.id = '00000000-0000-0000-0000-000000000000'
   return this.http.post<Product>(this.apiUrl + '/api/products', addProductRequest)
  }

  getProduct(id: string):Observable<Product>{
    return this.http.get<Product>(this.apiUrl + '/api/products/' + id);
  }

  updateProduct(id: string, updateProductRequest: Product): Observable<Product>{
   return this.http.put<Product>(this.apiUrl + '/api/products/' +id, updateProductRequest);
      }

  deleteProduct(id:string):Observable<Product>{
    return this.http.delete<Product>(this.apiUrl + '/api/products/' + id);
      }
}
