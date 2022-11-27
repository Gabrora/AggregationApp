main flow:
- on startup application ensures that database 'db' is created.
- if 'AggregatedElectricities' table is empty, application will start the flow of fetching the CSVs
- CSVS fetching process will take some time, because of the large size of the data
- after fetching, the data will get filtered and aggregated
- fineally the data will be stored in the database 
- API Get will now be able to return the data. url : http://localhost:8081/electricity

the applicationn should have no problems, every part of the assignment is done.

