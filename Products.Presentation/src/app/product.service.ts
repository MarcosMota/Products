import { Injectable, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class ProductService {

    apiBase: string = "http://localhost:58320/";
    endPoint:string = "api/v1/products/";
    $productSaved: EventEmitter<any> = new EventEmitter(); 
    
    constructor(private http: HttpClient) { }

    getAllProducts(): Observable<any>{
        return this.http.get(this.apiBase + this.endPoint)
            .pipe(
                map(res => <any>res)
            )
    }

    saveProduct(data: any): Observable<any>{
        if(data.ProductId == 0){
            return this.http.post(this.apiBase + this.endPoint, data);
        }else{
            return this.http.put(this.apiBase + this.endPoint, data);
        }
    }

    deleteProduct(id: number): Observable<any>{
           return this.http.delete(this.apiBase + this.endPoint + id);
        
    }
}
