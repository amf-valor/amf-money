import { Trade } from './trade.model';
import { TradingBookSettings } from '../trading-book-settings/trading-book-settings.model';

export interface TradingBook{
    id ?: number
    setting: TradingBookSettings
    trades: Trade[]
}