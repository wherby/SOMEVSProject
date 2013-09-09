
from QueryByDate import QueryByDate,Singleton
from AR import *
import GlobalVars
from Audit import *
from TimeChange import *
#import copy

SHQA=["tao zhou","liping yu","Sandy"]
#STATUSLIST=["Returned to submitter","Test Me","To be reproduced","As Designed"]
ACTIVESTATUSLIST=["Returned to submitter","Test Me","To be reproduced","As Designed"]

ARList=[]
ARAudit=[]
ARBackup=[]

class QueryByName:
    def __init__(self):
        Singleton()
        global ARList,ARBackup
        ARList=GlobalVars.ARList
        ARBackup=GlobalVars.ARBackup
        
    def resetARList(self):
        global ARList,ARBackup
        ARList=ARBackup

    def setARList(self,List):
        global ARList,ARBackup
        ARList=List

    def getARList(self):
        global ARList,ARBackup
        return ARList

    def getARByName(self,nameList):
        global ARList
        f=ARFilter()
        resultTemp=f.ownByFilter(ARList,nameList)
        print "SHQA AR number2",len(resultTemp)
        return resultTemp

    def getARByAssignedName(self,nameList):
        global ARList
        f=ARFilter()
        resultTemp=f.assignToFilter(ARList,nameList)
        print "SHQA AR number2",len(resultTemp)
        resultTemp=f.statusClassFilter(resultTemp)
        return resultTemp    

    def getARByStatus(self,ARList,statusList):
        f=ARFilter()
        resultTemp=f.assignToFilter(ARList,statusList)
        print "SHQA AR status number",len(resultTemp)
        return resultTemp

    def getARTimeRange(self,ARListTemp):
        for i in range(0,len(ARListTemp)):
            t=time.gmtime()
            ARListTemp[i].turnRoundTime=TimeDiff(ARListTemp[i].last_modified_date, t).getDiff()
            print (ARListTemp[i].turnRoundTime)
        return ARListTemp


    def getStatus(self):
        global ARList
        print "ARLIST status",len(ARList)
        ARTemp=self.getARByName(SHQA) #for test
        f=ARFilter()
        #ARTemp=f.statusClassFilter(ARList)
        ARTemp=f.statusClassFilter(ARTemp)
        print "SHQA AR number",len(ARTemp)
        #ARTemp=self.getARByStatus(ARList,STATUSLIST) 
        return ARTemp

    def getTRStatus(self,start,end,nameList):
        global ARList
        f=ARFilter()
        ARTemp=f.ownByFilter(ARList,nameList)
        ARTemp=f.closeDateFilter(ARTemp,start,end)
        return ARTemp

    def getActiveTRStatus(self,nameList):
        global ARList
        f=ARFilter()
        ARTemp=f.ownByFilter(ARList,nameList)
        ARTemp=f.getOpenAR(ARTemp)
        #ARTemp=f.closeDateFilter(ARTemp,start,end)
        return ARTemp

    def getAverageTAT(self,ARList):
        time=0.0
        for i in range(0,len(ARList)):
            time+=ARList[i].turnRoundTime
        if len(ARList)>0:
            time=time/len(ARList)
        return round(time,2)
        

if __name__ == "__main__":
    qe=QueryByName()
    ARTemp=qe.getARByName(SHQA)
    qe.getStatus()
    ARTemp=qe.getARByStatus(ARTemp,STATUSLIST)
    print len(ARTemp)
    print ARTemp
    #qe.getARTimeRange(ARTemp)
