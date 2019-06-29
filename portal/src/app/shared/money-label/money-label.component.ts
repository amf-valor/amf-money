import { Component, Input, LOCALE_ID, Inject } from '@angular/core';

@Component({
  selector: 'amp-money-label',
  template: '<span>{{title}}{{value | currency : coin}}</span>',
})
export class MoneyLabelComponent{

  @Input() title: string
  @Input() value: number;
  coin: string

  constructor(@Inject(LOCALE_ID) localeId:string) { 
    if(localeId == 'pt-BR'){
      this.coin = 'BRL'
    }
  }
}
