SETLOCAL ENABLEDELAYEDEXPANSION

SET SOLDIR=C:\Repos\ShippingCalculator\ShippingCalculator
SET OPENCOVER=%SOLDIR%\packages\OpenCover.4.6.166\tools\OpenCover.Console.exe
SET REPORTGEN=%SOLDIR%\packages\ReportGenerator.2.3.2.0\tools\ReportGenerator.exe
SET MSTEST=C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE\MSTest.exe
SET TESTDLL=C:\Repos\ShippingCalculator\ShippingCalculator\SuncrestShippingCalculator.Tests\bin\Debug\SuncrestShippingCalculator.Tests.dll
SET RESULTSFILE=%SOLDIR%\SuncrestShippingCalculator.trx
SET TARGETARGS=/testcontainer:%TESTDLL% /resultsfile:%RESULTSFILE%
SET OUTXML=C:\temp\coverage.xml
SET OUTHTMLDIR=C:\Temp\ShippingCalculatorCoverage
SET NS=
SET FILTER=+[SuncrestShippingCalculator]* -[SuncrestShippingCalculator.Tests]* -[*]*Config -[*]*MvcApplication

IF NOT EXIST %OUTHTMLDIR% mkdir %OUTHTMLDIR%

:: Get rid of leftover results file(s)
IF EXIST %RESULTSFILE% del %RESULTSFILE%
PUSHD C:\Repos\ShippingCalculator\ShippingCalculator\SuncrestShippingCalculator\bin

:: Run OpenCover using the specified parameters
%OPENCOVER% -register:user -mergebyhash -skipautoprops -target:"%MSTEST%" -targetargs:"%TARGETARGS%" -output:%OUTXML% -filter:"%FILTER%"
IF %ERRORLEVEL% NEQ 0 SET _ERROR_ENCOUNTERED=1

:: Run ReportGenerator if OpenCover ran successfully
IF NOT DEFINED _ERROR_ENCOUNTERED (
	%REPORTGEN% -reports:%OUTXML% -targetdir:%OUTHTMLDIR%
	IF !ERRORLEVEL! NEQ 0 SET _ERROR_ENCOUNTERED=1
)

:: Clean up temporary files
FOR /D /R %%X IN (%USERNAME%*) DO RD /S /Q "%%X"
POPD

IF NOT DEFINED _ERROR_ENCOUNTERED (
	START "Code Coverage Report" %OUTHTMLDIR%\index.htm
)

:: see http://www.allenconway.net/2015/06/using-opencover-and-reportgenerator-to.html
:: START "Code Coverage Report" C:\Temp\ShippingCalculatorCoverage\index.htm