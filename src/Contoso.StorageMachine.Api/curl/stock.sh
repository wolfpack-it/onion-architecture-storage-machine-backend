#!/bin/bash
# GET /stock
# Returns an overview of actual stock (non-empty bins) currently stored in the Storage Machine.

echo ""
echo ""
echo "GET /stock"

curl -s -X GET "http://localhost:5000/stock" \
     -H "Accept: application/json"
