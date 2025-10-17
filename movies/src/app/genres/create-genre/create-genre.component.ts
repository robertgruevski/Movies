import { Component, inject } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { Router } from '@angular/router';
import { GenreCreationDTO } from '../genres.models';
import { GenresFormComponent } from '../genres-form/genres-form.component';
import { GenresService } from '../genres.service';
import { extractErrors } from '../../shared/functions/extractErrors';
import { DisplayErrorsComponent } from '../../shared/components/display-errors/display-errors.component';
import { CRUD_SERVICE_TOKEN } from '../../shared/providers/providers';
import { CreateEntityComponent } from "../../shared/components/create-entity/create-entity.component";

@Component({
  selector: 'app-create-genre',
  imports: [
    MatButtonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    GenresFormComponent,
    DisplayErrorsComponent,
    CreateEntityComponent
],
  templateUrl: './create-genre.component.html',
  styleUrl: './create-genre.component.css',
  providers: [{ provide: CRUD_SERVICE_TOKEN, useClass: GenresService }],
})
export class CreateGenreComponent {
  genresForm = GenresFormComponent;
}
