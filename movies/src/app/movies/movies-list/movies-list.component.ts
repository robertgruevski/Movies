import { CurrencyPipe, DatePipe, UpperCasePipe } from '@angular/common';
import { Component, Input } from '@angular/core';
import { GenericList } from "../../shared/components/generic-list/generic-list";
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-movies-list',
  imports: [DatePipe, CurrencyPipe, UpperCasePipe, GenericList, MatButtonModule, MatIconModule],
  templateUrl: './movies-list.component.html',
  styleUrl: './movies-list.component.css',
})
export class MoviesListComponent {
  @Input({ required: true })
  movies!: any[];

  addMovie() {
    this.movies.push({
      title: 'Inception',
      releaseDate: new Date('07-03-2012'),
      price: 500,
    });
  }

  removeMovie(movie: any) {
    let index = this.movies.findIndex((m: any) => m.title === movie.title);
    this.movies.splice(index, 1);
  }
}
