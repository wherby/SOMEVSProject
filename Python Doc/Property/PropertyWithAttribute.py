import math

class Rectangle(object):
    def __init__(self,x,y):
        self.x=x
        self.y=y

    def nested_property(c):
        return property(**c())

    @nested_property
    def area():
        doc="Area of the rectangle"
        def fget(self):
            return self.x*self.y
        def fset(self,value):
            ratio=math.sqrt((1.0*value)/self.area)
            self.x*=ratio
            self.y*=ratio
        return locals()





if __name__=="__main__":
    a=Rectangle(10,20)
    print a.area


#The example wrap the properties of area 
#   using attribute to wrap the attribute
#        using @Fa to decrorate function Fb()  then call Fb() will
#        have result of Fa(Fb()) 
#
#The test reslut will be 
#  200
