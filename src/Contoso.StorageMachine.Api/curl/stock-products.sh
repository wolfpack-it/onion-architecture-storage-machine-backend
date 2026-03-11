#!/bin/bash
# GET /stock/products
# Returns an overview of all products stored in the Storage Machine,
# regardless of which bins contain them.

echo ""
echo ""
echo "GET /stock/products"

curl -s -X GET "http://localhost:5000/stock/products" \
     -H "Accept: application/json"
