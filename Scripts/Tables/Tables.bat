
ECHO CREATING TABLE

sqlcmd -S %server% -d %db_name% -i "Buyers.sql" >> "OutputLog.log"
sqlcmd -S %server% -d %db_name% -i "Products.sql" >> "OutputLog.log"
sqlcmd -S %server% -d %db_name% -i "Orders.sql" >> "OutputLog.log"
sqlcmd -S %server% -d %db_name% -i "BuyerOrderDetails.sql" >> "OutputLog.log"
sqlcmd -S %server% -d %db_name% -i "ProductOrderDetaits.sql" >> "OutputLog.log"
sqlcmd -S %server% -d %db_name% -i "PaymentLogs.sql" >> "OutputLog.log"
sqlcmd -S %server% -d %db_name% -i "ProductLogs.sql" >> "OutputLog.log"