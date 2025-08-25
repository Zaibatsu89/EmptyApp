#!/usr/bin/env pwsh

# Build script for .NET 10 MAUI applications
# This script builds both MauiApp1 and MauiApp2 for all supported platforms

param(
    [string]$Configuration = "Debug",
    [string]$Platform = "All",
    [switch]$Test = $false
)

Write-Host "=== .NET 10 MAUI Application Build Script ===" -ForegroundColor Green
Write-Host "Building applications for .NET 10..." -ForegroundColor Green

# Check if .NET 10 SDK is installed
$dotnetVersion = dotnet --version
Write-Host "Using .NET version: $dotnetVersion" -ForegroundColor Yellow

if ($dotnetVersion -notlike "10.0*") {
    Write-Host "Warning: .NET 10 SDK is required for this build!" -ForegroundColor Red
    Write-Host "Current version: $dotnetVersion" -ForegroundColor Red
    Write-Host "Please install .NET 10 SDK from: https://dotnet.microsoft.com/download/dotnet/10.0" -ForegroundColor Red
    exit 1
}

# Function to build a project
function Build-Project {
    param(
        [string]$ProjectPath,
        [string]$ProjectName
    )
    
    Write-Host "`n=== Building $ProjectName ===" -ForegroundColor Cyan
    
    # Navigate to the project directory
    Set-Location $ProjectPath
    
    # Clean previous builds
    Write-Host "Cleaning previous builds..." -ForegroundColor Yellow
    dotnet clean
    
    # Restore packages
    Write-Host "Restoring packages..." -ForegroundColor Yellow
    dotnet restore
    
    # Build the project
    Write-Host "Building project..." -ForegroundColor Yellow
    dotnet build --configuration $Configuration
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host "$ProjectName build completed successfully!" -ForegroundColor Green
        
        # List available frameworks
        Write-Host "`nAvailable target frameworks for $ProjectName" -ForegroundColor Cyan
        dotnet list $ProjectName.csproj package
        
        return $true
    } else {
        Write-Host "$ProjectName build failed!" -ForegroundColor Red
        return $false
    }
}

# Function to test a project
function Test-Project {
    param(
        [string]$ProjectPath,
        [string]$ProjectName
    )
    
    Write-Host "`n=== Testing $ProjectName ===" -ForegroundColor Cyan
    
    Set-Location $ProjectPath
    
    # Test Windows build
    Write-Host "Testing Windows build..." -ForegroundColor Yellow
    try {
        dotnet run --framework "net10.0-windows10.0.19041.0" --no-build
        Write-Host "$ProjectName Windows test completed!" -ForegroundColor Green
    } catch {
        Write-Host "$ProjectName Windows test failed or timed out!" -ForegroundColor Yellow
    }
}

# Build both projects
$mauiApp1Success = Build-Project "MauiApp1" "MauiApp1"
Set-Location ".."
$mauiApp2Success = Build-Project "MauiApp2" "MauiApp2"
Set-Location ".."

# Summary
Write-Host "`n=== Build Summary ===" -ForegroundColor Green
if ($mauiApp1Success) {
    Write-Host "MauiApp1: SUCCESS" -ForegroundColor Green
} else {
    Write-Host "MauiApp1: FAILED" -ForegroundColor Red
}

if ($mauiApp2Success) {
    Write-Host "MauiApp2: SUCCESS" -ForegroundColor Green
} else {
    Write-Host "MauiApp2: FAILED" -ForegroundColor Red
}

# Run tests if requested
if ($Test -and $mauiApp1Success -and $mauiApp2Success) {
    Write-Host "`n=== Running Application Tests ===" -ForegroundColor Green
    Test-Project "MauiApp1" "MauiApp1"
    Test-Project "MauiApp2" "MauiApp2"
}

# Final status
if ($mauiApp1Success -and $mauiApp2Success) {
    Write-Host "`nAll builds completed successfully!" -ForegroundColor Green
    Write-Host "Both applications are now running on .NET 10!" -ForegroundColor Green
    exit 0
} else {
    Write-Host "`nSome builds failed!" -ForegroundColor Red
    exit 1
}
