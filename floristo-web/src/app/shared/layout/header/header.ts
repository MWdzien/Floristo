import { Component, inject, signal } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop'
import { CategoryApi } from "../../../core/api/category-api";
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './header.html',
  styleUrl: './header.scss',
})
export class Header {
  private readonly _categoryApi = inject(CategoryApi);

  readonly topLevelCategories = toSignal(this._categoryApi.getTopLevel(), { initialValue: [] });

  protected readonly menuOpen = signal(false);

  toggleMenu() {
    this.menuOpen.update(v => !v);
  }

  closeMenu() {
    this.menuOpen.set(false);
  }

  protected readonly close = close;
}
