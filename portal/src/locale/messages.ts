export class Messages{
    static readonly enUS: Map<string, string> = new Map<string, string>([
        ['tradingBookSettings.add.title','New trading portfolio'],
        ['tradingBookSettings.portfolioName.required','Portfolio name is required'],
        ['tradingBookSettings.capitalPerTrading.required','Acceptable capital per trading is required'],
        ['tradingBookSettings.capitalPerTrading.min','Capital per trading minimum is '],
        ['tradingBookSettings.capitalPerTrading.max','Capital per trading maximum is '],
        ['tradingBookSettings.riskRewardRatio.required','Acceptable risk reward ratio is required'],
        ['tradingBookSettings.riskRewardRatio.min','Risk reward minimum is '],
        ['tradingBookSettings.riskRewardRatio.max','Risk reward maximum is '],
        ['tradingBookSettings.riskPerTrade.required', 'Acceptable risk per trading is required'],
        ['tradingBookSettings.riskPerTrade.min', 'Risk per trading minimum is '],
        ['tradingBookSettings.riskPerTrade.max', 'Risk per trading maximum is '],
        ['tradingBookSettings.capital.required', 'Capital is required'],
        ['tradingBookSettings.capital.min', 'Capital minimum is '],
        ['tradingBookSettings.capital.max', 'Capital maximum is '],
        ['notification.agree','got it!'],
        ['notification.network.error',
        'Oops! something went wrong with our servers, please try again later']
    ])

    static readonly ptBR: Map<string, string> = new Map<string, string>([
        ['tradingBookSettings.add.title','Novo portfolio de negociação'],
        ['tradingBookSettings.portfolioName.required','Informar o nome do portfolio é obrigatório'],
        ['tradingBookSettings.capitalPerTrading.required', 
        'Informar o capital aceitável por negociação é obrigatório'],
        ['tradingBookSettings.capitalPerTrading.min', 'O capital por negociação minimo é '],
        ['tradingBookSettings.capitalPerTrading.max', 'O capital por negociação máximo é '],
        ['tradingBookSettings.riskRewardRatio.required', 
        'Informar a relação risco ganho aceitável é obrigatório'],
        ['tradingBookSettings.riskRewardRatio.min', 'Relação risco ganho minimo é '],
        ['tradingBookSettings.riskRewardRatio.max', 'Relação risco ganho máximo é '],
        ['tradingBookSettings.riskPerTrade.required', 'Risco por negociação aceitável é obrigatório'],
        ['tradingBookSettings.riskPerTrade.min', 'Risco por negociação minimo é '],
        ['tradingBookSettings.riskPerTrade.max', 'Risco por negociação maximo é '],
        ['tradingBookSettings.capital.required', 'Informar o capital é obrigatório'],
        ['tradingBookSettings.capital.min', 'Capital minimo é '],
        ['tradingBookSettings.capital.max', 'Capital maximo é '],
        ['notification.agree','entendi!'],
        ['notification.network.error',
        'Oops! nossos servidores estão enfrentando dificuldades tente novamente mais tarde!']
    ])
}

