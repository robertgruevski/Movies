import { Component } from '@angular/core';
import { CRUD_SERVICE_TOKEN } from '../../shared/providers/providers';
import { GenresService } from '../genres.service';
import { IndexEntitiesComponent } from "../../shared/components/index-entities/index-entities.component";

@Component({
  selector: 'app-index-genres',
  imports: [IndexEntitiesComponent],
  templateUrl: './index-genres.component.html',
  styleUrl: './index-genres.component.css',
  providers: [{ provide: CRUD_SERVICE_TOKEN, useClass: GenresService }],
})
export class IndexGenresComponent {
  
}
