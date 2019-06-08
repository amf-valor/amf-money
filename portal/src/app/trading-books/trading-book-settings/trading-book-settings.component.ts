import { Component, OnInit } from '@angular/core';
import { MatDialogRef, MatSnackBar } from '@angular/material';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PortalApiService } from 'src/app/services/portal-api.service';
import { TradingBookSettings } from './trading-book-settings.model';

@Component({
  selector: 'amp-book-settings',
  templateUrl: './trading-book-settings.component.html',
  styleUrls: ['./trading-book-settings.component.css']
})
export class TradingBookSettingsComponent implements OnInit {

  bookSettingsForm: FormGroup;
  
  constructor(
    private dialogRef: MatDialogRef<TradingBookSettingsComponent>, 
    private formBuilder: FormBuilder,
    private portalApiService: PortalApiService,
    private snackBarRef: MatSnackBar) { }

  ngOnInit() {
    this.bookSettingsForm = this.formBuilder.group({
      bookName: this.formBuilder.control('', [Validators.required]),
      amountPerCaptal: this.formBuilder.control('', [Validators.required]),
      riskGainRelationship: this.formBuilder.control('', Validators.required)
    })
  }

  onNoBtnClick(): void{
    this.dialogRef.close();
  }

  onOkBtnClick(): void{
    const settings: TradingBookSettings = {
      name: this.bookSettingsForm.get('bookName').value,
      amountPerCaptal: this.bookSettingsForm.get('amountPerCaptal').value,
      riskGainRelationship: this.bookSettingsForm.get('riskGainRelationship').value
    }
    
    this.portalApiService.createTradingBook(settings).subscribe(() => {
      this.dialogRef.close();
    }, err => {
      this.snackBarRef.open('Oops! Houve um problema com nossos servidores tente novamente mais tarde!', 
      'entendi!', { duration: 5000})
    })
  }

}
