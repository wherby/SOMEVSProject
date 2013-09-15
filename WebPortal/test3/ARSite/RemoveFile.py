import os
import stat
import time,datetime

class RemoveFile:
    def __init__(self):
        tm=time.localtime(time.time())
        self.now=datetime.datetime(tm.tm_year,tm.tm_mon,tm.tm_mday)
        self.flist=[]
        #print self.now


    def getFileDate(self,fileName):
        fs=os.stat(fileName)
        tm=time.localtime(fs[stat.ST_CTIME])
        tm=datetime.datetime(tm.tm_year,tm.tm_mon,tm.tm_mday)
        #print tm
        return tm

    def getAllFile(self,dirName):
        self.flist=[]
        for fileName in os.listdir(dirName):
            fileName=dirName+fileName
            fs=os.stat(fileName)
            if not stat.S_ISDIR(fs[stat.ST_MODE]):
                #print fileName;
                self.getFileDate(fileName)
                self.flist.append(fileName)
        
    def removeAllFile(self,dirName):
        self.getAllFile(dirName)
        tm1=self.now
        for i in range(0, len(self.flist)):
            fname=self.flist[i]
            tm2=self.getFileDate(fname)
            tm2=tm2+datetime.timedelta(days=10)
            if(tm2<tm1):
                print "delete "+fname
                os.remove(fname)
                
        
        

    



def main():
    test=RemoveFile()
    #test.getFileDate('Test.zip')
    #test.getAllFile('..\\.\\static\\')
    test.removeAllFile('..\\.\\static\\')


if __name__=="__main__":
    main()
