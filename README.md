# Sales Process Project Guideline
Cloning or downloading repository to local machine in order to start

## Setup Database

Create a database naming exactly **SalesProcess** in mssql server.
Then go to **Scripts** folder and run `db_script.bat`

Provide your *server* and *database(SalesProcess)* name. It will automatically generate tables from scripts in *Tables* folder and
procedures from *Procedures* folder

Then run `DataScript.sql` to insert some demo data into database for testing application

## Configure and Run Project
Open project in visual studio and then open *Server Explorer*. Create a connection of database for our application. Then copy connection
string and replace *connection string* in `Web.config`

Now build and run
