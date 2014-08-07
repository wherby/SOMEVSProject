import itertools
b1=[1,2,3,4,5,6,7,8,9,10]
y=list(itertools.islice(b1,3))

def fib():
    x,y=0,1
    while True:
        yield x
        x,y=y,x+y
        

def peel(iterable,arg_cnt=1):
    iterator=iter(iterable)
    for num in range(arg_cnt):
        yield iterator.next()
    yield iterator

if __name__=="__main__":
    print y
    a=fib()
    print a.next()
    print list(itertools.islice(a,10))

    b,c,d=peel(a,2)
    print b,c,d

 

# itertools.islice could silce both array and infinite generator
# using peel to unpack the iterator
