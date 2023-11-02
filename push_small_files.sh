#!/bin/bash

# Set the maximum allowed file size (in bytes)
MAX_SIZE=$((100 * 1024 * 1024))

# Function to add and commit files smaller than 
the specified size
add_and_commit() {
    for file in $(git ls-files --others 
--exclude-standard); do
        # Check if the file size is less than the 
maximum allowed size
        if [ $(stat -c%s "$file") -lt $MAX_SIZE ]; 
then
            git add "$file"
            git commit -m "Add file: $file"
        else
            echo "Skipping $file (file size 
exceeds 100 MB)"
        fi
    done
}

# Add and commit files
add_and_commit

# Push the committed files to the remote 
repository
git push origin main

echo "Pushed all files smaller than 100 MB to the 
remote repository."
