import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import {CategoryNavigationItem, StorefrontCategoryDetails, TopLevelCategory} from "../models/category";

@Injectable({ providedIn: 'root'})
export class CategoryApi {
    private readonly http = inject(HttpClient);
    private readonly baseUrl = `${environment.apiUrl}/api/storefront/categories`;

    getNavigation(): Observable<CategoryNavigationItem[]> {
        return this.http.get<CategoryNavigationItem[]>(`${this.baseUrl}/navigation`);
    }

    getTopLevel(): Observable<TopLevelCategory[]> {
        return this.http.get<TopLevelCategory[]>(`${this.baseUrl}/top-level`)
    }

    getBySlug(slug: string): Observable<StorefrontCategoryDetails> {
        return this.http.get<StorefrontCategoryDetails>(`${this.baseUrl}/${slug}`);
    }
}