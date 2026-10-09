.PHONY: build test lint run publish msi all

SLN := MyApp.slnx

build:
	dotnet build $(SLN)

test:
	dotnet test --solution $(SLN)

lint:
	dotnet format $(SLN) --verify-no-changes
	CI=true dotnet build $(SLN) --no-incremental

run:
	dotnet run --project src/MyApp.App

publish:
	dotnet publish src/MyApp.App -c Release -o publish

msi: publish
	dotnet build installer/MyApp.Installer.wixproj -c Release -p:PublishDir=$(CURDIR)/publish

all: lint test
