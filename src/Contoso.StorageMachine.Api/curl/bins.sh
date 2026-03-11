#!/bin/bash
# GET /bins
# Returns an overview of all bins currently stored in the Storage Machine.

echo ""
echo ""
echo "GET /bins"

curl -s -X GET "http://localhost:5000/bins" \
     -H "Accept: application/json"
