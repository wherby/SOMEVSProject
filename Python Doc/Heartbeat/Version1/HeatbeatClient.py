import socket,time
SERVER_IP='172.20.200.29';SERVER_PORT=43278;BEAT_PERIOD=5
print 'Sending heartbeat to IP %s, port %d'%(SERVER_IP,SERVER_PORT)
print 'press Ctrl-C to stop'
while True:
    hbSocket=socket.socket(socket.AF_INET,socket.SOCK_DGRAM)
    hbSocket.sendto('pyHB',(SERVER_IP,SERVER_PORT))
    if __debug__:
        print 'Tome : %s' %time.time()
    time.sleep(BEAT_PERIOD)
