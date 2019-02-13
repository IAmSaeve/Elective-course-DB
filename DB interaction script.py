#!/usr/bin/env python3

import pymssql
conn = pymssql.connect(server='generalsqlserver.database.windows.net', user='Sebastian@generalsqlserver', password='P@ssw0rd', database='devdb')
cursor = conn.cursor()


cursor.execute('SELECT * FROM FirstTestTable')
row = cursor.fetchone()




while row:  
    print (row)
    row = cursor.fetchone()  