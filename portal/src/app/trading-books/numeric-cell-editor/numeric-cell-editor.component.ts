import { Component, AfterViewInit, ViewChild, ViewContainerRef } from '@angular/core';
import {ICellEditorAngularComp} from "ag-grid-angular";
import { ICellEditorParams } from 'ag-grid-community';

@Component({
  selector: 'amp-numeric-cell-editor',
  templateUrl: './numeric-cell-editor.component.html',
  styleUrls: ['./numeric-cell-editor.component.css']
})
export class NumericCellEditorComponent implements AfterViewInit, ICellEditorAngularComp {
  
  public value: number;
  @ViewChild('inputNumber', {read: ViewContainerRef, static: false}) input: any
  
  constructor() { }

  ngAfterViewInit() {
    setTimeout(() => {
      this.input.element.nativeElement.focus();
    })
  }

  getValue(): number {
    return this.value
  }

  agInit(params: ICellEditorParams): void {
    this.value = params.value;
  }
}
