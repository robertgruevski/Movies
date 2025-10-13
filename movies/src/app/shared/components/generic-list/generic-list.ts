import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-generic-list',
  imports: [],
  templateUrl: './generic-list.html',
  styleUrl: './generic-list.css'
})
export class GenericList {
  @Input({required: true})
  elements: any;
}
