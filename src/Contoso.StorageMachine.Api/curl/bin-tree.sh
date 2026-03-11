#!/bin/bash
# GET /bin/tree/{binIdentifier}
# Returns a JSON representation of a single bin tree stored in the Storage Machine.
# Replace BIN-001 with an actual bin identifier.

BIN_IDENTIFIER="${1:-BIN-001}"

echo ""
echo ""
echo "GET /bin/tree/${BIN_IDENTIFIER}"

curl -s -X GET "http://localhost:5000/bin/tree/${BIN_IDENTIFIER}" \
     -H "Accept: application/json"
