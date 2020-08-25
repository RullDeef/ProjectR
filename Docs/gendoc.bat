doxygen .\Doxyfile
cd .\latex\
CALL make.bat
move .\refman.pdf ..\refman.pdf
