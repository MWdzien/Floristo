import { Component, inject } from '@angular/core';
import { CurrencyPipe } from '@angular/common'
import { RouterLink } from '@angular/router'
import { ProductApi } from "../../core/api/product-api";
import { CategoryApi } from "../../core/api/category-api";
import { toSignal } from  '@angular/core/rxjs-interop'
import { PlaceholderImage} from "../../shared/ui/placeholder-image/placeholder-image";
import {Header} from "../../shared/layout/header/header";

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [PlaceholderImage, CurrencyPipe, RouterLink, Header],
  templateUrl: './home.html',
  styleUrl: './home.scss',
})
export class Home {
  private readonly _productApi = inject(ProductApi);
  private readonly _categoryApi = inject(CategoryApi);

  readonly newArrivals = toSignal(this._productApi.getNewArrivals(8), { initialValue: [] });
  readonly categories = toSignal(this._categoryApi.getTopLevel(), { initialValue: []});
}
