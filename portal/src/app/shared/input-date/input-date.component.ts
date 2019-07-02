import { Component } from '@angular/core';
import { InputComponent } from '../input/input.component';

@Component({
  selector: 'amp-input-date',
  templateUrl: './input-date.component.html',
  styleUrls: ['./input-date.component.css']
})
export class InputDateComponent extends InputComponent {

  constructor() {
    super();
  }
}
