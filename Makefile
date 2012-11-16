


all: windll lindll core

core: dll tests exe

lin: lindll core

win: windll core

exe:
	mcs GD-Demo/*.cs -t:exe -o bin/GD-Demo.exe -r:bin/GD-Sharp.dll

dll:
	mcs GD-Sharp/*.cs -t:library -r:bin/GD-Import.dll -o bin/GD-Sharp.dll

lindll:
	mcs GD-Import/*.cs -t:library -o bin/GD-Import.dll	
	cp bin/GD-Import.dll bin/Linux.GD-Import.dll

windll:
	mcs GD-Import/*.cs -t:library -o bin/GD-Import.dll -d:WINDOWS
	cp bin/GD-Import.dll bin/Windows.GD-Import.dll

tests:
	mcs GD-Test/*.cs -t:library -o bin/GD-Test.dll -r:bin/GD-Sharp.dll,nunit.framework

clean: 
	-rm bin/*

install: all
	cp bin/GD-Sharp.dll /usr/lib
	cp bin/GD-Import.dll /usr/lib

uninstall:
	rm /usr/lib/GD-Sharp.dll
	rm /usr/lib/GD-Import.dll
