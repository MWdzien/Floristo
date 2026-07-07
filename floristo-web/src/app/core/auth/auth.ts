import { Injectable, computed, inject, signal } from '@angular/core';
import {
    User,
    onIdTokenChanged,
    signInWithEmailAndPassword,
    createUserWithEmailAndPassword,
    signOut,
    GoogleAuthProvider,
    signInWithPopup,
    updateProfile,
} from 'firebase/auth';
import { FirebaseClientService } from './firebase-client';

@Injectable({ providedIn: 'root' })
export class AuthService {
    private readonly _auth = inject(FirebaseClientService).auth;
    private readonly _user = signal<User | null>(null);
    private readonly _idToken = signal<string | null>(null);
    readonly currentUser = this._user.asReadonly();
    readonly idToken = this._idToken.asReadonly();
    readonly isAuthenticated = computed(() => this._user() !== null);

    constructor() {
        onIdTokenChanged(this._auth, async (user) => {
            this._user.set(user);
            this._idToken.set(user ? await user.getIdToken() : null);
        });
    }

    async signInWithEmail(email: string, password: string): Promise<void> {
        await signInWithEmailAndPassword(this._auth, email, password);
    }

    async signUpWithEmail(email: string, password: string, firstName: string, lastName: string): Promise<void> {
        const cred = await createUserWithEmailAndPassword(this._auth, email, password);
        await updateProfile(cred.user, { displayName: `${firstName} ${lastName}` });
    }

    async signInWithGoogle(): Promise<void> {
        await signInWithPopup(this._auth, new GoogleAuthProvider());
    }

    async signOut(): Promise<void> {
        await signOut(this._auth);
    }

    async refreshIdToken(): Promise<void> {
        await this._user()?.getIdToken(true);
    }

    async getIdTokenClaims(): Promise<Record<string, unknown> | null> {
        const user = this._user();
        if (!user)
            return null;
        const result = await user.getIdTokenResult();
        return result.claims;
    }
}