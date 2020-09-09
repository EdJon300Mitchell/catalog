HTML Doc is using JSDOc (defacto standard) JavaScript documentation format and jsdoc utils to generate basic help files

From node.js prompt (Visual Studio needs to have had node.js enabled), install tools:
cd Docs\
mkdir tools
cd tools\
npm install jsdoc
cd ..

mkdir HTML
tools\node_modules\.bin\jsdoc ..\Samples\C#\ExampleCatalog\Content\scripts\catalog-v1.0.1.js

Replace HTML\ files with files from out\
