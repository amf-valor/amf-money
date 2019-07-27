import { Component, OnInit } from '@angular/core';
import { BaseComponent } from '../shared/base.component';

@Component({
  selector: 'amp-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent extends BaseComponent{

  constructor(){
    super();
  }

}
