import { Component, OnInit, Input } from '@angular/core';
import { TradingBook } from './trading-book.model';
import { GridApi } from 'ag-grid-community';
import { NumericCellEditorComponent } from '../numeric-cell-editor/numeric-cell-editor.component';
import { MoneyCellRendererComponent, MONEY_CELL_RENDERER } from './money-cell-renderer/money-cell-renderer.component';
import { PercentCellRendererComponent, PERCENT_CELL_RENDERER } from './percent-cell-renderer/percent-cell-renderer.component';
import { PortalApiService } from 'src/app/services/portal-api.service';
import { UtilsService } from 'src/app/services/utils.service';
import { Trade } from './trade.model'
import { MatDialog } from '@angular/material';
import { TradingBookSettingsComponent } from '../trading-book-settings/trading-book-settings.component';

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
          valueFormatter: "isNaN(value) ? 0 : value",
          cellStyle: function (params) {
            if (params.value < params.context.riskRewardRatio){ 
              return { backgroundColor: '#F44336'};
            }else{
              return { backgroundColor: 'white'};
            }
          }
        },
        {
          headerName: 'Capital Alocado', 
          field: 'allocatedCaptal',
          cellRenderer: PERCENT_CELL_RENDERER,
          valueGetter: "(getValue('total') + ctx.totalCaptal) / ctx.totalCaptal -1",
          cellStyle: function (params) {
            if (params.value > params.context.amountPerCaptal){ 
              return { backgroundColor: '#F44336'};
            }else{
              return { backgroundColor: 'white'};
            }
          }
        },
        {
          headerName: 'Risco Carteira', 
          field: 'risk',
          cellRenderer: PERCENT_CELL_RENDERER,
          cellStyle: function (params) {
            if (params.value > params.context.riskPerTrade){ 
              return { backgroundColor: '#F44336'};
            }else{
              return { backgroundColor: 'white'};
            }
          },
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

  gridContext: { 
    totalCaptal: number, 
    riskRewardRatio: number,
    amountPerCaptal: number,
    riskPerTrade: number 
  }

  constructor(private portalApiService: PortalApiService, 
    private utilsService: UtilsService,
    private dialog: MatDialog) { }

  ngOnInit() {
    this.gridContext = {
      totalCaptal: this.tradingBook.setting.totalCaptal,
      riskRewardRatio: this.tradingBook.setting.riskRewardRatio,
      amountPerCaptal: this.tradingBook.setting.amountPerCaptal,
      riskPerTrade: this.tradingBook.setting.riskPerTrade
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

  onSettingsBtnClick(){
    const dialogRef = this.dialog.open(TradingBookSettingsComponent, {
      data: {
        title: this.tradingBook.setting.name,
        name: this.tradingBook.setting.name,
        amountPerCaptal : this.tradingBook.setting.amountPerCaptal,
        riskRewardRatio : this.tradingBook.setting.riskRewardRatio,
        totalCaptal: this.tradingBook.setting.totalCaptal,
        riskPerTrade: this.tradingBook.setting.riskPerTrade
      }
    })

    dialogRef.afterClosed().subscribe(newSettings => {
      if(newSettings !== undefined){
        this.portalApiService.updateSettings(this.tradingBook.id, newSettings)
          .subscribe(() => {
            this.tradingBook.setting = newSettings
            this.utilsService.showMessage("configurações atualizadas com sucesso!")
          }, err => {
            this.utilsService.showNetworkError()
          })    
      }
    })
  }
}
