#!/usr/bin/env pwsh

# Build script for .NET 10 MAUI application
# This script builds the application for all supported platforms

param(
    [string]$Configuration = "Debug",
    [string]$Platform = "All"
)

Write-Host "Building MauiApp1 for .NET 10..." -ForegroundColor Green

# Check if .NET 10 SDK is installed
$dotnetVersion = dotnet --version
Write-Host "Using .NET version: $dotnetVersion" -ForegroundColor Yellow

# Navigate to the project directory
Set-Location "MauiApp1"

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
    Write-Host "Build completed successfully!" -ForegroundColor Green
    
    # List available frameworks
    Write-Host "`nAvailable target frameworks:" -ForegroundColor Cyan
    dotnet list MauiApp1.csproj package
    
    # Show package information
    Write-Host "`nPackage information:" -ForegroundColor Cyan
    dotnet list package
    
} else {
    Write-Host "Build failed!" -ForegroundColor Red
    exit 1
}

Write-Host "`nBuild script completed." -ForegroundColor Green
