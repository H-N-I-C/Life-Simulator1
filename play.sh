#!/bin/sh
set -eu
cd /home/user/Life-Simulator1
exec /usr/bin/python3 -E -s lifesim.py "$@"
