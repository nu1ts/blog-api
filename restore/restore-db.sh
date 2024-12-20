#!/bin/bash
set -e

BACKUP_FILE=/docker-entrypoint-initdb.d/gar70.backup

echo "Restoring database $GAR70_NAME from backup $BACKUP_FILE..."

pg_restore --no-owner --dbname=${GAR70_NAME} --username=${GAR70_USER} --clean --if-exists --verbose $BACKUP_FILE

echo "Database restored successfully!"