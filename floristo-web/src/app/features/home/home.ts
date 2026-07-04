import { Component, inject } from '@angular/core';
import { ProductApi } from "../../core/api/product-api";
import { toSignal } from  '@angular/core/rxjs-interop'
import { PlaceholderImage} from "../../shared/ui/placeholder-image/placeholder-image";

@Component({
  selector: 'app-home',
  imports: [PlaceholderImage],
  templateUrl: './home.html',
  styleUrl: './home.scss',
})
export class Home {
}
