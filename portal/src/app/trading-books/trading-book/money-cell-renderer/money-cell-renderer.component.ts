import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'amp-money-cell-renderer',
  template: `<span>{{params.value | currency:'BRL'}}</span>`
})
export class MoneyCellRendererComponent implements OnInit {

  params: any;

  constructor() { }

  ngOnInit() {
  }

  agInit(params: any): void {
    this.params = params;
  }
}
