import { Component, OnInit, Input } from '@angular/core';
import { TradingBook } from './trading-book.model';
import { GridApi } from 'ag-grid-community';
import { NumericCellEditorComponent } from '../numeric-cell-editor/numeric-cell-editor.component';

@Component({
  selector: 'amp-trading-book',
  templateUrl: './trading-book.component.html',
  styleUrls: ['./trading-book.component.css']
})
export class TradingBookComponent implements OnInit {

  @Input() tradingBook: TradingBook

  gridApi: GridApi
  defaultColDef = {resizable: true}
  columnDefs = [
    {
      headerName: 'Negociação',
      children:[
        {headerName: 'Tipo Operação', field: 'operationType', editable: true},
        {headerName: 'Ativo', field: 'asset', editable: true},
        {headerName: 'Quantidade', field: 'quantity', editable: true, cellEditorFramework: NumericCellEditorComponent},
        {headerName: 'Preço', field: 'price', editable: true},
        {headerName: 'Total', field: 'total', editable: true},
        {headerName: 'Stop Loss', field: 'stopLoss', editable: true},
        {headerName: 'Stop Gain', field: 'stopGain', editable: true}
      ]
    },
    {
      headerName: 'Gerenciamento de risco',
      children:[
        {headerName: 'Risco ganho', field: 'riskRewardRatio'},
        {headerName: 'Capital Alocado', field: 'allocatedCaptal'},
        {headerName: 'Risco', field: 'risk'}
      ]
    }
  ];

  rowData = [
    { operationType: 'Compra', asset: 'BCFF11', quantity: 10, price: 10, total: 100, stopLoss: 15.00, stopGain: 40, riskRewardRatio: 4, allocatedCaptal: 15, risk: 0.34},
    { operationType: 'Compra', asset: 'BPFF11', price: 50.90 },
    { operationType: 'Venda', asset: 'VISC11', price: 800.20 }
  ];

  
  constructor() { }

  ngOnInit() {}

  onGridReady(params) {
    this.gridApi = params.api
    this.gridApi.sizeColumnsToFit()
  }
}
