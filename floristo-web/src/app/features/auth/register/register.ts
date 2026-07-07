import { Component } from '@angular/core';
import { ReactiveFormsModule, FormGroup, FormControl, Validators } from '@angular/forms';
import { Router, RouterLink, ActivatedRoute } from '@angular/router';
import { inject, signal } from '@angular/core';
import { AuthFlowService } from "../../../core/auth/auth-flow";
import { FirebaseError } from "firebase/app";


@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './register.html',
  styleUrl: './register.scss',
})
export class Register {
  private readonly authFlow = inject(AuthFlowService);
  private readonly router = inject(Router);
  private readonly route = inject(ActivatedRoute);
  private readonly returnUrl = this.route.snapshot.queryParamMap.get('returnUrl') ?? '/';

  protected readonly form = new FormGroup({
    firstName: new FormControl('', {
      nonNullable: true,
      validators: [Validators.required, Validators.minLength(2), Validators.maxLength(50)]
    }),
    lastName: new FormControl('', {
      nonNullable: true,
      validators: [Validators.required, Validators.minLength(2), Validators.maxLength(100)]
    }),
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
      const { firstName, lastName, email, password } = this.form.getRawValue();
      await this.authFlow.signUpWithEmail(email, password, firstName, lastName);
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
      await this.authFlow.signInWithGoogle();
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
        case 'auth/email-already-in-use':
          return 'Ten adres email jest już zajęty.';
        case 'auth/weak-password':
          return 'Hasło powinno mieć co najmniej 6 znaków.'
        case 'auth/invalid-email':
          return 'Nieprawidłowy format adresu email.';
        case 'auth/too-many-requests':
          return 'Zbyt wiele prób. Spróbuj później.';
        default:
          return 'Coś poszło nie tak. Spróbuj ponownie.';
      }
    }
    return 'Wystąpił błąd. Spróbuj ponownie.';
  }
}
