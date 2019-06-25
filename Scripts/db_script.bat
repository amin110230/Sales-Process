SET /p server= Enter SQL Server Name:
SET /p db_name= Enter DB Name:

CD "Tables"
DEL "OutputLog.log"
CALL Tables.bat

CD "../Procedures"
DEL "OutputLog.log"
FOR %%f in (*.sql) do sqlcmd -S %server% -d %db_name% /E /i "%%f" -o "OutputLog.log"

CD ".."