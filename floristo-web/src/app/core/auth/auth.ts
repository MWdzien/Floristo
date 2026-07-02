import { Injectable, computed, inject, signal } from '@angular/core';
import { Session, User } from '@supabase/supabase-js';
import { SupabaseClientService} from "./subabase-client";

@Injectable({ providedIn: 'root' })
export class AuthService {
    private readonly supabase = inject(SupabaseClientService).client;

    private readonly _session = signal<Session | null>(null);

    readonly session = this._session.asReadonly();
    readonly currentUser = computed<User | null>(() => this._session()?.user ?? null);
    readonly isAuthenticated = computed(() => this._session() !== null);
    readonly accessToken = computed(() => this._session()?.access_token ?? null);

    constructor() {
        this.supabase.auth.getSession().then(({ data }) => {
            this._session.set(data.session);
        })

        this.supabase.auth.onAuthStateChange((_event, session) => {
            this._session.set(session);
        })
    }

    async signUp(email: string, password: string) {
        return this.supabase.auth.signUp({ email, password });
    }

    async signIn(email: string, password: string) {
        return this.supabase.auth.signInWithPassword({ email, password });
    }

    async signOut() {
        return this.supabase.auth.signOut();
    }
}