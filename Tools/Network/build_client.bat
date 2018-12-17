echo off
set curPath=%cd%
set destPath=%curPath:Tools:\proto=Client\Assets\Scripts\Network\Protos%
echo %destPath%

google\bin\protoc.exe --csharp_out=%destPath% Test.proto

pause
echo "Press any key to exit!"