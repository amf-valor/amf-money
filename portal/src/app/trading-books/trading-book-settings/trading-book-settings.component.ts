import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TradingBookSettings } from './trading-book-settings.model';
import { MessageService } from 'src/app/services/message.service';

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
    @Inject(MAT_DIALOG_DATA) public settings: TradingBookSettings,
    private messageService: MessageService) { }

  ngOnInit() {
    this.initBookSettingsForm()
    this.initControlErrorMessages()
  }
  
  private initControlErrorMessages() {
    this.controlErrorMessages = new Map<string, Map<string, string>>();
    
    this.controlErrorMessages.set(this.bookName, new Map<string, string>([
      ['required', this.messageService.get('tradingBookSettings.portfolioName.required')]
    ]))

    this.controlErrorMessages.set(this.amountPerCaptal, new Map<string, string>([
      ['required', this.messageService.get('tradingBookSettings.capitalPerTrading.required')],
      ['min', 
        `${this.messageService.get('tradingBookSettings.capitalPerTrading.min')} 
         ${this.amountPerCaptalMin}%`],
      ['max', 
      `${this.messageService.get('tradingBookSettings.capitalPerTrading.max')} 
       ${this.amountPerCaptalMax}%`],
    ]))

    this.controlErrorMessages.set(this.riskRewardRatio, new Map<string, string>([
      ['required', this.messageService.get('tradingBookSettings.riskRewardRatio.required')],
      ['min', 
        `${this.messageService.get('tradingBookSettings.riskRewardRatio.min')} 
         ${this.riskRewardRatioMin}`],
      ['max', 
      `${this.messageService.get('tradingBookSettings.riskRewardRatio.max')} 
       ${this.riskRewardRatioMax}`],
    ]))

    this.controlErrorMessages.set(this.riskPerTrade, new Map<string, string>([
      ['required', this.messageService.get('tradingBookSettings.riskPerTrade.required')],
      ['min', 
        `${this.messageService.get('tradingBookSettings.riskPerTrade.min')} 
         ${this.riskPerTradeMin}%`],
      ['max', 
      `${this.messageService.get('tradingBookSettings.riskPerTrade.max')} 
       ${this.riskPerTradeMax}%`],
    ]))

    this.controlErrorMessages.set(this.totalCaptal, new Map<string, string>([
      ['required', this.messageService.get('tradingBookSettings.capital.required')],
      ['min', 
        `${this.messageService.get('tradingBookSettings.capital.min')} 
         ${this.totalCaptalMin}`],
      ['max', 
      `${this.messageService.get('tradingBookSettings.capital.max')} 
       ${this.totalCaptalMax}`],
    ]))
  }

  private initBookSettingsForm(){
    this.bookSettingsForm = this.formBuilder.group({
      bookName: this.formBuilder.control(this.settings.name, [Validators.required]),
      amountPerCaptal: this.formBuilder.control(this.settings.amountPerCaptal * 100, [
        Validators.required, 
        Validators.min(this.amountPerCaptalMin), 
        Validators.max(this.riskRewardRatioMax)
      ]),
      riskRewardRatio: this.formBuilder.control(this.settings.riskRewardRatio, [
        Validators.required, 
        Validators.min(this.riskRewardRatioMin), 
        Validators.max(this.riskRewardRatioMax)
      ]),
      riskPerTrade: this.formBuilder.control(this.settings.riskPerTrade * 100, [
        Validators.required, 
        Validators.min(this.riskPerTradeMin), 
        Validators.max(this.riskPerTradeMax)
      ]),
      totalCaptal: this.formBuilder.control(this.settings.totalCaptal, [
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
    const settings: TradingBookSettings = {
      title: this.settings.title,
      name: this.bookSettingsForm.get(this.bookName).value,
      amountPerCaptal: this.bookSettingsForm.get(this.amountPerCaptal).value / 100,
      riskRewardRatio: this.bookSettingsForm.get(this.riskRewardRatio).value,
      totalCaptal: Number(this.bookSettingsForm.get(this.totalCaptal).value),
      riskPerTrade: this.bookSettingsForm.get(this.riskPerTrade).value / 100,
    }
    this.dialogRef.close(settings)
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
