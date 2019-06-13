import { Component, OnInit, Input } from '@angular/core';
import { TradingBook } from './trading-book.model';

@Component({
  selector: 'amp-trading-book',
  templateUrl: './trading-book.component.html',
  styleUrls: ['./trading-book.component.css']
})
export class TradingBookComponent implements OnInit {

  @Input() tradingBook: TradingBook
  
  constructor() { }

  ngOnInit() {
  }

}
