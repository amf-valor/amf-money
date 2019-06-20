import { Component, OnInit, Input } from '@angular/core';
import { TradingBook } from './trading-book.model';
import { GridApi } from 'ag-grid-community';
import { NumericCellEditorComponent } from '../numeric-cell-editor/numeric-cell-editor.component';
import { MoneyCellRendererComponent, MONEY_CELL_RENDERER } from './money-cell-renderer/money-cell-renderer.component';
import { PercentCellRendererComponent, PERCENT_CELL_RENDERER } from './percent-cell-renderer/percent-cell-renderer.component';

@Component({
  selector: 'amp-trading-book',
  templateUrl: './trading-book.component.html',
  styleUrls: ['./trading-book.component.css']
})
export class TradingBookComponent implements OnInit {

  @Input() tradingBook: TradingBook
  
  gridApi: GridApi
  defaultColDef = {resizable: true}
  frameworkComponents = {
    moneyCellRendererComponent: MoneyCellRendererComponent,
    percentCellRendererComponent: PercentCellRendererComponent
  };

  columnDefs = [
    {
      headerName: 'Negociação',
      children:[
        {
          headerName: 'Tipo Operação', 
          field: 'operationType', 
          editable: true,
          cellEditor: 'agSelectCellEditor',
          cellEditorParams: {values: ['Compra', 'Venda']}
        },
        {headerName: 'Ativo', field: 'asset', editable: true},
        {
          headerName: 'Quantidade', 
          field: 'quantity', 
          type: "valueColumn", 
          cellEditorFramework: NumericCellEditorComponent
        },
        {
          headerName: 'Preço', 
          field: 'price',
          type: "valueColumn",
          cellRenderer: MONEY_CELL_RENDERER
        },
        {
          headerName: 'Total', 
          field: 'total',
          cellRenderer: MONEY_CELL_RENDERER,
          valueGetter: "getValue('price') * getValue('quantity')"
        },
        {
          headerName: 'Stop Loss', 
          field: 'stopLoss', 
          editable: true,
          cellRenderer: MONEY_CELL_RENDERER
        },
        {
          headerName: 'Stop Gain', 
          field: 'stopGain', 
          editable: true,
          cellRenderer: MONEY_CELL_RENDERER
        }
      ]
    },
    {
      headerName: 'Gerenciamento de risco',
      children:[
        {
          headerName: 'Risco ganho', 
          field: 'riskRewardRatio',
          valueGetter: "data.operationType == 'Compra' ? ((data.stopGain - data.price) * data.quantity)"+
            " / ((data.price - data.stopLoss) * data.quantity) : " +
            "(data.price - data.stopGain) / (data.stopLoss - data.price)",
          valueFormatter: "isNaN(value) ? 0 : value"
          
        },
        {
          headerName: 'Capital Alocado', 
          field: 'allocatedCaptal',
          cellRenderer: PERCENT_CELL_RENDERER,
          valueGetter: "(getValue('total') + ctx.totalCaptal) / ctx.totalCaptal -1"
        },
        {
          headerName: 'Risco Carteira', 
          field: 'risk',
          cellRenderer: PERCENT_CELL_RENDERER,
          valueGetter: "data.operationType == 'Compra' ? " +
            "((data.price - data.stopLoss) * data.quantity + ctx.totalCaptal) / ctx.totalCaptal -1 "+
            ": (ctx.totalCaptal / (ctx.totalCaptal - ((data.stopLoss - data.price) * data.quantity))) -1"
        }
      ]
    }
  ];

  rowData = [
    { operationType: 'Compra', asset: 'BCFF11', quantity: 10, price: 10, total: 100, stopLoss: 15.00, stopGain: 40, riskRewardRatio: 4, allocatedCaptal: 0.15, risk: 0.34},
    { operationType: 'Compra', asset: 'BPFF11', price: 50.90 },
    { operationType: 'Venda', asset: 'VISC11', price: 800.20 }
  ];

  columnTypes = {
    valueColumn: {
      editable: true,
      valueParser: "Number(newValue)",
    }
  };

  gridContext

  constructor() { 
  }

  ngOnInit() {
    this.gridContext = {
      totalCaptal: this.tradingBook.totalCaptal
    }
  }

  onGridReady(params) {
    this.gridApi = params.api
    this.gridApi.sizeColumnsToFit()
  }

  onAddNewRowClick(){   
    this.gridApi.updateRowData({ 
      add: [{
        operationType:'',
        asset: '',
        quantity: 0,
        price: 0,
        total: 0,
        stopLoss: 0,
        stopGain: 0,
        riskRewardRatio: 0,
        allocatedCaptal: 0,
        risk: 0
      }]
    });
  }
}
