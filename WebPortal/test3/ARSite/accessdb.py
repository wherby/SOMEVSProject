

import win32com.client
import time
from AR import *
from Audit import *

from RemoveFile import *

ARList=[]
AuditList=[]

class accessdb:
    def __init__(self,dbname):
        self.dbname=dbname
        self.db= "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\administrator.SR5DOM\\Desktop\\TF\\TeamforgeIntegration\\tftools\\tf.mdb;User Id=Admin;Password=;Persist Security Info=False"
        self.conn=win32com.client.Dispatch('ADODB.Connection')
        self.conn.ConnectionString =self.db
        #remove file in static folder
        remove=RemoveFile()
        remove.removeAllFile('.\\static\\')



    def getAllAR(self,ARList):
        self.conn.Open()
        self.rs=win32com.client.Dispatch('ADODB.Recordset')
        self.sql="select  * from artifact_main"
        self.rs.Open('['+self.sql+']',self.conn,1,3)
        self.rs.MoveFirst()
        record={}
        while not self.rs.EOF:
            a=AR(self.rs.Fields.Item(0).Value,\
                 self.rs.Fields.Item(1).Value,\
                 self.rs.Fields.Item(2).Value,\
                 self.rs.Fields.Item(3).Value,\
                 self.rs.Fields.Item(4).Value,\
                 self.rs.Fields.Item(5).Value,\
                 self.rs.Fields.Item(6).Value,\
                 self.rs.Fields.Item(7).Value,\
                 self.rs.Fields.Item(8).Value,\
                 self.rs.Fields.Item(9).Value,\
                 self.rs.Fields.Item(10).Value,\
                 self.rs.Fields.Item(11).Value,\
                 self.rs.Fields.Item(12).Value,\
                 self.rs.Fields.Item(13).Value,\
                 self.rs.Fields.Item(14).Value,\
                 self.rs.Fields.Item(15).Value,\
                 self.rs.Fields.Item(16).Value,\
                 self.rs.Fields.Item(17).Value,\
                 self.rs.Fields.Item(18).Value)
            ARList.append(a)
            self.rs.MoveNext()
        self.conn.Close()
        print len(ARList)
        return ARList

    def getAllAudit(self,auditList):
        self.conn.Open()
        self.rs=win32com.client.Dispatch('ADODB.Recordset')
        self.sql="select * from audit_history"
        self.rs.Open('['+self.sql+']',self.conn,1,3)
        self.rs.MoveFirst()
        record={}
        while not self.rs.EOF:
            a=Audit(self.rs.Fields.Item(0).Value,\
                 self.rs.Fields.Item(1).Value,\
                 self.rs.Fields.Item(2).Value,\
                 self.rs.Fields.Item(3).Value,\
                 self.rs.Fields.Item(4).Value,\
                 self.rs.Fields.Item(5).Value,\
                 self.rs.Fields.Item(6).Value,\
                 self.rs.Fields.Item(7).Value,\
                 self.rs.Fields.Item(8).Value,\
                 self.rs.Fields.Item(9).Value,)
            auditList.append(a)
            self.rs.MoveNext()
        self.conn.Close()
        print len(auditList)
        return auditList

    def combinARandAudit(self,ARList,AuditList):        
        AuditNotCom=[]
        i=0
        k=0
        for i in range(0,len(ARList)):
            while(ARList[i].artifact_id==AuditList[k].artifact_id):
                ARList[i].auditList.append(AuditList[k])
                k=k+1
                #print k,i
                if(k>=(len(AuditList))):
                    break
            ARList[i].auditList=sorted(ARList[i].auditList,key=lambda kv:kv.date_modified,reverse=True)
            
            if(k>=(len(AuditList)-1)):
                break
        print k
        print len(ARList[0].auditList)
        return ARList

def main():
    nam="a";
    mysql="select * from artifact_main"
    db=accessdb(nam)
    db.getAllAR(ARList)
    db.getAllAudit(AuditList)
    db.combinARandAudit(ARList,AuditList)
if __name__=="__main__":
    main()
            
            
