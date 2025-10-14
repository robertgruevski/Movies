import { Component, inject } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { Router } from '@angular/router';
import { GenreCreationDTO } from '../genres.models';
import { GenresFormComponent } from "../genres-form/genres-form.component";

@Component({
  selector: 'app-create-genre',
  imports: [MatButtonModule, ReactiveFormsModule, MatFormFieldModule, MatInputModule, GenresFormComponent],
  templateUrl: './create-genre.component.html',
  styleUrl: './create-genre.component.css',
})
export class CreateGenreComponent {
  router = inject(Router);

  saveChanges(genre: GenreCreationDTO) {
    // save changes
    console.log(genre);
    this.router.navigate(['/genres']);
  }
}
