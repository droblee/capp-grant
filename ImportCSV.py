'''
=============================================
=============================================
 Purpose:
    Import data from CSV into database. Script
    will enumarate a folder for all CSV files.
    File name should match table name. Columns
    should also match.

 Author:
    David Roblee (droblee@gmail.com)

 Date:
    08/02/2024

 Version:
    1.0 - Initial script.

 Notes:
    Edit lines 62-65 for the database and line
    71 for the path to import files.
=============================================
=============================================
'''

# Import libraries
import os
import pandas as pd
import mysql.connector
from mysql.connector import Error
from getpass import getpass

# Function to connect to database
def funConnection(hostname, username, password, instance):
    dbConnection = None
    try:
        dbConnection = mysql.connector.connect(
            host=hostname,
            user=username,
            passwd=password,
            database=instance
        )
        print("Connection to database successful")
    except Error as e:
        print(f"The error '{e}' occurred")
    return dbConnection

# Function to read CSV files and insert data into database
def funInsertData(dbConnection, csvInsertFile, tableName):
    tableData = pd.read_csv(csvInsertFile)
    cols = "`,`".join([str(i) for i in tableData.columns.tolist()])

    for i, row in tableData.iterrows():
        row = row.where(pd.notnull(row), None)
        sql = f"INSERT INTO `{tableName}` (`" + cols + "`) VALUES (" + "%s,"*(len(row)-1) + "%s)"
        cursor = dbConnection.cursor()
        cursor.execute(sql, tuple(row))
        dbConnection.commit()
    print(f"Data from {csvInsertFile} inserted into {tableName} successfully")

# Database connection variables
dbHostname = "Server or IP"
dbUsername = "Username"
dbPassword = getpass("Enter database account password:")
dbInstance = "Database"

# Connect to the database
dbConnection = funConnection(dbHostname, dbUsername, dbPassword, dbInstance)

# Path to CSV files
csvPath = r"Path to import CSV files."

# Enumerate directory for CSV files and upload data
for csvFile in os.listdir(csvPath):
    if csvFile.endswith(".csv"):
        tableName = csvFile[:-4]
        csvInsertFile = os.path.join(csvPath, csvFile)
        funInsertData(dbConnection, csvInsertFile, tableName)

# Close the database dbConnection
if dbConnection.is_connected():
    dbConnection.close()
    print("Closed database connection")
