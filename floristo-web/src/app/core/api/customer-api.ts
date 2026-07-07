import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from "../../../environments/environment";
import { StorefrontCustomer } from "../models/customer";

export interface UpdateCustomerRequest {
    firstName: string,
    lastName: string
}

export interface ProvisionCustomerRequest {
    firstName: string,
    lastName?: string
}

@Injectable({ providedIn: 'root' })
export class CustomerApi {
    private readonly http = inject(HttpClient);
    private readonly baseUrl = `${environment.apiUrl}/api/storefront/customers`;

    getMe(): Observable<StorefrontCustomer> {
        return this.http.get<StorefrontCustomer>(`${this.baseUrl}/me`);
    }

    updateMe(request: UpdateCustomerRequest): Observable<StorefrontCustomer> {
        return this.http.put<StorefrontCustomer>(`${this.baseUrl}/me`, request);
    }

    provision(request: ProvisionCustomerRequest): Observable<void> {
        return this.http.post<void>(`${this.baseUrl}/me/provision`, request);
    }
}