import { Component } from '@angular/core';
import { ReactiveFormsModule, FormGroup, FormControl, Validators } from '@angular/forms';
import { Router, RouterLink, ActivatedRoute } from '@angular/router';
import { inject, signal } from '@angular/core';
import { AuthService } from "../../../core/auth/auth";
import { FirebaseError } from "firebase/app";

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './login.html',
  styleUrl: './login.scss',
})
export class Login {
  private readonly auth = inject(AuthService);
  private readonly router = inject(Router);
  private readonly route = inject(ActivatedRoute);
  private readonly returnUrl = this.route.snapshot.queryParamMap.get('returnUrl') ?? '/';

  protected readonly form = new FormGroup({
    email: new FormControl('', {
      nonNullable: true,
      validators: [Validators.required, Validators.email]
    }),
    password: new FormControl('', {
      nonNullable: true,
      validators: [Validators.required, Validators.minLength(6), Validators.maxLength(128)]
    })
  });
  protected readonly errorMessage = signal<string | null>(null);
  protected readonly isLoading = signal(false);

  async onEmailSubmit(): Promise<void> {
    if (this.form.invalid)
      return;
    this.isLoading.set(true);
    this.errorMessage.set(null);
    try {
      const { email, password } = this.form.getRawValue();
      await this.auth.signInWithEmail(email, password);
      await this.router.navigateByUrl(this.returnUrl);
    } catch (e) {
      this.errorMessage.set(this.mapAuthError(e));
    } finally {
      this.isLoading.set(false);
    }
  }

  async onGoogleSubmit(): Promise<void> {
    this.isLoading.set(true);
    this.errorMessage.set(null);
    try {
      await this.auth.signInWithGoogle();
      await this.router.navigateByUrl(this.returnUrl);
    } catch (e) {
      this.errorMessage.set(this.mapAuthError(e));
    } finally {
      this.isLoading.set(false);
    }
  }

  private mapAuthError(e: unknown): string {
    if (e instanceof FirebaseError) {
      switch (e.code) {
        case 'auth/invalid-credential':
        case 'auth/wrong-password':
        case 'auth/user-not-found':
          return "Nieprawidłowy email lub hasło.";
        case 'auth/too-many-requests':
          return 'Zbyt wiele prób. Spróbuj później.';
        default:
          return 'Coś poszło nie tak. Spróbuj ponownie.';
      }
    }
    return 'Wystąpił błąd. Spróbuj ponownie.';
  }
}
