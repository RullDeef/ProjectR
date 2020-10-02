#!/bin/bash

doxygen ./Doxyfile
cd ./latex/
cmd < make.bat
mv ./refman.pdf ../refman.pdf
cd ..
