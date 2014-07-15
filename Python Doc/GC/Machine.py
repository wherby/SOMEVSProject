

GFILE=[]


class Machine:
    def __init__(self):
        self.globalV={}
        self.localV={}
        pass

    def setupEnv(self):
        for tempfile in GFILE:
            execfile(tempfile,self.globalV,self.localV)
        pass

    def addGFile(self,fileTemp):
        global GFILE
        GFILE.append(fileTemp)
        pass

    def exeLine(self,lineTemp):
        exec(lineTemp,self.globalV,self.localV)
        pass





if __name__=='__main__':
    ma=Machine()
    ma.addGFile("C:\Python27\MyTest\GC\Source.py")
    ma.setupEnv()
    ma.exeLine('a=Test();print "a.a",a.a')
    ma.exeLine('b=Test();b.a=3;print "b.a",b.a')
