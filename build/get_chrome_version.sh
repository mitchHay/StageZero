#!/bin/bash

echo "Chrome location:" $(which chrome)

CHROME_VERSION=$(chrome --version)
CHROME_VERSION_ARR=($CHROME_VERSION) # Outputs as [ Google, Chrome, <version> ]
OUTPUT_VERSION=${CHROME_VERSION_ARR[2]}

echo "Chrome version:" $OUTPUT_VERSION

# Output to Github actions env var
echo "CHROME_VERSION=$OUTPUT_VERSION" >> "$GITHUB_ENV"
