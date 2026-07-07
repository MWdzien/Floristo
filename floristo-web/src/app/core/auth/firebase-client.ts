import { Injectable } from '@angular/core';
import { initializeApp, FirebaseApp } from 'firebase/app';
import { Auth, getAuth } from 'firebase/auth';
import { Analytics, getAnalytics } from 'firebase/analytics'
import { environment } from '../../../environments/environment';

@Injectable({ providedIn: 'root' })
export class FirebaseClientService {
    readonly app: FirebaseApp = initializeApp(environment.firebase);
    readonly auth: Auth = getAuth(this.app);
    readonly analytics: Analytics = getAnalytics(this.app);
}