import { Injectable, inject } from '@angular/core'
import { AuthService } from './auth';
import { CustomerApi } from '../api/customer-api';
import { firstValueFrom } from 'rxjs'

@Injectable({ providedIn: 'root' })
export class AuthFlowService {
    private readonly _authService = inject(AuthService);
    private readonly _customerApi = inject(CustomerApi);

    async signInWithEmail(email: string, password: string): Promise<void> {
        await this._authService.signInWithEmail(email, password);
    }

    async signUpWithEmail(email: string, password: string, firstName: string, lastName: string): Promise<void> {
        await this._authService.signUpWithEmail(email, password, firstName, lastName);
        await this.ensureProvisioned(firstName, lastName);
    }

    async signInWithGoogle(): Promise<void> {
        await this._authService.signInWithGoogle();

        const user = this._authService.currentUser();
        const { firstName, lastName } = this.splitDisplayName(user?.displayName ?? null);
        await this.ensureProvisioned(firstName, lastName);
    }

    private splitDisplayName(displayName: string | null): { firstName: string; lastName?: string } {
        if (!displayName || !displayName.trim())
            return { firstName: 'Użytkownik' };
        const split = displayName.trim().split(/\s+/);
        return { firstName: split[0], lastName: split.slice(1).join(' ') || undefined }
    }

    private async ensureProvisioned(firstName: string, lastName?: string): Promise<void> {
        const claims = await this._authService.getIdTokenClaims();
        if (claims && claims['customer_id']) {
            return;
        }
        try {
            await firstValueFrom(this._customerApi.provision({ firstName, lastName }));
            await this._authService.refreshIdToken();
        } catch (e) {
            await this._authService.signOut();
            throw e;
        }
    }
}