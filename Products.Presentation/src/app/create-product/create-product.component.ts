import { Component, Inject } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MatSnackBar, MAT_DIALOG_DATA } from '@angular/material';
import { AppComponent } from '../app.component';
import { Observable } from 'rxjs';
import { CategoryService } from '../category.service';
import { ProductService } from '../product.service';
import { ListProductItem } from '../list-product/list-product-datasource';

@Component({
    selector: 'app-create-product',
    templateUrl: './create-product.component.html',
    styleUrls: ['./create-product.component.css'],
})
export class CreateProductComponent {
    addressForm = this.fb.group({
        id: 0,
        name: [null, Validators.required],
        category: [null, Validators.required],
        price: [null, Validators.required]
    });

    hasUnitNumber = false;

    $categories: Observable<any>;
    states = [
        { name: 'Alabama', id: 'AL' },
        { name: 'Alaska', id: 'AK' },
        { name: 'American Samoa', id: 'AS' },
        { name: 'Arizona', id: 'AZ' },
        { name: 'Arkansas', id: 'AR' },
        { name: 'California', id: 'CA' },
        { name: 'Colorado', id: 'CO' },
        { name: 'Connecticut', id: 'CT' },
        { name: 'Delaware', id: 'DE' },
        { name: 'District Of Columbia', id: 'DC' },
        { name: 'Federated States Of Micronesia', id: 'FM' },
        { name: 'Florida', id: 'FL' },
        { name: 'Georgia', id: 'GA' },
        { name: 'Guam', id: 'GU' },
        { name: 'Hawaii', id: 'HI' },
        { name: 'Idaho', id: 'ID' },
        { name: 'Illinois', id: 'IL' },
        { name: 'Indiana', id: 'IN' },
        { name: 'Iowa', id: 'IA' },
        { name: 'Kansas', id: 'KS' },
        { name: 'Kentucky', id: 'KY' },
        { name: 'Louisiana', id: 'LA' },
        { name: 'Maine', id: 'ME' },
        { name: 'Marshall Islands', id: 'MH' },
        { name: 'Maryland', id: 'MD' },
        { name: 'Massachusetts', id: 'MA' },
        { name: 'Michigan', id: 'MI' },
        { name: 'Minnesota', id: 'MN' },
        { name: 'Mississippi', id: 'MS' },
        { name: 'Missouri', id: 'MO' },
        { name: 'Montana', id: 'MT' },
        { name: 'Nebraska', id: 'NE' },
        { name: 'Nevada', id: 'NV' },
        { name: 'New Hampshire', id: 'NH' },
        { name: 'New Jersey', id: 'NJ' },
        { name: 'New Mexico', id: 'NM' },
        { name: 'New York', id: 'NY' },
        { name: 'North Carolina', id: 'NC' },
        { name: 'North Dakota', id: 'ND' },
        { name: 'Northern Mariana Islands', id: 'MP' },
        { name: 'Ohio', id: 'OH' },
        { name: 'Oklahoma', id: 'OK' },
        { name: 'Oregon', id: 'OR' },
        { name: 'Palau', id: 'PW' },
        { name: 'Pennsylvania', id: 'PA' },
        { name: 'Puerto Rico', id: 'PR' },
        { name: 'Rhode Island', id: 'RI' },
        { name: 'South Carolina', id: 'SC' },
        { name: 'South Dakota', id: 'SD' },
        { name: 'Tennessee', id: 'TN' },
        { name: 'Texas', id: 'TX' },
        { name: 'Utah', id: 'UT' },
        { name: 'Vermont', id: 'VT' },
        { name: 'Virgin Islands', id: 'VI' },
        { name: 'Virginia', id: 'VA' },
        { name: 'Washington', id: 'WA' },
        { name: 'West Virginia', id: 'WV' },
        { name: 'Wisconsin', id: 'WI' },
        { name: 'Wyoming', id: 'WY' }
    ];


    constructor(
        public snackBar: MatSnackBar,
        private fb: FormBuilder,
        categoryService: CategoryService,
        private productService: ProductService,
        public dialogRef: MatDialogRef<AppComponent>,
        @Inject(MAT_DIALOG_DATA) public data: ListProductItem) {
        
        this.$categories = categoryService.getAllCategories();
        if(data != null){
            console.log(data);
            this.addressForm.patchValue({
                id: data.id,
                name: data.name,
                category: data.idCategory,
                price: data.price
            });
        }
    }

    onNoClick(): void {
        this.dialogRef.close();
    }

    onSaveClick(): void {
        if (this.addressForm.valid) {
            const data = this.addressForm.value;
            console.log(data);
            debugger;
            this.productService.saveProduct({
                "ProductId": data.id,
                "Name": data.name,
                "Price": data.price,
                "CategoryId": data.category
            }).subscribe(
                data => {
                    this.dialogRef.close();
                    this.productService.$productSaved.emit();
                    this.snackBar.open(data, '', {
                        duration: 2000,
                      });
                },
                err => console.log(err),
                () => console.log('yay')
            );
        }
    }

}
