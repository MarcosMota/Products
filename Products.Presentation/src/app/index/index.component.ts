import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material';
import { CreateProductComponent } from '../create-product/create-product.component';
import { Router } from '@angular/router';

@Component({
    selector: 'app-index',
    templateUrl: './index.component.html',
    styleUrls: ['./index.component.css']
})
export class IndexComponent {

    constructor(private dialog: MatDialog,private router: Router) { }

    onNewProduct() {
        let dialogRef = this.dialog.open(CreateProductComponent, {
            height: 'auto',
            width: '600px',
        });
    }
    
    onLogout(){
        localStorage.removeItem('logged');
        this.router.navigate(["login"]);

    }

    onDocumentation(){
        window.location.href = 'http://localhost:58320/swagger/ui/index';
    }
}
