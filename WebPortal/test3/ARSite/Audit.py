import time
class Audit:
    def __init__(self,id,artifact_id,date_modified,property_name,\
                 operation,old_value,new_value,modified_by_username,\
                 modified_by_fullname,comment):
        self.id=id
        self.artifact_id=artifact_id
        #print date_modified
        self.date_modified=time.strptime(str(date_modified),"%m/%d/%y %X")
        self.property_name=property_name
        self.operation=operation
        self.old_value=old_value
        self.new_value=new_value
        self.modified_by_username=modified_by_username
        self.modified_by_fullname=modified_by_fullname
        self.comment=comment
        #print self

    def __str__(self):
        return str(self.date_modified)+ " "+str(self.artifact_id)


class StatusChange:
    def __init__(self,changeTime,old,new,timeSpan):
        self.changeTime=changeTime
        self.old=old
        self.new=new
        self.timeSpan=timeSpan

    def __str__(self):
        return str(self.changeTime)+" "+self.old+"->"+self.new+" "+str(self.timeSpan)
