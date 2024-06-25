@ECHO off
SETLOCAL

set MODELCOMPILER=..\..\..\build\bin\Release\net8.0\Opc.Ua.ModelCompiler.exe
set MODEL=DemoModel
set VERSION=v105
set EXCLUDE=Draft
set INPUT=.
set CSVINPUT=.
set OUTPUT=.
set USEALLOWSUBTYPES=-useAllowSubtypes

ECHO Building Model %MODEL%
ECHO %MODELCOMPILER% compile -version %VERSION% -exclude %EXCLUDE% -d2 "%INPUT%\%MODEL%.xml" -cg "%CSVINPUT%\%MODEL%.csv" -o2 "%OUTPUT%" %USEALLOWSUBTYPES%
%MODELCOMPILER% compile -version %VERSION% -exclude %EXCLUDE% -d2 "%INPUT%\%MODEL%.xml" -cg "%CSVINPUT%\%MODEL%.csv" -o2 "%OUTPUT%" %USEALLOWSUBTYPES%
IF %ERRORLEVEL% NEQ 0 ( ECHO Failed %MODEL% & EXIT /B 3 )