import { Component, OnInit, Inject } from '@angular/core';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { TradingBookSettingsComponent } from './trading-book-settings/trading-book-settings.component';
import { TradingBook } from './trading-book/trading-book.model';
import { PortalApiService } from '../services/portal-api.service';
import { UtilsService } from '../services/utils.service';
import { MessageService } from '../services/message.service';

@Component({
  selector: 'amp-trading-books',
  templateUrl: './trading-books.component.html',
  styleUrls: ['./trading-books.component.css']
})
export class TradingBooksComponent implements OnInit {

  tradingBooks : TradingBook[]

  constructor(
    private dialog: MatDialog, 
    private portalApiService: PortalApiService,
    private utilsService: UtilsService,
    private messageService: MessageService){ }

  ngOnInit(){
    this.portalApiService.getAllTradingBooks()
      .subscribe(tradingBooks => {
        this.tradingBooks = tradingBooks
      }, err => {
        this.utilsService.showNetworkError()
      })
  }

  onCreateBookBtnClick(): void{
    const dialogRef = this.dialog.open(TradingBookSettingsComponent, {
      data: {
        title: this.messageService.get('tradingBookSettings.add.title'),
        name: '',
        amountPerCaptal : 0.15,
        riskRewardRatio : 3,
        totalCaptal: '',
        riskPerTrade: 0.01
      }
    })
    
    dialogRef.afterClosed().subscribe(settings => {
      if(settings !== undefined){
        this.portalApiService.createTradingBook(settings)
        .subscribe(newId => {
          const tradingBook: TradingBook = {
            id: newId,
            setting: settings,
            trades: []
          } 
          this.tradingBooks.push(tradingBook)
        }, err => this.utilsService.showNetworkError())
      }
    })
  }
}
