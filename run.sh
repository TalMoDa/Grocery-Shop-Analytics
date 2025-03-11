#!/bin/bash

echo "🚀 Ensuring correct directory..."
cd "$(dirname "$0")"

echo "📦 Installing frontend dependencies..."
cd ShopAnalyticsClient && npm install && cd ..

echo "📦 Installing backend dependencies..."
cd ShopAnalytics/ShopAnalytics && dotnet restore && cd ../..

echo "⚙️  Building backend..."
cd ShopAnalytics/ShopAnalytics && dotnet build && cd ../..

echo "⚙️  Building frontend..."
cd ShopAnalyticsClient && npm run build && cd ..

echo "🚀 Starting both frontend & backend..."
cd ShopAnalyticsClient && npm run startAll
