# Stereogram Solver CI Pipeline

This repository contains automated tests for the [Stereogram Solver](https://piellardj.github.io/stereogram-solver/).

## Solution Overview

- Using dotnet 9 (C#) + Selenium
- ChromeDriver
- Page Object Model
- Image comparison (using MagickImage)

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

## Implemented Test Cases

- Page is up and running
- Preset selection is working
- Preset results are correct
- Result comparison is working as intended
- Uploading custom stereogram is working and the result is correct
- Displacement slider is working and the edge case result is evaluated correctly

## Possible Additional Test Cases

- Canvas size
- Loading corrupted files / wrong formats / mime types
- Loading too small or big files
- Displacement slider min/max values
- Generation took too long

## Other Non-covered Testing Areas

- Performance - the speed of result generation.
We could start with a simple stopwatch. Or use browser devtools.
Since the speed is dependant on the client-side (JavaScript), we would have to ensure the same HW (resources) are used for measurements to follow the performance trends.

- Security - the application is using client-side computing and the files are not uploaded/stored anywhere so the risks are low.

- Usability - we could check the application accross different browsers, sizes and devices to see if it renders correctly, and that all the input elements are reachable. Keyboard controls.

- Exploratory - just go crazy. Try to simulate confused/reckless user.
Quickly change settings, spam button inputs, do long flows, etc.

## Other Possible Improvements

- Add logging
- Take screenshots of actual results
- Test runner parallelization
- Explicit waiting (ie. with additional JS flag when the canvas is ready)
