REM Run  all test scripts.  Assumes reposityr is intially empty

node Create First
node Create error
node Create Third
node Create Second
node Delete 101
node Update 102 Second
node Update 103 Third
node Read
