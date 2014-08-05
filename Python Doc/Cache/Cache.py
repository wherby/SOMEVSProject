
class CachedAttribute(object):
    def __init__(self,method,name=None):
        self.method=method
        self.name=name or method.__name__

    def __get__(self,inst,cls):
        if inst is None:
            return self
        result=self.method(inst)
        setattr(inst,self.name,result)
        return result

class CachedClassAttribute(CachedAttribute):
    def __get__(self,inst,cls):
        return super(CachedClassAttribute,self).__get__(cls,cls)



class MyObject(object):
    def __init__(self,n):
        self.n=n

    @CachedAttribute
    def square(self):
        print "Computing"
        return self.n * self.n

if __name__=="__main__":
    m=MyObject(23)
    print vars(m)
    print m.square
    print m.square
    print vars(m)
    del m.square
    print vars(m)
    m.n=42
    print vars(m)
    print m.square
    print vars(m)

#CachedAttribute: using the attr for a method, then initialize the method value
#                 when call the method which is decorated by the attribute,    
#                 then the result will be cached and return the result
#result will be
#{'n': 23}
#Computing
#529
#529
#{'square': 529, 'n': 23}
#{'n': 23}
#{'n': 42}
#Computing
#1764
#{'square': 1764, 'n': 42}
##

