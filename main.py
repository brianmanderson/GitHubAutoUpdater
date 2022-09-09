import os
import time
paths = []
fid = open(os.path.join('.', 'Paths.txt'))
for line in fid:
    paths.append(line.strip('\n'))
fid.close()
for path in paths:
    if os.path.exists(path):
        for root, dirs, files in os.walk(path):
