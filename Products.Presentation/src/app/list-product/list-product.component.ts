import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { MatPaginator, MatSort, MatDialog, MatSnackBar } from '@angular/material';
import { ListProductDataSource } from './list-product-datasource';
import { ProductService } from '../product.service';
import { CreateProductComponent } from '../create-product/create-product.component';

@Component({
    selector: 'app-list-product',
    templateUrl: './list-product.component.html',
    styleUrls: ['./list-product.component.css'],
})
export class ListProductComponent implements OnInit {
    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild(MatSort) sort: MatSort;
    @Input() dataSource: ListProductDataSource;

    constructor(
        private productService: ProductService,
        public snackBar: MatSnackBar,
        private dialog: MatDialog) { }
    /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
    displayedColumns = ['id', 'name', 'price', 'category', 'star'];

    ngOnInit() {
        this.dataSource = new ListProductDataSource(this.productService, this.paginator, this.sort);
        this.productService.$productSaved.subscribe(p => this.onRefreshData());
    }


    onDelete(item: any) {
        this.productService
            .deleteProduct(item.id)
            .subscribe(res => {
                this.snackBar.open('Produto excluido com sucesso', '', {
                    duration: 2000,
                });
                this.onRefreshData();
            })
    }

    onEdit(item: any) {
        let dialogRef = this.dialog.open(CreateProductComponent, {
            height: 'auto',
            width: '600px',
            data: item
        });

        dialogRef.afterClosed().subscribe(res => this.onRefreshData())
    }

    onRefreshData() {
        this.dataSource = new ListProductDataSource(this.productService, this.paginator, this.sort);
    }
}
