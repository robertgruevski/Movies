import { Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-genre',
  imports: [MatButtonModule],
  templateUrl: './create-genre.component.html',
  styleUrl: './create-genre.component.css'
})
export class CreateGenreComponent {
  router = inject(Router);

  saveChanges() {
    // save changes

    this.router.navigate(['/genres']);
  }
}
