import { Component, OnInit, Input, AfterContentInit } from '@angular/core';
import { FormGroup, AbstractControl } from '@angular/forms';

@Component({
  selector: 'amp-input',
  templateUrl: './input.component.html',
  styleUrls: ['./input.component.css']
})
export class InputComponent implements OnInit, AfterContentInit {

  @Input() errorMessage: string
  @Input() type: string
  @Input() placeholder: string
  @Input() parentFormGroup: FormGroup
  @Input() controlName: string
  @Input() class: string
  @Input() min: number
  
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

  hasError(): boolean{
    return this.control.invalid && (this.control.dirty || this.control.touched)
  }

}
