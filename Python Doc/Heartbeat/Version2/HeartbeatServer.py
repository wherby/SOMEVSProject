import socket,threading,time

UDP_PORT=43278; CHECK_PERIOD=20;CHECK_TIMEOUT=15

class Heartbeats(dict):
    def __init__(self):
        super(Heartbeats,self).__init__()
        self._lock=threading.Lock()

    def __setitem__(self,key,value):
        self._lock.acquire()
        #print "new Client comes"+str(key)+" "+str(value)
        try:
            super(Heartbeats,self).__setitem__(key,value)
        finally:
            self._lock.release()

    def getSilent(self):
        limit=time.time()-CHECK_TIMEOUT
        self._lock.acquire()
        try:
            silent=[ip for (ip,ipTime) in self.items() if ipTime<limit]
        finally:
            self._lock.release()
        return silent

class Receiver(threading.Thread):
    def __init__(self,goOnEvent,heartbeats):
        super(Receiver,self).__init__()
        self.goOnEvent=goOnEvent
        self.heartbeats=heartbeats
        self.recSocket=socket.socket(socket.AF_INET,socket.SOCK_DGRAM)
        self.recSocket.settimeout(CHECK_TIMEOUT)
        self.recSocket.bind(("",UDP_PORT))

    def run(self):
        #print "running in receiver...."
        while self.goOnEvent.isSet():
            #print "Try to get data"
            try:
                data, addr=self.recSocket.recvfrom(5)
                #print "Get data :"+str(data)+" - "+str(addr)
                if data=='pyHB':
                    self.heartbeats[addr[0]]=time.time()
            except socket.timeout:
            
                #print "failed to get data"
                pass

class ServerSetUp:
    def __init__(self,num_receivers=1):
        self.receiverEvent=threading.Event()
        self.receiverEvent.set()
        self.heartbeats=Heartbeats()
        self.receivers=[]
        for i in range(num_receivers):
            receiver=Receiver(goOnEvent=self.receiverEvent,heartbeats=self.heartbeats)
            receiver.start()
            self.receivers.append(receiver)
        print 'Threaded heartbeat server listrning on port %d' % UDP_PORT
        print 'Press Ctrl-C to stop'
        self.running=True
        pass

    def run(self):
        try:
            while self.running:
                silent=self.heartbeats.getSilent()
                print 'Silent client: %s' % silent
                time.sleep(CHECK_PERIOD)

        except KeyboardInterrupt:
            print 'Exiting, please wait...'
            self.receiverEvent.clear()
            for receive in self.receivers:
                receive.join()
            print 'Finished.'


def main(num_receivers=3):
    receiverEvent=threading.Event()
    receiverEvent.set()
    heartbeats=Heartbeats()
    receivers=[]
    for i in range(num_receivers):
        receiver=Receiver(goOnEvent=receiverEvent,heartbeats=heartbeats)
        receiver.start()
        receivers.append(receiver)
    print 'Threaded heartbeat server listrning on port %d' % UDP_PORT
    print 'Press Ctrl-C to stop'
    try:
        while True:
            silent=heartbeats.getSilent()
            print 'Silent client: %s' % silent
            time.sleep(CHECK_PERIOD)

    except KeyboardInterrupt:
        print 'Exiting, please wait...'
        receiverEvent.clear()
        for receive in receivers:
            receive.join()
        print 'Finished.'

if __name__=='__main__':
    se=ServerSetUp()
    se.run()

