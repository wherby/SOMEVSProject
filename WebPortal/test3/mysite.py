import web
import time
import datetime
import os
import re
import uuid
import cgi

from web import form
from datetime import date
import sys
sys.path.append(".\ARSite")
from QueryByDate import QueryByDate,Singleton
from QueryByName import QueryByName
from ESILogic import ESILogic



urls = (
    '/', 'MainFrame',
	'/OpenBugStatus','OpenBugStatus',
	'/ToBeVerified','ToBeVerified',
	'/QARecords','QARecords',
	'/OwnedByQA','OwnedByQA',
    '/TurnAroundStatus', 'TurnAroundStatus',
    '/TurnAroundTime', 'TurnAroundTime',
    '/viewgraph', 'viewgraph',
    '/MainFrame', 'MainFrame'
    )

render = web.template.render('templates/')
app = web.application(urls, globals())

myform = form.Form(
    form.Dropdown('ViewBy', [
        'week',
        'day',
        'month',
        'quarter'
        ]),
    form.Textbox('From', description = 'From (mm/dd/yyyy)'),
    form.Textbox('To', description = 'To (mm/dd/yyyy)'))
print time.localtime(time.time())
te=Singleton()
teName=QueryByName()
print time.localtime(time.time())

def ParseData(data=''):
    print data
    pairs={}
    for key_value in data.split('&'):
        key,value=key_value.split('=')
        pairs[key]=value
    return pairs



class viewgraph:
    def GET(self):
        web.header('Content-Type', 'image/png')
        u = web.input(p = None).p
        datafile = "static\\%s.data" % u
        pngfile = "static\\%s.png" % u
        scriptfile = "static\\%s.script" % u
        
        fl = open(pngfile, 'rb')
        ret = fl.read()
        fl.close()

#        os.remove(datafile)
#        os.remove(pngfile)
#        os.remove(scriptfile)
        
        return ret

class OpenBugStatus:
    def GET(self):
        return render.OpenBugStatus(None, None, None, None, None, None, None, None,None, None, None)
    def POST(self):
        p0=None
        p1=None
        p2=None
        p3=None
        formData = web.data()
        print formData
        pairs=ParseData(formData)
        print pairs
        
        product=pairs['product']
        release=pairs['release']
        viewby0=pairs['viewby']
        pList=[]
        if pairs.has_key('p0'):
            p0=pairs['p0']
            pList.append(0)
        if(pairs.has_key('p1')):
            p1=pairs['p1']
            pList.append(1)
        if(pairs.has_key('p2')):
            p2=pairs['p2']
            pList.append(2)
        if(pairs.has_key('p3')):
            p3=pairs['p3']
            pList.append(3)
        fromTime=pairs['from'].replace("%2F", "/")
        toTime=pairs['to'].replace("%2F", "/")
# Create photo
        b = viewby0
        f = fromTime
        t = toTime

        bv = str(b)
        fv = str(f)
        tv = str(t)

        r = re.compile('\d\d/\d\d/\d\d\d\d')

        errormsg = None
        
        if not r.match(fv):
            errormsg = 'From/To must be conformant to mm/dd/yy format.'

        if not r.match(tv):
            errormsg = 'From/To must be conformant to mm/dd/yy format.'

        f = time.strptime(fv, '%m/%d/%Y')
        print "From Time",f
        t = time.strptime(tv, '%m/%d/%Y')

        if f >= t:
            errormsg = 'From must be < To.'

        if not bv or bv == '': bv = 'w'
        viewby = "-%c" % bv[0]

        u = uuid.uuid1()



        start=time.strptime(fv,r'%m/%d/%Y')
        end=time.strptime(tv,r'%m/%d/%Y')
        if product=="0":
            a=ESILogic()
            errormsg=a.ESIOpenBugStatus(te,start,end,u,bv,fv,tv,pList,b)
        else:
            errormsg="vsi"+product
           
        

            
        imgurl = "/viewgraph?p=%s" % u
        
        
        return render.OpenBugStatus(product, release, p0, p1, p2, p3, viewby0, fromTime, toTime, errormsg, imgurl)
		
		
class ToBeVerified:
    def GET(self):
        return render.ToBeVerified(None, None, None, None, None, None, None, None,None, None, None)
    def POST(self):
        p0=None
        p1=None
        p2=None
        p3=None
        formData = web.data()
        print formData
        pairs=ParseData(formData)
        print pairs
        
        product=pairs['product']
        release=pairs['release']
        viewby0=pairs['viewby']
        pList=[]
        if pairs.has_key('p0'):
            p0=pairs['p0']
            pList.append(0)
        if(pairs.has_key('p1')):
            p1=pairs['p1']
            pList.append(1)
        if(pairs.has_key('p2')):
            p2=pairs['p2']
            pList.append(2)
        if(pairs.has_key('p3')):
            p3=pairs['p3']
            pList.append(3)
        fromTime=pairs['from'].replace("%2F", "/")
        toTime=pairs['to'].replace("%2F", "/")
        
        # Create photo
        b = viewby0
        f = fromTime
        t = toTime

        bv = str(b)
        fv = str(f)
        tv = str(t)

        r = re.compile('\d\d/\d\d/\d\d\d\d')

        errormsg = None
        
        if not r.match(fv):
            errormsg = 'From/To must be conformant to mm/dd/yy format.'

        if not r.match(tv):
            errormsg = 'From/To must be conformant to mm/dd/yy format.'

        f = time.strptime(fv, '%m/%d/%Y')
        print "From Time",f
        t = time.strptime(tv, '%m/%d/%Y')

        if f >= t:
            errormsg = 'From must be < To.'

        if not bv or bv == '': bv = 'w'
        viewby = "-%c" % bv[0]

        u = uuid.uuid1()
        
        if product=="0":
            a=ESILogic()
            errormsg=a.ESIToBeVerified(te,u,bv,fv,tv,pList,b)
        else:
            errormsg="vsi"+product
            
        imgurl = "/viewgraph?p=%s" % u
        
        
        return render.OpenBugStatus(product, release, p0, p1, p2, p3, viewby0, fromTime, toTime, errormsg, imgurl)

		
class QARecords:
    def GET(self):
        return render.QARecords(None, None)
    def POST(self):
        formData = web.data()
        print formData
        pairs=ParseData(formData)
        print pairs
        
        product=pairs['product']
        
        qaButton=' <div style="float:left;width:1000"><ul class=\"button-menu\"><li class=\"first\" onclick="ShowHide(this)">\
<a href="#"><label style="width:100;cursor:pointer;text-align:left;">%s</label><label id=sym%s style="width:800;cursor:pointer;text-align:right;">+</label></a></li></ul>\
  <table border=\"0\" style=\" border-color:FFFFFF\"  id=\"%sTable\"  style=\"display:none;\">'
        row='<tr ><!-- Row 1 -->\
     <td width=100>%s</td><!-- Col 1 -->\n\
     <td width=500>%s</td><!-- Col 2 -->\n\
     <td width=100>%s</td><!-- Col 3 -->\n\
     <td width=100>%s</td><!-- Col 4 -->\n\
     <td width=100>%s</td><!-- Col 5 -->\n\
     <td width=80>%s</td><!-- Col 6 -->\n\
  </tr>'
        end='<tr ><!-- Row 1 -->\
     <td colspan=6 align=\"right\">\
     <input type=\"hidden\" value="" name=\"%sHidden\">\
     <input type=\"button\" value=\"<\" id=\"%s\" onclick="ChangePage(this.id,0)" class=groovybutton>\
     &nbsp;<label id=\"%sLabel\"></label>&nbsp;\
        <input type=\"button\" value=\">\" id=\"%s\" onclick="ChangePage(this.id,1)" class=groovybutton></td><!-- Col 6 -->  </tr> </table></div><div style="height:40"></div>'

        if product=="0":
            a=ESILogic()
            test=a.ESIQARecords(teName,qaButton,row,end)
        else:
            test="vsi"+product


        
        return render.QARecords(product, test)
		
class OwnedByQA:
    def GET(self):
        return render.OwnedByQA(None, None)
    def POST(self):
        formData = web.data()
        print formData
        pairs=ParseData(formData)
        print pairs
        
        product=pairs['product']
        
        qaButton=' <table border=\"1\"  style=\" border-color:FFFFFF\" id=\"table\" style=\"border-collapse:collapse\">'
        row='<tr ><!-- Row 1 -->\
     <td width=100>%s</td><!-- Col 1 -->\n\
     <td width=500>%s</td><!-- Col 2 -->\n\
     <td width=100>%s</td><!-- Col 3 -->\n\
     <td width=100>%s</td><!-- Col 4 -->\n\
     <td width=100>%s</td><!-- Col 5 -->\n\
     <td width=100>%s</td><!-- Col 6 -->\n\
  </tr>'
        end = '<tr ><!-- Row 1 -->     <td colspan=6 align=\"right\">     <input type=\"hidden\" value="" name=\"hidden\">\
     <input type=\"button\" value=\"<\"  onclick="ChangePage(0)" class=groovybutton> &nbsp;<label id=\"label\"></label>&nbsp;\
        <input type=\"button\" value=\">\"  onclick="ChangePage(1)" class=groovybutton></td><!-- Col 6 -->  </tr> </table></div><div style="height:40"></div>'

        if product=="0":
            a=ESILogic()
            test=a.ESIOwnedByQA(teName,qaButton,row,end)
        else:
            test="vsi"+product

        #test="artf0001:tile,,sandy,,test me,,vnxe,,5" 
        
        return render.OwnedByQA(product, test)

class TurnAroundStatus:
    def GET(self):
        return render.TurnAroundStatus(None, None, None, None)
    def POST(self):
        formData = web.data()
        print formData
        pairs=ParseData(formData)
        print pairs
        
        product=pairs['product']
        fromTime=pairs['from'].replace("%2F", "/")
        toTime=pairs['to'].replace("%2F", "/")

        start=time.strptime(fromTime,r'%m/%d/%Y')
        end=time.strptime(toTime,r'%m/%d/%Y')

        qaPrefix = '<ul><li><a href="#" onclick=toggle("%s")><label style="width:200px;">%s</label>\
	<label style="width:300px;">Avg Turn-around Time: %s days</label>	<label style="width:200px;">Closed Bugs: %s</label>\
	<label style="width:240px;">Turn-around bugs: %s</label>	<img src="static/src/images/open.JPG" width="20px"   border="0" /></a><ul id=%s class="closed">\
       <li><a href="#"><label style="width:500px; cursor:auto;">Artifact ID:Title</label>    	<label style="width:100px; cursor:auto;">Priority</label>\
    	<label style="width:100px; cursor:auto;">Serverity</label>    	<label style="width:200px; cursor:auto;">Avg Turn-around time</label></a>	</li>'
        artfPrefix = '<li ><a href="#" onclick=toggle("%s")><label class="titles">%s:%s</label><label style="width:50px;">&nbsp;</label><label style="width:100px;">%s</label><label style="width:100px;">%s</label>\
        <label style="width:230px;">%s days</label><img src="static/src/images/open.JPG" width="20px"   border="0" /></a><ul id=%s class="closed">'
        artfStatus = '<li><a href="#"><label style="width:500px;text-align:center;">%s</label><label style="width:200px;">%s</label><label style="width:200px;">%s days</label></a></li>'
        artfSuffix = '</ul>		</li>'
        qaSuffix = '</ul></li></ul>'

        if product=="0":
            a=ESILogic()
            res=a.ESITurnAroundStatus(teName,start,end,qaPrefix,artfPrefix,artfStatus,artfSuffix,qaSuffix)
        else:
            res="vsi"+product

        return render.TurnAroundStatus(product, fromTime, toTime, res)
    
class TurnAroundTime:
    def GET(self):
        return render.TurnAroundTime(None, None, None, None, None)
    def POST(self):
        formData = web.data()
        print formData
        pairs=ParseData(formData)
        print pairs
        
        product=pairs['product']
        viewby=pairs['viewby']
        fromTime=pairs['from'].replace("%2F", "/")
        toTime=pairs['to'].replace("%2F", "/")

        start=time.strptime(fromTime,r'%m/%d/%Y')
        end=time.strptime(toTime,r'%m/%d/%Y')




        # Create photo
        b = viewby
        f = fromTime
        t = toTime

        bv = str(b)
        fv = str(f)
        tv = str(t)

        r = re.compile('\d\d/\d\d/\d\d\d\d')

        errormsg = None
        
        if not r.match(fv):
            errormsg = 'From/To must be conformant to mm/dd/yy format.'

        if not r.match(tv):
            errormsg = 'From/To must be conformant to mm/dd/yy format.'

        f = time.strptime(fv, '%m/%d/%Y')
        print "From Time",f
        t = time.strptime(tv, '%m/%d/%Y')

        if f >= t:
            errormsg = 'From must be < To.'



        u = uuid.uuid1()
        if product=="0":
            a=ESILogic()
            errormsg=a.ESITurnAroundTime(teName,u,bv,fv,tv,b,te)
        else:
            errormsg="vsi"+product
            
        imgurl = "/viewgraph?p=%s" % u
        
        return render.TurnAroundTime(product, viewby, fromTime, toTime, imgurl)
    
class MainFrame:
    def GET(self):
        print web.input()
        p = web.input(p = None).p
        t = web.input(t = None).t
        if p == None:
            p = 'OpenBugStatus'        
            t = 'Open Bug Status'
        return render.MainFrame(t,p)
		
if __name__ == "__main__":
    app.run()

