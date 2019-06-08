import { Component, OnInit, Inject } from '@angular/core';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { TradingBookSettingsComponent } from './trading-book-settings/trading-book-settings.component';

@Component({
  selector: 'amp-trading-books',
  templateUrl: './trading-books.component.html',
  styleUrls: ['./trading-books.component.css']
})
export class TradingBooksComponent implements OnInit {

  tradingBooks : any[]

  constructor(private dialog: MatDialog) { 
    this.tradingBooks = [];
  }

  ngOnInit() {
  }

  onCreateBookBtnClick(): void{
      this.dialog.open(TradingBookSettingsComponent,{
       
      })
  }

}
