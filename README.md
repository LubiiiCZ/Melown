# Stereogram Solver CI Pipeline

This repository contains automated tests for the [Stereogram Solver](https://piellardj.github.io/stereogram-solver/).

## Solution Overview

- Using dotnet 9 (C#) + Selenium
- Chromedriver
- Page Object Model

## Pipeline Overview

- Using ubuntu-latest + dotnet 9
- Install Chrome
- Restore + Build
- Run tests and store the trx file
- Convert the trx file to HTML using trxlog2html tool
- Upload the HTML report as an artifact

## Triggers

- Every push
- Every PR
- Manually

## To Run the Tests Locally

- Install .NET 9 SDK
- Clone the repo

```bash
dotnet restore
dotnet build
dotnet dotnet test --logger "trx;LogFileName=test-results.trx"
```

## To Generate the Report Locally

- Install trxlog2html Tool (if needed)
- Generate the report

```bash
dotnet tool install --global trxlog2html
trxlog2html -i TestResults/test-results.trx -o report.html
```
