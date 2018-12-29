:: https://www.jianshu.com/p/48c71e7359d3
:: https://www.cnblogs.com/DswCnblog/p/5432326.html

@echo off
set curPath=%cd%
echo curPath = %curPath%

set sourcePath=%curPath:Tools\Network=Resources\Protos%
echo sourcePath = %sourcePath%

set destPath=%curPath:Tools\Network=Server\Server\Scripts\Network\Protos%
echo destPath = %destPath%

google\bin\protoc.exe -I=%sourcePath% --csharp_out=%destPath% Common.proto
google\bin\protoc.exe -I=%sourcePath% --csharp_out=%destPath% Mail.proto
google\bin\protoc.exe -I=%sourcePath% --csharp_out=%destPath% Login.proto

pause
echo "Press any key to exit!"