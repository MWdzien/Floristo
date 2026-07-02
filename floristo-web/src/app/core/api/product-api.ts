import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { PagedResult } from '../models/paged-result'
import { ProductSortOption } from '../models/product-sort'
import { StorefrontProduct, StorefrontProductDetails } from '../models/product'

export interface ProductListQuery {
    categorySlug?: string,
    sort?: ProductSortOption,
    page?: number,
    pageSize?: number
}

@Injectable({ providedIn: 'root' })
export class ProductApi {
    private readonly http = inject(HttpClient);
    private readonly baseUrl = `${environment.apiUrl}/api/storefront/products`;

    list(query: ProductListQuery = {}): Observable<PagedResult<StorefrontProduct>> {
        let params = new HttpParams();

        if (query.categorySlug)
            params = params.set('categorySlug', query.categorySlug);
        if (query.sort)
            params = params.set('sort', query.sort);
        if (query.page !== undefined)
            params = params.set('page', query.page);
        if (query.pageSize !== undefined)
            params = params.set('pageSize', query.pageSize);

        return this.http.get<PagedResult<StorefrontProduct>>(this.baseUrl, { params });
    }

    getBySlug(slug: string): Observable<StorefrontProductDetails> {
        return this.http.get<StorefrontProductDetails>(`${this.baseUrl}/${slug}`);
    }

    getNewArrivals(limit = 8): Observable<StorefrontProduct[]> {
        const params = new HttpParams().set('limit', limit);
        return this.http.get<StorefrontProduct[]>(`${this.baseUrl}/new-arrivals`, {params});
    }

    search(query: string, page = 1, pageSize = 24): Observable<PagedResult<StorefrontProduct>> {
        const params = new HttpParams()
            .set('q', query)
            .set('page', page)
            .set('pageSize', pageSize);

        return this.http.get<PagedResult<StorefrontProduct>>(`${this.baseUrl}/search`, { params });

    }
}