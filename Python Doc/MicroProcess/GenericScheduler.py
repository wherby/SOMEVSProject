import signal

def empty(name):
    while True:
        print "<empty process>",name
        yield None

def terminating(name,maxn):
    for i in xrange(maxn):
        print "Here %s, %s out of %s" %(name,i,maxn)
        yield None
    print "Done with %s, bailing out after %s" % (name,maxn)

def delay(duration=0.8):
    import time
    while True:
        print "<sleep %d>" % duration
        time.sleep(duration)
        yield  None

class GenericScheduler(object):

    def __init__(self,threads,stop_asap=False):
        signal.signal(signal.SIGINT,self.shutdownHandler)
        self.shutdownRequest=False
        self.threads=threads
        self.stop_asap=stop_asap

    def shutdownHandler(self,n,frame):
        print "request to shut down"
        self.shotdownRequest=True

    def schedule(self):
        def noop():
            while True: yield None
        n=len(self.threads)
        while True:
            for  i, thread in enumerate(self.threads):
                try: thread.next()
                except StopIteration:
                    if self.stop_asap: return
                    n-= 1
                    if n==0 :return
                    self.threads[i]=noop()
                if self.shutdownRequest:
                    return

if __name__=="__main__":
    s=GenericScheduler([empty('boo'),delay(),empty('foo'),terminating('file',5)
                        ,delay(0.5)],stop_asap=True)
    s.schedule()
    s=GenericScheduler([empty('boo'),delay(),empty('foo'),terminating('file',5)
                        ,delay(0.5)],stop_asap=False)
    s.schedule()
