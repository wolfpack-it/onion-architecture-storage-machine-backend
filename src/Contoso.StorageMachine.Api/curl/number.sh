#!/bin/bash
# POST /number
# Accepts a single integer in the JSON body.
# Returns 200 for odd numbers, 406 for even numbers, 400 for non-numeric input.

echo ""
echo ""
echo "POST /number"

echo "--- odd number (expects 200) ---"
curl -s -X POST "http://localhost:5000/number" \
     -H "Content-Type: application/json" \
     -d "7"

echo ""
echo "--- even number (expects 406) ---"
curl -s -X POST "http://localhost:5000/number" \
     -H "Content-Type: application/json" \
     -d "4"

echo ""
echo "--- invalid body (expects 400) ---"
curl -s -X POST "http://localhost:5000/number" \
     -H "Content-Type: application/json" \
     -d '"not-a-number"'
