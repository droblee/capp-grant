'=============================================
'=============================================
' Purpose:
'    These code blocks are for the macros
'    found in the "CAPP Report.xlsm" file.
'    File was saved in VB for code highlights.

' Author:
'    David Roblee (droblee@gmail.com)

' Date:
'    08/02/2024

' Version:
'    1.0 - Initial script.

' Notes:
'    This file is not meant to be ran. It is
'    only to display the macros used in the
'    report file.
'=============================================
'=============================================

Sub Clear()
    ' Delcaare variables
    Dim wsReporting As Worksheet
    Dim wsResults As Worksheet

    ' Assign sheet
    Set wsReporting = ThisWorkbook.Sheets("Reporting")

    ' Set all checkboxes (D7 to D13) to TRUE
    wsReporting.Range("D7:D12").Value = True
    
    ' Set the start date to 03/01/2020 and end date to 01/01/2022
    wsReporting.Range("D15").Value = DateValue("03/01/2020")
    wsReporting.Range("D17").Value = DateValue("01/01/2022")
    
    ' Set total delinquent and total customers cells to blank
    wsReporting.Range("E20").Value = ""
    wsReporting.Range("E22").Value = ""
    
    ' Delete existing Results sheet if it exists
    On Error Resume Next
    Application.DisplayAlerts = False
    Set wsResults = ThisWorkbook.Sheets("Results")
    If Not wsResults Is Nothing Then wsResults.Delete
    Application.DisplayAlerts = True
    On Error GoTo 0
End Sub

Sub RunReport()
    ' Declare variables
    Dim wsReporting As Worksheet
    Dim wsData As Worksheet
    Dim wsResults As Worksheet
    Dim startDate As Date
    Dim endDate As Date
    Dim dataRow As Range
    Dim outputRow As Integer
    Dim balanceSum As Double
    Dim uniqueCustomers As Collection
    Dim selectedColumns As Collection
    Dim colIndex As Integer
    Dim colOffset As Integer
    Dim col As Variant

    ' Assign worksheets
    Set wsReporting = ThisWorkbook.Sheets("Reporting")
    Set wsData = ThisWorkbook.Sheets("Data")
    
    ' Set total delinquent and total customers cells to blank
    wsReporting.Range("E20").Value = ""
    wsReporting.Range("E22").Value = ""
    
    ' Delete existing Results sheet if it exists
    On Error Resume Next
    Application.DisplayAlerts = False
    ThisWorkbook.Sheets("Results").Delete
    Application.DisplayAlerts = True
    On Error GoTo 0
    
    ' Assign variables
    Set uniqueCustomers = New Collection
    Set selectedColumns = New Collection

    ' Get date range from form
    startDate = wsReporting.Range("D15").Value
    endDate = wsReporting.Range("D17").Value
    
    ' Check D7 to D11 for TRUE values and add corresponding columns to the collection
    If wsReporting.Range("D7").Value = True Then selectedColumns.Add "Customer ID"
    If wsReporting.Range("D8").Value = True Then selectedColumns.Add "Full Name"
    If wsReporting.Range("D9").Value = True Then selectedColumns.Add "Email"
    If wsReporting.Range("D10").Value = True Then selectedColumns.Add "Phone"
    If wsReporting.Range("D11").Value = True Then selectedColumns.Add "Address"
    If wsReporting.Range("D12").Value = True Then selectedColumns.Add "Electric Tier"

    ' Always include "Bill Date" and "Balance Due"
    selectedColumns.Add "Bill Date"
    selectedColumns.Add "Balance Due"

    ' Prompt if no fields are selected
    If selectedColumns.Count = 2 Then
        MsgBox "No fields selected. Please select fields and re-run report."
    Else
        ' Create new Results sheet
        Set wsResults = ThisWorkbook.Sheets.Add(After:=ThisWorkbook.Sheets(ThisWorkbook.Sheets.Count))
        wsResults.Name = "Results"
    
        ' Add headers to the Results sheet
        colOffset = 0
        For Each col In selectedColumns
            wsResults.Cells(1, 1 + colOffset).Value = col
            colOffset = colOffset + 1
        Next col
    
        ' Filter data based on date range and selected columns
        balanceSum = 0
        outputRow = 2
        For Each dataRow In wsData.Range("A2:H" & wsData.Cells(wsData.Rows.Count, "A").End(xlUp).Row).Rows
            If dataRow.Cells(1, 7).Value >= startDate And dataRow.Cells(1, 7).Value <= endDate Then
                ' Copy the data if columns are selected
                colOffset = 0
                For Each col In selectedColumns
                    Select Case col
                        Case "Customer ID"
                            colIndex = 1
                        Case "Full Name"
                            colIndex = 2
                        Case "Email"
                            colIndex = 3
                        Case "Phone"
                            colIndex = 4
                        Case "Address"
                            colIndex = 5
                        Case "Electric Tier"
                            colIndex = 6
                        Case "Bill Date"
                            colIndex = 7
                        Case "Balance Due"
                            colIndex = 8
                    End Select
                    wsResults.Cells(outputRow, 1 + colOffset).Value = dataRow.Cells(1, colIndex).Value
                    colOffset = colOffset + 1
                Next col
                
                balanceSum = balanceSum + dataRow.Cells(1, 8).Value
                On Error Resume Next
                uniqueCustomers.Add dataRow.Cells(1, 1).Value, CStr(dataRow.Cells(1, 1).Value)
                On Error GoTo 0
                outputRow = outputRow + 1
            End If
        Next dataRow
        
        ' Auto-fit the columns
        wsResults.Columns.AutoFit
        
        ' Update summary cells in the Form sheet
        wsReporting.Range("E20").Value = balanceSum
        wsReporting.Range("E22").Value = uniqueCustomers.Count
        
        ' Switch back to the Form sheet
        wsReporting.Activate
    End If
End Sub
