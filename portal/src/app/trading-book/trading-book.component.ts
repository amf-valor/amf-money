import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-trading-book',
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
