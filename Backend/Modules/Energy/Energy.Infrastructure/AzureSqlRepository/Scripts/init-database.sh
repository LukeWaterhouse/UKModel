#!/bin/bash

# EF Core Database Initialization Script for UKModel
# This script ensures the database is created and all migrations are applied
# Usage: ./scripts/init-database.sh

set -e

echo "🚀 UKModel Database Initialization Script"
echo "=========================================="

# Navigate to Backend directory
cd "$(dirname "$0")/../../../../../"

echo ""
echo "📦 Building solution..."
dotnet build --configuration Release

echo ""
echo "🔧 Ensuring database exists and applying Energy migrations..."
dotnet ef database update \
    --context EnergyDbContext \
    --project Modules/Energy/Energy.Infrastructure \
    --startup-project UKModel \
    --verbose

echo ""
echo "📊 Listing applied migrations for Energy..."
echo "Energy Database Migrations:"
dotnet ef migrations list \
    --context EnergyDbContext \
    --project Modules/Energy/Energy.Infrastructure \
    --startup-project UKModel

echo ""
echo "🔍 Verifying database connection..."
echo "Energy Database Info:"
dotnet ef dbcontext info \
    --context EnergyDbContext \
    --project Modules/Energy/Energy.Infrastructure \
    --startup-project UKModel

echo ""
echo "✅ Database initialization completed successfully!"
echo ""
echo "📋 Summary:"
echo "   • Built solution in Release configuration"
echo "   • Created database if it didn't exist"
echo "   • Applied all pending migrations for EnergyDbContext"
echo "   • Verified database connection"
echo ""
echo "Your UKModel database is now ready for use! 🎉"
