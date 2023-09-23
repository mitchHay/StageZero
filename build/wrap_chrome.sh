#!/bin/bash

CHROME_PATH=$(which chrome)
WRAPPER_PATH=$(readlink -f "$CHROME_PATH")
BASE_PATH="$WRAPPER_PATH-base"
mv "$WRAPPER_PATH" "$BASE_PATH"

cat > "$WRAPPER_PATH" <<_EOF
#!/bin/bash

# umask 002 ensures default permissions of files are 664 (rw-rw-r--) and directories are 775 (rwxrwxr-x).
umask 002

# Debian/Ubuntu seems to not respect --lang, it instead needs to be a LANGUAGE environment var
# See: https://stackoverflow.com/a/41893197/359999
for var in "\$@"; do
   if [[ \$var == --lang=* ]]; then
      LANGUAGE=\${var//--lang=}
   fi
done

# Set language environment variable
export LANGUAGE="\$LANGUAGE"

# Note: exec -a below is a bashism.
exec -a "\$0" "$BASE_PATH" --no-sandbox "\$@"
_EOF
chmod +x "$WRAPPER_PATH"