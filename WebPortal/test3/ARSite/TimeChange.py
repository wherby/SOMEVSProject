import datetime,time

class TimeSpan:
    def __init__(self,start,end):
        self.start=time.strptime(str(start),"%Y-%m-%d %X")
        self.end=time.strptime(str(end),"%Y-%m-%d %X")
        self.name=str(start.month)+"/"+str(start.day)+"/"+str(start.year)
        self.closeAR=[]
        self.openAR=[]
        self.p2AR=[]
        self.tBV=[]
        self.defaultAR=[]
        

    def __str__(self):
        return self.name
    
class TimeChange:
    def __init__(self,start,end):
        self.start=datetime.datetime(start.tm_year,start.tm_mon,start.tm_mday)
        self.end=datetime.datetime(end.tm_year,end.tm_mon,end.tm_mday)
        self.SpanList=[]
        
        
    def byDay(self):
        self.start-=datetime.timedelta(days=1)
        while self.start<=self.end:
            #print datetime.timedelta(days=1)          
            temp=TimeSpan(self.start,self.start+datetime.timedelta(days=1))
            self.start+=datetime.timedelta(days=1)
            self.SpanList.append(temp)
            #print self
       
    def byWeek(self):
        self.start-=datetime.timedelta(days=7)
        while self.start<=self.end:            
            temp=TimeSpan(self.start,self.start+datetime.timedelta(days=7-self.start.weekday()))
            self.start+=datetime.timedelta(days=7-self.start.weekday())
            self.SpanList.append(temp)
            #print self.start,self.end
    def byMonth(self):
        #self.start=datetime.datetime(self.start.year+(self.start.month-1)//12,(self.start.month-1)%12,1)
        if self.start.month==1:
            self.start=datetime.datetime(self.start.year-1,12,1)
        else:
            self.start=datetime.datetime(self.start.year,self.start.month-1,1)
        while self.start<=self.end:
            temp=TimeSpan(self.start,datetime.datetime(self.start.year+(self.start.month)//12,(self.start.month)%12+1,1))
            self.start=datetime.datetime(self.start.year+(self.start.month)//12,(self.start.month)%12+1,1)
            self.SpanList.append(temp)
            #print str(temp.start),str(temp.end)
            #print time.strftime("%Y-%m-%d",temp.start)

    def __str__(self):
        
        return str(self.start)+str(self.end)

class TimeDiff:
    def __init__(self,start,end):
        self.start=start
        self.end=end

    def getDiff(self):
        diffTime=float(self.end.tm_year)*365+float(self.end.tm_yday)+float(self.end.tm_hour)/24-(float(self.start.tm_year)*365+float(self.start.tm_yday)+float(self.start.tm_hour)/24)
        return diffTime

class StatusLog:
    def __init__(self,start,end,status):
        self.start=start
        self.end=end
        self.status=status
        
def main():
    start=time.strptime('10/1/2011',r'%m/%d/%Y')
    end=time.strptime('10/1/2012',r'%m/%d/%Y')
    a=TimeChange(start,end)
#print a
    #a.byDay()
#a.byWeek()
    a.byMonth()
if __name__=="__main__":
    main()
