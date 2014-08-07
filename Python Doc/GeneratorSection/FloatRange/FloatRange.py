import itertools

def frange(start,end=None,inc=1.0):
    if end is None:
        end=start+0.0
        start=0.0

    assert inc
    for i in itertools.count():
        next=start+i*inc
        if(inc>0.0 and next>=end) or (inc<0.0 and next<end):
            break
        yield next


if __name__=="__main__":
    f1=frange(5)
    print f1.next()
    print f1.next()
    for i in range(100):
        print f1.next()


#using for i in itertools.count(): is quicker than using while True:
