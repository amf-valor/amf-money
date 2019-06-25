import { Trade } from './trade.model';

export interface TradingBook{
    id ?: number
    name : string
    amountPerCaptal : number
    riskRewardRatio : number
    totalCaptal: number
    riskPerTrade: number
    trades: Trade[]
}