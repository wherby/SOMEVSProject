from TimeChange import TimeChange
import time
import GlobalVars

#STATUSLIST=["Returned to submitter","Test Me","To be reproduced","As Designed"]
STATUSLIST=GlobalVars.STATUSLIST

class AR:
    def __init__(self,ID,artifact_id,artifact_title,assigned_to_fullname,assigned_to_username,\
                 category,close_date,last_modified_date,priority,status,status_class,\
                 submitted_by_fullname,submitted_by_username,\
                 submitted_date,targeted_release,defect_type,fixed_by,verified_by,severity="10"):
        self.ID=ID
        self.artifact_id=artifact_id
        artifact_title=artifact_title.replace("\DQUOTE","\"")
        self.artifact_title=artifact_title.replace("\SQUOTE","\'")
        self.assigned_to_fullname=assigned_to_fullname
        self.assigned_to_username=assigned_to_username
        self.category=category
        #print close_date
        self.close_date=time.strptime(str(close_date),r'%m/%d/%y %X')
        self.last_modified_date=time.strptime(str(last_modified_date),r'%m/%d/%y %X')
        #TimeChange(last_modified_date).change
        self.priority=priority
        self.status=status
        self.status_class=status_class
        self.submitted_by_fullname=submitted_by_fullname
        self.submitted_by_username=submitted_by_username
        self.submitted_date=time.strptime(str(submitted_date),r'%m/%d/%y %X')
        self.targeted_release=targeted_release
        self.defect_type=defect_type
        self.fixed_by=fixed_by
        self.verified_by=verified_by
        self.severity=AR.severityChange(severity,priority)
        self.auditList=[]
	self.timeRange=0
	self.toBeVerified=0
	self.turnRoundTime=0
	self.statusChangeList=[]   #audit changelist
	self.statusLog=[] #status Log for date and status

    @staticmethod
    def severityChange(severity,priority):
        if severity>5 or severity<0:
            return priority
        else:
            return severity
    

    def getWarning(self):
        if self.turnRoundTime>2:
            return "Warning"
        else:
            return ""
            

    def __str__(self):
        return "artifact_id  %s self.priority %s)" %(self.artifact_id,self.priority)

class ARFilter:
    def p2Filter(self,ARList,end):
        #ARTemp=filter(lambda e:e.priority<=2,ARList)
        ARTemp=filter(lambda e:e.submitted_date<=end,ARList)
        ARTemp=filter(lambda e:(e.close_date>=end)|(e.close_date.tm_year==2001),ARTemp)
        ARTemp=filter(lambda e:self.removeDeferredStatus(e,end),ARTemp)
        return ARTemp

    def p2FilterEx(self,ARList,end,pList):
        #ARTemp=filter(lambda e:e.priority<=2,ARList)
        ARTemp=filter(lambda e:e.submitted_date<=end,ARList)
        ARTemp=filter(lambda e:(e.close_date>=end)|(e.close_date.tm_year==2001),ARTemp)
        ARTemp=filter(lambda e:self.removeDeferredStatus(e,end),ARTemp)
        ARTemp=self.priorityFilterSelect(ARTemp,pList)
        return ARTemp

    def priorityFilterSelect(self,ARListT,pList):
        print pList 
        ARTemp=filter(lambda e: pList.count(e.priority)>0,ARListT)
        print "ARTempLen",len(ARTemp)
        return ARTemp
        
    def severityFilter(self ,ARList,severityNumber):
        ARResult=filter(lambda e:e.severity<severityNumber,ARList)
        return ARResult
    
    def priorityFilter(self,ARList,priorityNumber):
        ARResult=filter(lambda e:e.priority<priorityNumber,ARList)
        return ARResult

    def statusFilter(self,ARList,statusList):
        ARResult=filter(lambda e: statusList.count(e.status)>0,ARList)
        return ARResult
    
    def assignToFilter(self,ARList,nameList):
        ARResult=filter(lambda e: nameList.count(e.assigned_to_username)>0,ARList)
        return ARResult


    def closeDateFilter(self,ARList,start,end):
        #print start,end,
        ARResult=filter(lambda e:e.close_date>start,ARList)
        ARResult=filter(lambda e:e.close_date<end,ARResult)
        return ARResult

    def submitDateFilter(self,ARList,start,end):
        #print start,end
        ARResult=filter(lambda e:e.submitted_date>start,ARList)
        #print len(ARResult)
        ARResult=filter(lambda e:e.submitted_date<end,ARResult)
        #print len(ARResult)
        #for a in ARResult:
            #print a.submitted_date
        return ARResult    

    def toBeVerifyFilter(self,ARList,start,end):
        ARTemp=[]
        for i in range(0,len(ARList)):
            for j in range(0,len(ARList[i].auditList)):
                if ARList[i].auditList[j].property_name=="status" and STATUSLIST.count(ARList[i].auditList[j].new_value)>0 :
                    ARList[i].toBeVerified=ARList[i].auditList[j].date_modified
                    if ARList[i].toBeVerified>start and ARList[i].toBeVerified<end:
                        ARTemp.append(ARList[i])
        return ARTemp
        #ARListTemp=filter(lambda e:e.toBeVerified!=0,ARList)
        #ARListTemp=filter(lambda e:e.toBeVerified>start,ARListTemp)
        #ARListTemp=filter(lambda e:e.toBeVerified<end,ARListTemp)
        #return ARListTemp

    def ownByFilter(self,ARList,nameList):
        ARListTemp=filter(lambda e:self.auditListChecker(e,nameList),ARList)
        return ARListTemp
                          

    def auditListChecker(self,AR,nameList):
        if nameList.count(AR.submitted_by_fullname)>0:
            return True
        for i in range(0,len(AR.auditList)):
            if nameList.count(AR.auditList[i].modified_by_fullname)>0 :
                return True
        return False
        
    def statusClassFilter(self,ARList,classValue="Open"):
        ARResult=filter(lambda e:e.status_class==classValue,ARList)
        return ARResult

    def removeDeferredStatus(self,AR,endTime):
        for statusTemp in AR.statusLog:
            if statusTemp.start< endTime and endTime<statusTemp.end and statusTemp.status=="Deferred":
                return False
        return True

    def getClosedAR(self,ARList):
        if len(ARList)==0:
            return []
        ARTemp=filter(lambda e:e.status_class=="Close",ARList)
        return ARTemp

    def getOpenAR(self,ARList):
        if len(ARList)==0:
            return []        
        ARTemp=filter(lambda e:e.status_class=="Open",ARList)
        return ARTemp
