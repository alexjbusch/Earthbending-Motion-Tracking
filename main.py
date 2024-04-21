#pipe server
from body import BodyThread
import time
import struct
import global_vars
from sys import exit



def signal_handler(sig, frame):
    print("Exiting…")
    global_vars.KILL_THREADS = True
    time.sleep(0.5)
    exit()
import signal
signal.signal(signal.SIGINT, signal_handler)
signal.signal(signal.SIGTERM, signal_handler)


thread = BodyThread()
thread.start()


while True:
    try:
        i = input()
    except KeyboardInterrupt:
        signal_handler(signal.SIGINT, None)
"""
i = input()
"""
print("Exiting…")        
global_vars.KILL_THREADS = True
time.sleep(0.5)
exit()