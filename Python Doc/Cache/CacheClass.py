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


class MyClass(object):
    class_attr=23

    @CachedClassAttribute
    def squre(cls):
        print "Computing.."
        return cls.class_attr * cls.class_attr


if __name__=="__main__":
    x=MyClass()
    y=MyClass()
    print x.squre
    print y.squre
    del MyClass.squre
    print x.squre

#using class cache, then the result will be cached to class , all the object
#from the class will be cached.
# The result will be:
#Computing..
#529
#529
#
#Traceback (most recent call last):
#  File "C:/Python27/MyTest/Cache/CacheClass.py", line 33, in <module>
#    print x.squre
#AttributeError: 'MyClass' object has no attribute 'squre'
#
