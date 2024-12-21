#!/bin/bash
set -e

BACKUP_FILE=/restore/gar70.backup

echo "Waiting for database to be ready..."
until pg_isready -h gar70-db -p 5432 -U $GAR70_USER; do
  sleep 1
done

echo "Restoring database $GAR70_NAME from backup $BACKUP_FILE..."
pg_restore --no-owner --dbname=postgresql://${GAR70_USER}:${GAR70_PASSWORD}@gar70-db:5432/${GAR70_NAME} --clean --if-exists --verbose $BACKUP_FILE

echo "Database restored successfully!"