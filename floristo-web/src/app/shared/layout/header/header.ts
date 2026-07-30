import { Component, inject, signal } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop'
import { CategoryApi } from "../../../core/api/category-api";
import { RouterLink } from '@angular/router';
import {CategoryNavigationItem} from "../../../core/models/category";

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
  readonly categories = toSignal(this._categoryApi.getNavigation(), { initialValue: [] });
  protected readonly menuOpen = signal(false);

  protected readonly expanded = signal(new Set<string>);

  protected readonly hasChildren = (category: CategoryNavigationItem) => !!category.children && category.children.length > 0;

  toggleMenu() {
    this.menuOpen.update(v => !v);
  }

  closeMenu() {
    this.menuOpen.set(false);
  }

  toggleCategory(slug: string){
    this.expanded.update(set => {
      const next = new Set(set);
      next.has(slug) ? next.delete(slug) : next.add(slug);
      return next;
    });
  }
}
