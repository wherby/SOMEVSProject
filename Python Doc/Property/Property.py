import math

class Rectangle(object):
    def __init__(self,x,y):
        self.x=x
        self.y=y

    def area():
        doc="Area of the rectangle"
        def fget(self):
            return self.x*self.y
        def fset(self,value):
            ratio=math.sqrt((1.0*value)/self.area)
            self.x*=ratio
            self.y*=ratio
        return locals()
    area=property(**area())



if __name__=="__main__":
    a=Rectangle(10,20)
    print a.area


#The example wrap the properties of area 
#   using property to wrap properties, the method must be fget,fset,fdel
#
#The test reslut will be 
#  200
