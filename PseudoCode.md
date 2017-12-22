
# PseudoCode


## Extraction System

### Current approach

- Files are sorted into String Array of ValidRar,
										ValidZip,
										Invalid
If the lists contain Files. Extraction STATE is SET to respective EXTENSION. 
The STATE will decide what EXTRACTION to call.

- Why is this approach bad?
	The LAST extension set will determine the extraction method, skipping files.

- Solution
	The Valid Lists will be queued and sorted into respective method.

### New Approach

- Lists are sorted into String Array of ValidRar,
										Validzip,
										Invalid,

IF (ValidRar.Length > 0) CALL Unrar(ValidRar)
IF (ValidZip.Length > 0) CALL Unzip(ValidZip)

