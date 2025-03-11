Write-Host "🚀 Ensuring correct directory..."
Set-Location $PSScriptRoot

Write-Host "📦 Installing frontend dependencies..."
Set-Location "ShopAnalyticsClient"
npm install
Set-Location ..

Write-Host "📦 Installing backend dependencies..."
Set-Location "ShopAnalytics\ShopAnalytics"
dotnet restore
Set-Location ../..

Write-Host "⚙️  Building backend..."
Set-Location "ShopAnalytics\ShopAnalytics"
dotnet build
Set-Location ../..

Write-Host "⚙️  Building frontend..."
Set-Location "ShopAnalyticsClient"
npm run build
Set-Location ..

Write-Host "🚀 Starting both frontend & backend..."
Set-Location "ShopAnalyticsClient"
npm run startAll
