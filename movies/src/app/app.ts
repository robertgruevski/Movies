import { CurrencyPipe, DatePipe, NgOptimizedImage, UpperCasePipe } from '@angular/common';
import { Component } from '@angular/core';
import { MoviesListComponent } from './movies/movies-list/movies-list.component';
import { MenuComponent } from './shared/components/menu/menu.component';
import { MatButtonModule } from '@angular/material/button';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, MoviesListComponent, MenuComponent, MatButtonModule],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {

}
