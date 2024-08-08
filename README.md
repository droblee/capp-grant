# Portfolio Entry: Resolving the Utility Bill Delinquency Reporting Challenge for City Grant Funds

## Problem Statement
During the COVID-19 pandemic, the city allowed its citizens to delay payments on their electric utility bills, resulting in a significant monetary shortfall. The government introduced the CAPP (COVID-19 Arrears Payment Program) grant to help cities recover these funds. To qualify, the city needed to submit detailed documentation showing citizen delinquency from 03/04/2020 to 12/31/2021. This included the total delinquent amount for each citizen and the specific delinquency period. The city’s legacy ERP system did not have a built-in report for this purpose, and creating a custom report through the ERP vendor would cost approximately $10,000—an expense the city could not afford.

## Solution
Leveraging my data mining expertise, I devised a cost-effective solution to extract the required data from the city's legacy ERP system. The process involved:
1. Data Extraction:
	- Created a custom query to extract the necessary delinquency data.
	- Joined multiple tables to compile comprehensive data including citizen details, delinquency amounts, and delinquency periods.
1. Data Transformation:
	- Exported the extracted data into a spreadsheet.
	- Developed a custom macro within the spreadsheet to filter and calculate delinquent totals based on user-defined fields.
  
## Technical Implementation
1. Custom Query Development:
	- Identified relevant tables in the ERP database: customer details, billing history, and utility service.
	- Constructed SQL queries to join these tables and retrieve records for the specified period (03/04/2020 – 12/31/2021).
1. Spreadsheet and Macro Development:
	- Imported the extracted data into a spreadsheet.
	- Created a macro to automate the filtering and calculation of delinquent amounts.
  
## Results
* The city successfully applied for the CAPP grant, recovering a million dollars in lost funds.
* The custom solution avoided the $10,000 expense of creating a report through the ERP vendor.
* The project highlighted the value of data analytics in solving real-world problems cost-effectively and efficiently.

## Conclusion
This project underscores my ability to leverage data analytics to address complex challenges and deliver substantial cost savings. By extracting, transforming, and analyzing data from a variety systems, I can provide actionable insights and solutions that drive positive outcomes for organizations.

### Portfolio Summary
### Project Title: Resolving Utility Bill Delinquency Reporting Challenge
### Client: City
### Problem: Need to document citizen delinquency for a government grant application, with limited budget to pay for a custom report from the ERP vendor.
### Solution: Used data mining techniques to extract and process the required data, creating a custom spreadsheet with automated calculations.
### Result: Enabled the city to apply for and secure a million-dollar grant, saving $10,000 in potential costs.
### Technologies Used: Db2 SQL, Excel, VB

This repository been successfully replicated in a home lab using Linux and MariaDB, demonstrating my flexibility and applicability across different environments.
