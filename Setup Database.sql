/*
=============================================
=============================================
 Purpose:
    Create a mockup MariaDB database for CAPP
    Grant project.

 Author:
    David Roblee (droblee@gmail.com)

 Date:
    08/02/2024

 Version:
    1.0 - Initial database and queries
=============================================
=============================================

=============================================
 Setup database and associated tables
*/
-- Create database
CREATE DATABASE CAPP_Grant;

-- Set database and create tables
USE CAPP_Grant;

-- Customers
CREATE TABLE Customers (
    CustomerID INT AUTO_INCREMENT PRIMARY KEY,
    FirstName VARCHAR(100) NOT NULL,
    LastName VARCHAR(100) NOT NULL,
    Email VARCHAR(255),
    Phone VARCHAR(20),
    LocationID INT NOT NULL,
    ElectricID INT NOT NULL);

-- Addresses
CREATE TABLE Addresses (
    AddressID INT AUTO_INCREMENT PRIMARY KEY,
    CustomerID INT NOT NULL,
    AddressLine1 VARCHAR(255) NOT NULL,
    AddressLine2 VARCHAR(255),
    City VARCHAR(100) NOT NULL,
    State VARCHAR(100) NOT NULL,
    ZipCode VARCHAR(20) NOT NULL,
    Country VARCHAR(100) NOT NULL
) AUTO_INCREMENT=101;

-- Electric
CREATE TABLE Electric ( 
	ElectricID INT AUTO_INCREMENT PRIMARY KEY,
	Name VARCHAR(255) NOT NULL,
	BaseCharge DECIMAL(10, 2) NOT NULL,
	Tier INT NOT NULL,
	Charge DECIMAL(10, 2) NOT NULL,
	Rate DECIMAL(10, 4) NOT NULL
) AUTO_INCREMENT=201;

-- Billing
CREATE TABLE Billing (
    BillID INT AUTO_INCREMENT PRIMARY KEY,
    CustomerID INT NOT NULL,
    LocationID INT NOT NULL,
    ElectricID INT NOT NULL,
    MeterID VARCHAR(50) NOT NULL,
    CurrentReading DECIMAL(10, 2) NOT NULL,
    PriorReading DECIMAL(10, 2) NOT NULL,
    Credits DECIMAL(10, 2),
    BillDate DATE NOT NULL,
    PastDue DECIMAL(10, 2),
    PriorBalance DECIMAL(10, 2)
) AUTO_INCREMENT=301;

/*
=============================================
 Create a view for delinquent accounts.
 Criteria:
    -Currently past due
    -Bill date between 03/01/2020 - 12/31/2021
 Return:
    -Customer ID
    -Full Name
    -Email
    -Phone
    -Address
    -Electric Tier
    -Bill Date
    -Balance Due
*/
CREATE VIEW DelinquentCustomers AS
SELECT
	c.CustomerID AS 'Customer ID',
	CONCAT(c.FirstName, ' ', c.LastName) AS 'Full Name',
	c.Email,
	c.Phone,
	CONCAT (
		a.AddressLine1,
		IF(a.AddressLine2 IS NOT NULL AND a.AddressLine2 != '', CONCAT(', ', a.AddressLine2, ', '), ', '), 
		a.City, ', ',
		a.State, ', ',
		a.ZipCode) AS 'Address',
	e.Name AS 'Electric Tier',
	b.BillDate AS 'Bill Date',
	MAX(b.PriorBalance - b.Credits) AS 'Balance Due'
FROM
	Customers c
	INNER JOIN Addresses a ON c.CustomerID = a.CustomerID
	INNER JOIN Electric e ON c.ElectricID = e.ElectricID
	INNER JOIN Billing b ON c.CustomerID = b.CustomerID
WHERE
	b.PastDue > 0 AND
	b.BillDate BETWEEN '2020-03-01' AND '2021-12-31'
GROUP BY
	c.CustomerID,
    b.BillDate;

/*
=============================================
 Query view for data export
*/
SELECT * FROM DelinquentCustomers;