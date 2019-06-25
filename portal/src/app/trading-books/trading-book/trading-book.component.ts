import { Component, OnInit, Input } from '@angular/core';
import { TradingBook } from './trading-book.model';
import { GridApi } from 'ag-grid-community';
import { NumericCellEditorComponent } from '../numeric-cell-editor/numeric-cell-editor.component';
import { MoneyCellRendererComponent, MONEY_CELL_RENDERER } from './money-cell-renderer/money-cell-renderer.component';
import { PercentCellRendererComponent, PERCENT_CELL_RENDERER } from './percent-cell-renderer/percent-cell-renderer.component';
import { PortalApiService } from 'src/app/services/portal-api.service';
import { UtilsService } from 'src/app/services/utils.service';
import { Trade } from './trade.model'

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
    {headerName: 'id', field: 'id', hide: true},
    {
      headerName: 'Negociação',
      children:[
        {
          headerName: 'Tipo Operação', 
          field: 'operationType', 
          editable: true,
          cellEditor: 'agSelectCellEditor',
          cellEditorParams: {values: ['Compra', 'Venda']},
          valueFormatter: function(params){
            if(params.value == "S") return "Venda"
            if(params.value == "B") return "Compra"
          }
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

  columnTypes = {
    valueColumn: {
      editable: true,
      valueParser: "Number(newValue)",
    }
  };

  gridContext: { totalCaptal: number; }

  constructor(private portalApiService: PortalApiService, private utilsService: UtilsService) { }

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
    const empty = {
      id: 0,  
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
    }
    this.gridApi.updateRowData({ add: [empty] });
  }

  onSyncBtnClick(){
    const trades: Trade[] = [];
    this.gridApi.forEachNode(node => {
      trades.push({
        id: node.data.id,
        operationType: node.data.operationType == "Compra" ? 'B' : 'S',
        asset: node.data.asset,
        quantity: node.data.quantity,
        price: node.data.price,
        stopGain: node.data.stopGain,
        stopLoss: node.data.stopLoss
      })
    });

    this.portalApiService.updateTrades(this.tradingBook.id, trades)
      .subscribe(() => {
        this.utilsService.showMessage('Book de ofertas sincronizado com exito!')
      }, err => {
        console.log(err)
        this.utilsService.showNetworkError()
      })
  }
}
