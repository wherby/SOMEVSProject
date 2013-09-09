from accessdb import accessdb
from TimeChange import *
from AR import *
from Audit import *
#from QueryByName import QueryByName
import GlobalVars
#import copy

ARList=[]
ARAudit=[]
ARBackup=[]
PList=[]

class Singleton(object):
    def __new__(cls,*args,**kwargs):
        if '_inst' not in vars(cls):
            cls._inst=super(Singleton,cls).__new__(cls,*args,**kwargs)
            cls.re=QueryByDate()
            #print "SG created"
            global ARList
            QueryByDate.getARTurnRoundTime(ARList)
            GlobalVars.ARList=ARList
            GlobalVars.ARBackup=ARList
            ARBackup=GlobalVars.ARBackup
        return cls.re
    
class QueryByDate:
    def __init__(self):
        global ARList,ARBackup,ARAudit
        nam="a";
        db=accessdb(nam)
        ARList=db.getAllAR(ARList)
        ARAudit=db.getAllAudit(ARAudit)
        ARList=db.combinARandAudit(ARList,ARAudit)      
        GlobalVars.ARList=ARList
        GlobalVars.ARBackup=ARList
        ARBackup=GlobalVars.ARBackup
        maxTR=10

    @staticmethod
    def getARTurnRoundTime(ARListTemp):
        SHQA=["liping yu","Sandy","tao zhou"]
        STATUSLIST=GlobalVars.STATUSLIST
        for i in range(0,len(ARListTemp)):
            temp=ARListTemp[i]
            timeRange=[]
            timeSum=0
            count=0
            tempS=temp.submitted_date
            tempE=time.localtime()
            tempStatusLog=[]
            #For the AR is open and assigned to QA
            if SHQA.count(temp.assigned_to_fullname)>0 and temp.status_class=="Open":
                start=temp.last_modified_date
                td=TimeDiff(start,tempE)
                tempTime=round(td.getDiff(),2)
                timeRange.append(tempTime)

            
            for j in range(0,len(temp.auditList)):
               if temp.auditList[j].property_name=="status":
                    tempStatusLog.append(StatusLog(tempS,temp.auditList[j].date_modified,temp.auditList[j].old_value))
                    tempS=temp.auditList[j].date_modified
               
               #print temp.auditList[j].modified_by_fullname
               #print SHQA[0]
               if temp.auditList[j].property_name=="assignedTo" and SHQA.count(temp.auditList[j].modified_by_fullname)>0:
                   #print temp.artifact_id
                   end=temp.auditList[j].date_modified
                   for k in range (j, len(temp.auditList)):
                       if temp.auditList[k].property_name=="assignedTo"  and SHQA.count(temp.auditList[k].modified_by_fullname)==0:
                           start=temp.auditList[k].date_modified
                           td=TimeDiff(start,end)
                           tempTime=round(td.getDiff(),2)
                           timeRange.append(tempTime)
                           ARListTemp[i].statusChangeList.append(StatusChange(start,temp.auditList[j].old_value,temp.auditList[j].new_value,tempTime))
                           #print start,temp.auditList[j].old_value,temp.auditList[j].new_value,tempTime
                           break
               else:
                   if temp.auditList[j].property_name=="assignedTo":
                       end=temp.auditList[j].date_modified
                       isLast=True
                       for k in range (j+1, len(temp.auditList)):
                           if temp.auditList[k].property_name=="assignedTo":
                               start=temp.auditList[k].date_modified
                               td=TimeDiff(start,end)
                               tempTime=round(td.getDiff(),2)                               
                               ARListTemp[i].statusChangeList.append(StatusChange(temp.auditList[j].date_modified,temp.auditList[j].old_value,temp.auditList[j].new_value,tempTime))
                               isLast=False
                               break
                       if isLast==True:
                           start=temp.submitted_date
                           td=TimeDiff(start,end)
                           tempTime=round(td.getDiff(),2)                               
                           ARListTemp[i].statusChangeList.append(StatusChange(temp.auditList[j].date_modified,temp.auditList[j].old_value,temp.auditList[j].new_value,tempTime))
            tempStatusLog.append(StatusLog(tempS,tempE,temp.status))
            for m in range(0,len(timeRange)):
                timeSum+=timeRange[m]
            if len(timeRange)>1:
                timeSum=timeSum/len(timeRange)
            ARListTemp[i].turnRoundTime= round(timeSum,2)
            ARListTemp[i].statusLog=tempStatusLog
        return ARListTemp


    def setARListP(self,pList):        
        global ARList,ARBackup,PList
        PList=pList
        ARList=ARBackup
        print "before pfilter", len(ARList)
        f=ARFilter()
        ARList=f.priorityFilterSelect(ARList,pList)
        print "After pfilter", len(ARList)
        
    def resetARList(self):
        global ARList,ARBackup
        ARList=ARBackup

    def setARList(self,List):
        global ARList,ARBackup
        ARList=List

    def getARList(self):
        global ARList,ARBackup
        return ARList

    def getByWeek(self,start,end,fileName):
        
        self.tempA=TimeChange(start,end)
        self.tempA.byWeek()
        self.query(fileName)

    def getByDay(self,start,end,fileName):
        self.tempA=TimeChange(start,end)
        self.tempA.byDay()
        self.query(fileName)

    def getByMonth(self,start,end,fileName):
        self.tempA=TimeChange(start,end)
        self.tempA.byMonth()
        self.query(fileName)

    def query(self,fileName):
        for i in range(0,len(self.tempA.SpanList)):
            a=self.tempA.SpanList[i]
            f=ARFilter()
            #print a.name
            self.tempA.SpanList[i].closeAR=f.closeDateFilter(ARList,a.start,a.end)
            self.tempA.SpanList[i].openAR=f.submitDateFilter(ARList,a.start,a.end)
            #self.resetARList()
            ARTemp=ARBackup
            self.tempA.SpanList[i].p2AR=f.p2FilterEx(ARList,a.end,PList)
            #print str(len(self.tempA.SpanList[i].openAR))+"  "+str(len(self.tempA.SpanList[i].closeAR))+"  "+str(len(self.tempA.SpanList[i].p2AR))
        self.printToFile(fileName)

    def getByWeekTBV(self,start,end,fileName):
        self.tempA=TimeChange(start,end)
        self.tempA.byWeek()
        self.queryTBV(fileName)
        
    def getByDayTBV(self,start,end,fileName):
        self.tempA=TimeChange(start,end)
        self.tempA.byDay()
        self.queryTBV(fileName)
        
    def getByMonthTBV(self,start,end,fileName):
        self.tempA=TimeChange(start,end)
        self.tempA.byMonth()
        self.queryTBV(fileName)        

    def queryTBV(self,fileName):
        for i in range(0,len(self.tempA.SpanList)):
            a=self.tempA.SpanList[i]
            f=ARFilter()
            #print a.name
            self.tempA.SpanList[i].tBV=f.toBeVerifyFilter(ARList,a.start,a.end)
        self.printToFileTBV(fileName)       
        
    def printToFile(self,fileName):
        f=open(fileName,"w")
        for a in self.tempA.SpanList:
            print>>f,"%s %d"%(a.name,len(a.openAR))
        print>>f,"\r\n"
        for a in self.tempA.SpanList:
            print>>f,"%s %d"%(a.name,len(a.closeAR))
        print>>f,"\r\n"
        for a in self.tempA.SpanList:
            print>>f,"%s %d"%(a.name,len(a.p2AR))
        f.close()

    def printToFileTBV(self,fileName):
        f=open(fileName,"w+")
        for a in self.tempA.SpanList:
            print>>f,"%s %d"%(a.name,len(a.tBV))
            #f.writelines("%s %d"%(a.name,len(a.tBV)))
        print>>f,"\r\n"
        #f.writelines("\r\n")
        f.close()


    def getByWeekTR(self,start,end,fileName,L1,L2,L3):
        self.maxTR=10
        self.tempA=TimeChange(start,end)
        self.tempA.byWeek()
        self.queryTR(fileName,L1)
        self.queryTR(fileName,L2)
        self.queryTR(fileName,L3)
        L1.extend(L2)
        L1.extend(L3)
        self.queryTR(fileName,L1)        
        
    def getByDayTR(self,start,end,fileName,L1,L2,L3):
        self.maxTR=10
        self.tempA=TimeChange(start,end)
        self.tempA.byDay()
        self.queryTR(fileName,L1)
        self.queryTR(fileName,L2)
        self.queryTR(fileName,L3)
        L1.extend(L2)
        L1.extend(L3)
        self.queryTR(fileName,L1) 

    def getByMonthTR(self,start,end,fileName,L1,L2,L3):
        self.maxTR=10
        self.tempA=TimeChange(start,end)
        self.tempA.byMonth()
        self.queryTR(fileName,L1)
        self.queryTR(fileName,L2)
        self.queryTR(fileName,L3)
        L1.extend(L2)
        L1.extend(L3)
        self.queryTR(fileName,L1) 
        
    def queryTR(self,fileName,L1):
        for i in range(1,len(self.tempA.SpanList)):
            a=self.tempA.SpanList[i]
            f=ARFilter()
            #print a.name
            self.tempA.SpanList[i].tBV=f.closeDateFilter(L1,a.start,a.end)
        self.printToFileTR(fileName)

    def printToFileTR(self,fileName):
        f=open(fileName,"a+")
        for a in self.tempA.SpanList:
            time=0.0;
            for j in a.tBV:
                time=time+j.turnRoundTime
                #print>>f,"%s %d"%("jj"+j.ID,j.turnRoundTime)
            if len(a.tBV)>0:
                time=time/len(a.tBV)
            if time>self.maxTR:
                self.maxTR=time
            print>>f,"%s %f"%(a.name,time)
        print>>f,"\r\n"
        f.close()

    #The method is used for assistant plotting.
    def getNumber(self):
        recordNumber=len(self.tempA.SpanList)
        if recordNumber<20:
            return 1
        else:
            return recordNumber/20

    def getYRange(self):
        maxValue=10
        for i in range(len(self.tempA.SpanList)):
            if maxValue<len(self.tempA.SpanList[i].p2AR):
                maxValue=len(self.tempA.SpanList[i].p2AR)
        return maxValue

    def getYRangeTBV(self):
        maxValue=10
        for i in range(len(self.tempA.SpanList)):
            if maxValue<len(self.tempA.SpanList[i].tBV):
                maxValue=len(self.tempA.SpanList[i].tBV)
        return maxValue
            
            
       
def main():
    start=time.strptime('10/1/2011',r'%m/%d/%Y')
    end=time.strptime('2/1/2012',r'%m/%d/%Y')
    a=TimeChange(start,end)
    #print a
    #a.byDay()
    #a.byWeek()
    #a.byMonth()
    #te=QueryByDate()
    te=Singleton()
    te=Singleton()
    ARList=te.getARList()
    f=ARFilter()
    te.setARList(f.assignToFilter(ARList,["Jialin He"]))
    te.getByWeek(start,end,"out.txt")
    te.resetARList()
    te.getByWeek(start,end,"out.txt")
    #te.printToFile()

if __name__=="__main__":
    main()
