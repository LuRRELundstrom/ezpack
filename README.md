# ezpack
Drag &amp; Drop console app for decompressing files.  
A beginners learning project, using SharpCompress.

## Todo

* Command-line input post startup.
* Progress tracking.
* ExceptionHandlers.
* Port to Async with events.
* GUI.
* Extraction queue.(Fixing simultaneous extracting.)

## Changelog
   
### Known issues
* Simultaneous rar and zip extraction not working.


### 0.1.4
* Corrupt files throw an exception and exits without crashing.
* Valid extensions are no longer case sensitive.
* Fixed printsize inconsistency.

### 0.1.3 
* Filter and print excluded files.
* Rar enabled.

### 0.1.2
* Added Overwrite permission for Zip extraction(fixed file exists exception).
* Filtering of input parameters.
* Changed Projectname.
* Rar temporary disabled.

### 0.1.1
* Added Multi zip-support.
* Added support for several multi-volumes. (FileAccess overwrite)
