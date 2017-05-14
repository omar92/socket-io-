@echo off
cls
title Command Prompt - by account3r2
ver
echo (C) Copyright Microsoft Corp.
echo.
:cmd
set /p "cmd=%cd%>"
%cmd%
echo.
goto cmd