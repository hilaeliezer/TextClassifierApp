# TextClassifier_Hila_Eliezer
## TextClassiferApp is a .netCore 2.0 console app that classify files to domains by rules 
### The app expect 2 input arguments and return as output the classified list of domains 
1. __path in the filesystem__  representing a file or a folder that should be scanned 
2. __json file path__  representing the list of classification rules

- __Import and save files from GitHub__ [https://github.com/Mcas-Interviews/TextClassifier_Hila_Eliezer.git]
- __How to run the app:__
1.open cmd
2. open repository where the app located
3. run command : **dotnet** **TextClassifierApp.dll** **arg1** **arg2**
*for example:* ...\TextClassifier_Hila_Eliezer\TextClassifierApp\bin\Debug\netcoreapp2.0>dotnet TextClassifierApp.dll arg1 arg2
arg1: txt/csv local file path
arg2: json classificationRuls 
## Features
- The  app support  csv or text file classification