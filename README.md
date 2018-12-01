# SyncMeasure
SyncMeasure is a synchrony measurement tool that use the Leap Motion controller output files in order to measure synchronization between two individuals.



**Installation Prerequisites - Important!**

Install [R v3.4.0](https://cran.r-project.org/bin/windows/base/old/3.4.0/R-3.4.0-win.exe) **Exact version!**

Install [RTools](https://cran.r-project.org/bin/windows/Rtools/Rtools35.exe)

Run [SyncMeasureSetup.exe](https://github.com/Romansko/SyncMeasure/raw/master/Releases/SyncMeasureSetup.exe)

If setup asks you to install .NET 4.5.2 Frame work, you can get it from here: [.NET v4.5.2](https://download.microsoft.com/download/B/4/1/B4119C11-0423-477B-80EE-7A474314B347/NDP452-KB2901954-Web.exe)

Install within R Console:
```
install.packages("devtools")
library(devtools)
install_github("reissphil/cvv")
install.packages("data.table")
```

**Packages & DLLs**

- [LeapMotion SDK](https://developer.leapmotion.com/get-started/). LeapCSharp v4.5, LeapC.dll v4.

- [R.NET](https://www.nuget.org/packages/R.NET/) for integrating R language in .NET enviorment.

- [CVV package](https://github.com/reissphil/cvv) for computing cosine of velocity vectors (CVV), a measure of synchrony for 3D motion in dyads.

- [CircularProgressBar](https://www.nuget.org/packages/CircularProgressBar/) for the good looking circluar progress bar.

- [CyotekImageBox](https://www.nuget.org/packages/CyotekImageBox/) for Graph plots.

