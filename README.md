# SyncMeasure
SyncMeasure is a synchrony measurement tool that use the Leap Motion controller output files in order to measure synchronization between two individuals.

===============================================================================

**Installation Prerequisites - Important!**

1. Install [R v3.4.0](https://cran.r-project.org/bin/windows/base/old/3.4.0/R-3.4.0-win.exe) **Exact version!**

2. Install [RTools](https://cran.r-project.org/bin/windows/Rtools/Rtools35.exe)

3. Open R Console and install the following packages as below:
```
install.packages("devtools")
library(devtools)
install_github("reissphil/cvv")
install.packages("data.table")
```
*Ignore warnings about packages that was built under R version 3.4.4. CVV package require R v3.4.0 and won't work otherwise.*

4. Install [SyncMeasureSetup.exe](https://github.com/Romansko/SyncMeasure/raw/master/Releases/SyncMeasureSetup.exe)

*If .NET 4.5.2 Framework is required during the setup, you can install it from:* [.NET v4.5.2](https://download.microsoft.com/download/B/4/1/B4119C11-0423-477B-80EE-7A474314B347/NDP452-KB2901954-Web.exe). *After .NET framework installation, Install SyncMeasure.*

===============================================================================

**Synchronization Parameters**

- "Average" - Calculation of all parameters considering weights as defined by the user.
- CVV: As described in [CVV package](https://github.com/reissphil/cvv). Can be a negative value.
- Grab and Pinch strength: The measurement is `1 - difference between hands`.

===============================================================================

**Packages used**

- [R.NET](https://www.nuget.org/packages/R.NET/) for integrating R language in .NET enviorment.

- [CVV package](https://github.com/reissphil/cvv) for computing cosine of velocity vectors (CVV), a measure of synchrony for 3D motion in dyads.

- [CircularProgressBar](https://www.nuget.org/packages/CircularProgressBar/) for the good looking circluar progress bar.

- [CyotekImageBox](https://www.nuget.org/packages/CyotekImageBox/) for Graph plots.

