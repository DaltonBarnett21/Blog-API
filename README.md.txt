Anytime you make changes to the DB modals, including the first migration run the 

1. add-migration migrationName

2. update-database -verbose

3. remove-migration migrationName       <--- this will revert the db to the previous migration
