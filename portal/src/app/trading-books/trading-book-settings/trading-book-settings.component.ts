import { Component, OnInit } from '@angular/core';
import { MatDialogRef, MatSnackBar } from '@angular/material';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PortalApiService } from 'src/app/services/portal-api.service';
import { UtilsService } from 'src/app/services/utils.service';
import { TradingBook } from '../trading-book/trading-book.model';

@Component({
  selector: 'amp-book-settings',
  templateUrl: './trading-book-settings.component.html',
  styleUrls: ['./trading-book-settings.component.css']
})
export class TradingBookSettingsComponent implements OnInit {

  static readonly BOOK_NAME: string = 'bookName'
  static readonly AMOUNT_PER_CAPTAL: string = 'amountPerCaptal'
  static readonly RISK_REWARD_RATIO: string = 'riskRewardRatio'
  static readonly RISK_PER_TRADE: string = 'riskPerTrade'
  static readonly TOTAL_CAPTAL: string = 'totalCaptal'

  bookSettingsForm: FormGroup
  controlErrorMessages: Map<string, Map<string, string>>
  
  constructor(
    private dialogRef: MatDialogRef<TradingBookSettingsComponent>, 
    private formBuilder: FormBuilder,
    private portalApiService: PortalApiService,
    private utilsService: UtilsService) { }

  ngOnInit() {
    this.initBookSettingsForm()
    this.initControlErrorMessages()
  }
  
  private initControlErrorMessages() {
    this.controlErrorMessages = new Map<string, Map<string, string>>();
    
    this.controlErrorMessages.set(this.bookName, new Map<string, string>([
      ['required','O nome do book de ofertas é obrigatório!']
    ]))

    this.controlErrorMessages.set(this.amountPerCaptal, new Map<string, string>([
      ['required', 'A porcentagem de concentração de captal por boleta é obrigatoria!'],
      ['min', `A concentração de captal minima é de ${this.amountPerCaptalMin}%`],
      ['max', `A concentração de captal maxima é de ${this.amountPerCaptalMax}%`]
    ]))

    this.controlErrorMessages.set(this.riskRewardRatio, new Map<string, string>([
      ['required', 'Informar a relação risco ganho é obrigatoria!'],
      ['min', `A relação risco ganho minima que pode ser definida é ${this.riskRewardRatioMin}`],
      ['max', `A relação risco ganho maxima que pode ser definida é ${this.riskRewardRatioMax}`]
    ]))

    this.controlErrorMessages.set(this.riskPerTrade, new Map<string, string>([
      ['required', 'Informar a porcentagem de risco por negociação é obrigatório!'],
      ['min', `Risco por negociação minimo é de ${this.riskPerTradeMin}%`],
      ['max', `Risco por negociação maximo é de ${this.riskPerTradeMax}%`]
    ]))

    this.controlErrorMessages.set(this.totalCaptal, new Map<string, string>([
      ['required', 'Informar o captal total é obrigatório'],
      ['min', `Capital total minimo é ${this.totalCaptalMin}`],
      ['max', `Capital total máximo é ${this.totalCaptalMax}`]
    ]))
  }

  private initBookSettingsForm(){
    this.bookSettingsForm = this.formBuilder.group({
      bookName: this.formBuilder.control('', [Validators.required]),
      amountPerCaptal: this.formBuilder.control('', [
        Validators.required, 
        Validators.min(this.amountPerCaptalMin), 
        Validators.max(this.riskRewardRatioMax)
      ]),
      riskRewardRatio: this.formBuilder.control('', [
        Validators.required, 
        Validators.min(this.riskRewardRatioMin), 
        Validators.max(this.riskRewardRatioMax)
      ]),
      riskPerTrade: this.formBuilder.control('', [
        Validators.required, 
        Validators.min(this.riskPerTradeMin), 
        Validators.max(this.riskPerTradeMax)
      ]),
      totalCaptal: this.formBuilder.control('', [
        Validators.required, 
        Validators.min(this.totalCaptalMin), 
        Validators.max(this.totalCaptalMax)
      ])
    })
  }


  onNoBtnClick(): void{
    this.dialogRef.close();
  }

  onOkBtnClick(): void{
    const settings: TradingBook = {
      name: this.bookSettingsForm.get(this.bookName).value,
      amountPerCaptal: this.bookSettingsForm.get(this.amountPerCaptal).value,
      riskRewardRatio: this.bookSettingsForm.get(this.riskRewardRatio).value,
      totalCaptal: this.bookSettingsForm.get(this.totalCaptal).value,
      riskPerTrade: this.bookSettingsForm.get(this.riskPerTrade).value,
      trades: []
    }
    
    this.portalApiService.createTradingBook(settings).subscribe(tradingBook => {
      this.dialogRef.close(tradingBook);
    }, err => {
      this.utilsService.showNetworkError()
      console.log(err)
    })
  }

  get bookName(): string{
    return TradingBookSettingsComponent.BOOK_NAME
  }

  get amountPerCaptal(): string{
    return TradingBookSettingsComponent.AMOUNT_PER_CAPTAL
  }

  get riskRewardRatio(): string{
    return TradingBookSettingsComponent.RISK_REWARD_RATIO
  }

  get riskPerTrade(): string{
    return TradingBookSettingsComponent.RISK_PER_TRADE
  }

  get amountPerCaptalMin(): number{
    return 1
  }

  get amountPerCaptalMax(): number{
    return 50
  }

  get riskRewardRatioMin(): number{
    return 2
  }

  get riskRewardRatioMax(): number{
    return 9999
  }

  get riskPerTradeMin(): number{
    return 0.01
  }

  get riskPerTradeMax(): number{
    return 100
  }

  get totalCaptal() : string {
    return TradingBookSettingsComponent.TOTAL_CAPTAL
  }

  get totalCaptalMin(): number{
    return 1
  }

  get totalCaptalMax(): number{
    return 99999999999999999
  }

}
