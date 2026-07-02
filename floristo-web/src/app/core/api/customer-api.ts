import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from "../../../environments/environment";
import { StorefrontCustomer } from "../models/customer";

export interface UpdateCustomerRequest {
    firstName: string,
    lastName: string,
    phone?: string
}

@Injectable({ providedIn: 'root' })
export class CustomerApi {
    private readonly http = inject(HttpClient);
    private readonly baseUrl = `${environment.apiUrl}/api/storefront/customer`;

    getMe(): Observable<StorefrontCustomer> {
        return this.http.get<StorefrontCustomer>(`${this.baseUrl}/me`);
    }

    updateMe(request: UpdateCustomerRequest): Observable<StorefrontCustomer> {
        return this.http.put<StorefrontCustomer>(`${this.baseUrl}/me`, request);
    }
}