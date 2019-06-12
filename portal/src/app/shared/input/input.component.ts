import { Component, OnInit, Input, AfterContentInit } from '@angular/core';
import { FormGroup, AbstractControl } from '@angular/forms';
import { TradingBookSettingsComponent } from 'src/app/trading-books/trading-book-settings/trading-book-settings.component';

@Component({
  selector: 'amp-input',
  templateUrl: './input.component.html',
  styleUrls: ['./input.component.css']
})
export class InputComponent implements OnInit, AfterContentInit {

  @Input() errorMessages: Map<string, string>
  @Input() type: string
  @Input() placeholder: string
  @Input() parentFormGroup: FormGroup
  @Input() controlName: string
  @Input() class: string
  @Input() min: number
  @Input() max: number
  
  private control: AbstractControl

  constructor() { }

  ngOnInit() {
    this.control = this.parentFormGroup.controls[this.controlName]
  }

  ngAfterContentInit(){
    if(this.control === undefined){
      throw new Error('Esse componente precisa ser usado com uma diretiva formControlName')
    }
  }

  get errorMessage() {
    for (const error in this.control.errors) {
      if (this.control.errors.hasOwnProperty(error) && this.control.touched) {
        return this.errorMessages.get(error)
      }
    }

    return null
  }

  hasError(): boolean{
    return this.control.invalid && (this.control.dirty || this.control.touched)
  }
}
