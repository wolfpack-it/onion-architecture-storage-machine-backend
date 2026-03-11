#!/bin/bash
# GET /hello
# Returns a simple health-check message.

echo ""
echo ""
echo "GET /hello"

curl -s -X GET "http://localhost:5000/hello"
