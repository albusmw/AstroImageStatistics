# AstroImageStatistics
AstroImageStatistics (AIS) is a Windows Software Tool to calculate and process astronomical images, expecially with the focus of precise statistics and precise processing.

Key features:

- Verbose statistics of the image data
- "Zero-bin-size" histogram
- Image display including custom color maps for mono images
- FITSGrep function: Get all FITS file headers on a bunch of files and search all keyword entries, including filters
- Very fast processing due to the usage of the Intel IPP library

## Background

In all known software tool which can display a histogram, BIN's are formed in the histogram to display the statistics. Due to this binning, strange effects can be seen in the histogram which may be misleading, or error effects in the image data (caused e.g. due to software problems) may not be seen.
AIS displays the histogram of FITS files in "full resolution", means that e.g. a UInt16 FITS file (which are the most produced by astro imaging software) hat 65536 bins, so all effects can be seen.

## Screenshots

![MainWindow](screenshots\MainWindow.png)