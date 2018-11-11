import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ListProductComponent } from './list-product/list-product.component';
import { CreateProductComponent } from './create-product/create-product.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MaterialModule } from './material.module';
import { HttpClientModule } from '@angular/common/http';
import { ListCategoryComponent } from './list-category/list-category.component';
import { LoginComponent } from './login/login.component';
import { AppRoutingModule } from './app.routing';
import { IndexComponent } from './index/index.component';

@NgModule({
   declarations: [
      AppComponent,
      ListProductComponent,
      ListCategoryComponent,
      LoginComponent,
      CreateProductComponent,
      IndexComponent
   ],
   imports: [
      HttpClientModule,
      FormsModule,
      BrowserModule,
      AppRoutingModule,
      BrowserAnimationsModule,
      MaterialModule,
      ReactiveFormsModule
   ],
   entryComponents: [
      CreateProductComponent
   ],
   providers: [],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
