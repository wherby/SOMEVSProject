import socket,time


class HeartbeatCient:
    def __init__(self,SERVER_IP,SERVER_PORT,BEAT_PERIOD=5):
        self.running=True
        self.SERVER_IP=SERVER_IP
        self.SERVER_PORT=SERVER_PORT
        self.BEAT_PERIOD=BEAT_PERIOD
        pass

    def run(self):
        print 'Sending heartbeat to IP %s, port %d'%(self.SERVER_IP,self.SERVER_PORT)
        print 'press Ctrl-C to stop'
        while self.running:
            self.hbSocket=socket.socket(socket.AF_INET,socket.SOCK_DGRAM)
            self.hbSocket.sendto('pyHB',(self.SERVER_IP,self.SERVER_PORT))
            if __debug__:
                print 'Time : %s' %time.time()
            time.sleep(self.BEAT_PERIOD)

def main():
    client=HeartbeatCient('172.20.200.29',43278)
    client.run()

if __name__=='__main__':
    main()
