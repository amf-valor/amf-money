import { Component, OnInit, Inject, LOCALE_ID } from '@angular/core';

@Component({
  selector: 'amp-money-cell-renderer',
  template: `<span>{{params.value | currency:coin}}</span>`
})
export class MoneyCellRendererComponent {

  params: any;
  coin: string;

  constructor(@Inject(LOCALE_ID) localeId: string) { 
    if(localeId == 'pt-BR'){
      this.coin = 'BRL';
    }
  }

  agInit(params: any): void {
    this.params = params;
  }
}

export const MONEY_CELL_RENDERER = 'moneyCellRenderer'
