import { Component, Input, numberAttribute } from '@angular/core';
import { TheaterCreationDTO, TheaterDTO } from '../theaters.models';
import { TheatersFormComponent } from "../theaters-form/theaters-form.component";

@Component({
  selector: 'app-edit-theater',
  imports: [TheatersFormComponent],
  templateUrl: './edit-theater.component.html',
  styleUrl: './edit-theater.component.css',
})
export class EditTheaterComponent {
  @Input({ transform: numberAttribute }) id!: number;

  model: TheaterDTO = { name: 'Emagine', id: 1, latitude: 42.67312794427652, longitude: -82.97417659063075};

  saveChanges(theater: TheaterCreationDTO) {
    console.log('Editing the theater', theater);
  }
}
