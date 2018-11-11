import { DataSource } from '@angular/cdk/collections';
import { MatPaginator, MatSort } from '@angular/material';
import { map } from 'rxjs/operators';
import { Observable, of as observableOf, merge } from 'rxjs';
import { ProductService } from '../product.service';

// TODO: Replace this with your own data model type
export class ListProductItem {
  name: string;
  id: number;
  price: number;
  idCategory: number;
  category: string;

  constructor(
    id: number,
    name: string,
    price: number,
    idCategory: number,
    category: string,
  ) {
    this.name = name;
    this.id = id;
    this.price = price;
    this.idCategory = idCategory;
    this.category = category;
  }
}


/**
 * Data source for the ListProduct view. This class should
 * encapsulate all logic for fetching and manipulating the displayed data
 * (including sorting, pagination, and filtering).
 */
export class ListProductDataSource extends DataSource<ListProductItem> {
  data: ListProductItem[] = [];

  constructor(
    private productService: ProductService,
    private paginator: MatPaginator,
    private sort: MatSort) {
    super();

  }

  /**
   * Connect this data source to the table. The table will only update when
   * the returned stream emits new items.
   * @returns A stream of the items to be rendered.
   */
  connect(): Observable<ListProductItem[]> {
    // Combine everything that affects the rendered data into one update
    // stream for the data-table to consume.
    return this.productService
      .getAllProducts()
      .pipe(
        map(res => {
          let list: ListProductItem[] = [];
          res.forEach(product => {
            const t = new ListProductItem(product.ProductId, product.Name, product.Price, product.Category.CategoryId,product.Category.Name);
            list.push(t);
          });
          return list;
        }))
  }

  /**
   *  Called when the table is being destroyed. Use this function, to clean up
   * any open connections or free any held resources that were set up during connect.
   */
  disconnect() { }

  /**
   * Paginate the data (client-side). If you're using server-side pagination,
   * this would be replaced by requesting the appropriate data from the server.
   */
  private getPagedData(data: ListProductItem[]) {
    const startIndex = this.paginator.pageIndex * this.paginator.pageSize;
    return data.splice(startIndex, this.paginator.pageSize);
  }

  /**
   * Sort the data (client-side). If you're using server-side sorting,
   * this would be replaced by requesting the appropriate data from the server.
   */
  private getSortedData(data: ListProductItem[]) {
    if (!this.sort.active || this.sort.direction === '') {
      return data;
    }

    return data.sort((a, b) => {
      const isAsc = this.sort.direction === 'asc';
      switch (this.sort.active) {
        case 'name': return compare(a.name, b.name, isAsc);
        case 'id': return compare(+a.id, +b.id, isAsc);
        default: return 0;
      }
    });
  }
}

/** Simple sort comparator for example ID/Name columns (for client-side sorting). */
function compare(a, b, isAsc) {
  return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
}
