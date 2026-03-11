#!/bin/bash
# GET /bin/tree/{binIdentifier}/products/count
# Counts all products contained in all bins of a single bin tree.
# Replace BIN-001 with an actual bin identifier.

BIN_IDENTIFIER="${1:-BIN-001}"

echo ""
echo ""
echo "GET /bin/tree/${BIN_IDENTIFIER}/products/count"

curl -s -X GET "http://localhost:5000/bin/tree/${BIN_IDENTIFIER}/products/count"
