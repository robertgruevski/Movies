import { Component, inject } from '@angular/core';
import { ActorsFormComponent } from '../actors-form/actors-form.component';
import { ActorCreationDTO } from '../actors.models';
import { ActorsService } from '../actors.service';
import { Router } from '@angular/router';
import { extractErrors } from '../../shared/functions/extractErrors';
import { DisplayErrorsComponent } from "../../shared/components/display-errors/display-errors.component";

@Component({
  selector: 'app-create-actor',
  imports: [ActorsFormComponent, DisplayErrorsComponent],
  templateUrl: './create-actor.component.html',
  styleUrl: './create-actor.component.css',
})
export class CreateActorComponent {

  actorsService = inject(ActorsService);
  router = inject(Router);
  errors: string[] = [];

  saveChanges(actor: ActorCreationDTO) {
    this.actorsService.create(actor).subscribe({
      next: () => {
        this.router.navigate(['/actors']);
      },
      error: err => {
        const errors = extractErrors(err);
        this.errors = errors;
      }
    })
  }
}
