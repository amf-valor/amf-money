CREATE TABLE IF NOT EXISTS tradingBooks (
	id int primary key auto_increment 
	,name VARCHAR(100) 
	,amountPerCaptal DECIMAL(2,2)
    ,riskRewardRatio TINYINT(1) 
	,createdAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP 
    )ENGINE = INNODB;