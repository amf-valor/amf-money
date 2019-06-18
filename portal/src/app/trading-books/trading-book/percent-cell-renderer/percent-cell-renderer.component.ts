import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'amp-percent-cell-renderer',
  template: `<span>{{params.value | percent}}</span>`
})
export class PercentCellRendererComponent implements OnInit {

  params: any;

  constructor() { }

  ngOnInit() {
  }

  agInit(params: any): void {
    this.params = params;
  }
}

export const PERCENT_CELL_RENDERER = 'percentCellRendererComponent'
