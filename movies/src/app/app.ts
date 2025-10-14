import { Component } from '@angular/core';
import { MenuComponent } from './shared/components/menu/menu.component';
import { MatButtonModule } from '@angular/material/button';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, MenuComponent, MatButtonModule],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {

}
