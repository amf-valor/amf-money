import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'amp-trading-book',
  templateUrl: './trading-book.component.html',
  styleUrls: ['./trading-book.component.css']
})
export class TradingBookComponent implements OnInit {

  tradingBooks : any[]

  constructor() { 
    this.tradingBooks = [];
  }

  ngOnInit() {
  }

}
