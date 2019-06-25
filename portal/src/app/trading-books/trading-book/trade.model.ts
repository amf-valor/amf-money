export interface Trade{
    id: number
    operationType: string
    asset: string
    quantity: number
    price: number
    stopLoss: number
    stopGain: number
}