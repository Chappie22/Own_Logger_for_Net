**Logger for .Net** \
Options, what are available using lib: \
-> ID (Counter of each log); \
-> Datetime; \
-> Log level: *(ALL, DEBUG, INFO, WARN, ERROR, FATAL, OFF, TRACE);*\
-> Log message;\
-> Posibility to change the sequence.\
\
**Obligatory things:\
1st obligatory step (Setup the file, which will be created and used for logs) = Logger.FileSetup(string filePath, string fileName); \
...(Some logs) e.g. Logger.WARN("This is a warn message"); \
2nd obligatory step (Safely close the file stream, used for logs) = Logger.Close();**
\


*Log description: \
ALL	All levels including custom levels.\
DEBUG	Designates fine-grained informational events that are most useful to debug an application.\
INFO	Designates informational messages that highlight the progress of the application at coarse-grained level.\
WARN	Designates potentially harmful situations.\
ERROR	Designates error events that might still allow the application to continue running.\
FATAL	Designates very severe error events that will presumably lead the application to abort.\
OFF	The highest possible rank and is intended to turn off logging.\
TRACE	Designates finer-grained informational events than the DEBUG.\
