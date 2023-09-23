#!/bin/bash
# Adapted from https://github.com/SeleniumHQ/docker-selenium/blob/trunk/NodeChrome/wrap_chrome_binary

WRAPPER_PATH=$(readlink -f /opt/google/chrome/google-chrome)
BASE_PATH="$WRAPPER_PATH-base"
mv "$WRAPPER_PATH" "$BASE_PATH"

cat > "$WRAPPER_PATH" <<_EOF
#!/bin/bash

# umask 002 ensures default permissions of files are 664 (rw-rw-r--) and directories are 775 (rwxrwxr-x).
umask 002

# Note: exec -a below is a bashism.
exec -a "\$0" "$BASE_PATH" --no-sandbox "\$@"
_EOF
chmod +x "$WRAPPER_PATH"