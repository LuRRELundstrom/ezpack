# ezpack
Drag &amp; Drop console app for decompressing files.  
A beginners learning project, using SharpCompress by Adamhathcock.

## Todo

* Rewrite Displayed Results. Size, text formatting. (0.1.6)
* Progress tracking.
* ExceptionHandlers.
* Port to Async with events. (0.2.0)
* GUI. (0.3.0)

## Changelog
   
### Known issues
* Incorrect results displayed when mixing zip and invalid files.

### 0.1.5
* Add - Command-line input post startup.
* Add - Extraction queue. (Priority = Rar)
* Fix - Extraction text formatting.

### 0.1.4
* Add - Corrupt files throw an exception and exits instead of crashing.
* Fix - Valid extensions are no longer case sensitive.
* Fix - Size calculations inconsistent.

### 0.1.3 
* Add - Filter and print excluded files.
* Add - Rar support enabled.

### 0.1.2
* Add - Overwrite permission for Zip extraction(fixed file exists exception).
* Add - Filtering of input parameters.
* Fix - Changed Projectname.
* Rem - Rar support temporary disabled.

### 0.1.1
* Add - Multi zip-support.
* Add - Support for several multi-volumes. (FileAccess overwrite)
