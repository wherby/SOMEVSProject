import os
import time

class ESILogic:
    
    def ESIOpenBugStatus(self,te,start,end,u,bv,fv,tv,pList,b):
        datafile = "static\\%s.data" % u
        pngfile = "static\\%s.png" % u
        scriptfile = "static\\%s.script" % u
        te.resetARList
        te.setARListP(pList)
        print pList
        if b=="week":
            te.getByWeek(start,end,datafile)
        if b=="day":
            te.getByDay(start,end,datafile)
        if b=="month":
            te.getByMonth(start,end,datafile)
        

        num=te.getNumber()
        yRange=te.getYRange()
        yRange=yRange+yRange*0.2
        #rint yRange
        script = "set title 'Bug Trend (by %s) %s - %s'\n" % (bv, fv, tv) \
                + "set boxwidth 0.3\n" \
                + "set style fill solid 1.000000 border -1\n" \
                + "set bmargin 5\n" \
                + "set lmargin 5\n" \
                + "set rmargin 5\n" \
                + "set pointsize 2\n" \
                + "set ylabel 'Count'\n" \
                + "set tics scale 0.0\n" \
                + "set grid y\n" \
                + "set xtics rotate by -45\n" \
                + "set term png size 800,600\n" \
                + "set output '%s'\n" % (pngfile) \
                + "set yrange [0:%d]\n"% (yRange) \
                + "set key autotitle columnhead\n" \
                + "plot '%s' using ($0-0.2):2 index 0 with boxes title '   Incoming Bugs', \\\n" % (datafile) \
                + "''                      using ($0+0.2):2 index 1 with boxes title '     Closed Bugs', \\\n" \
                + "''                      using ($0):2:xticlabels(int($0)%% %d==0 ? strcol(1):'') index 2 with linespoints lt 4 pt 7 title 'Active Bugs'\n" %(num) \
                + "set output\n"
        
        sf = open(scriptfile, "w+")
        sf.write(script)
        sf.close()

        progstr = "\"C:\\Program Files (x86)\\gnuplot\\bin\\gnuplot.exe\" %s" % scriptfile
        exitcode = os.system(progstr)
        errormsg=None
        if exitcode != 0:
            print exitcode
            errormsg = "error plotting data."
        return errormsg

    def ESIToBeVerified(self,te,u,bv,fv,tv,pList,b):
        datafile = "static\\%s.data" % u
        pngfile = "static\\%s.png" % u
        scriptfile = "static\\%s.script" % u

        start=time.strptime(fv,r'%m/%d/%Y')
        end=time.strptime(tv,r'%m/%d/%Y')
        te.resetARList
        te.setARListP(pList)
        print pList
        if b=="week":
            te.getByWeekTBV(start,end,datafile)
        if b=="day":
            te.getByDayTBV(start,end,datafile)
        if b=="month":
            te.getByMonthTBV(start,end,datafile)
        
        num=te.getNumber()

        yRange=te.getYRangeTBV()
        yRange=yRange+yRange*0.2
        
        script = "set title 'Bug Trend (by %s) %s - %s'\n" % (bv, fv, tv) \
                + "set boxwidth 0.3\n" \
                + "set style fill solid 1.000000 border -1\n" \
                + "set bmargin 5\n" \
                + "set lmargin 5\n" \
                + "set rmargin 5\n" \
                + "set pointsize 2\n" \
                + "set ylabel 'Count'\n" \
                + "set tics scale 0.0\n" \
                + "set grid y\n" \
                + "set xtics rotate by -45\n" \
                + "set term png size 800,600\n" \
                + "set output '%s'\n" % (pngfile) \
                + "set yrange [0:%d]\n"% (yRange) \
                + "set key autotitle columnhead\n" \
                + "plot '%s' using ($0-0.2):2:xticlabels(int($0)%% %d==0 ? strcol(1):'')  index 0 with boxes title ' To be Verified'\n" % (datafile,num) \
                + "set output\n"
        
        sf = open(scriptfile, "w+")
        sf.write(script)
        sf.close()

        progstr = "\"C:\\Program Files (x86)\\gnuplot\\bin\\gnuplot.exe\" %s" % scriptfile
        exitcode = os.system(progstr)
        errormsg=None
        if exitcode != 0:
            print exitcode
            errormsg = "error plotting data."
        return errormsg

    def ESIQARecords(self,teName,qaButton,row,end):
        teName.resetARList()
        ARTemp=teName.getARByAssignedName(["tao zhou"])
        
        #ARTemp=teName.getARTurnRoundTime(ARTemp)
        
        test = qaButton % ("tao zhou","tao zhou", "tao zhou")
        test += row % ( "Artifact ID", "Title", "Status", "Category", "Days", "Alert")
        for i in range(0, len(ARTemp)):
            test +=row%(ARTemp[i].artifact_id,ARTemp[i].artifact_title,ARTemp[i].status,ARTemp[i].category,ARTemp[i].turnRoundTime,ARTemp[i].getWarning())
        test += end % ("tao zhou","tao zhou","tao zhou","tao zhou")

        teName.resetARList()
        ARTemp=teName.getARByAssignedName(["Sandy"])
        
        #ARTemp=teName.getARTurnRoundTime(ARTemp)
        
        test += qaButton % ("Sandy","Sandy", "Sandy")
        test += row % ( "Artifact ID", "Title", "Status", "Category", "Days", "Alert")
        for i in range(0, len(ARTemp)):
            test +=row%(ARTemp[i].artifact_id,ARTemp[i].artifact_title,ARTemp[i].status,ARTemp[i].category,ARTemp[i].turnRoundTime,ARTemp[i].getWarning())
        test += end % ("Sandy","Sandy","Sandy","Sandy")

        teName.resetARList()
        ARTemp=teName.getARByAssignedName(["liping yu"])
        
        #ARTemp=teName.getARTurnRoundTime(ARTemp)
        
        test += qaButton % ("liping yu","liping yu", "liping yu")
        test += row % ( "Artifact ID", "Title", "Status", "Category", "Days", "Alert")
        for i in range(0, len(ARTemp)):
            test +=row%(ARTemp[i].artifact_id,ARTemp[i].artifact_title,ARTemp[i].status,ARTemp[i].category,ARTemp[i].turnRoundTime,ARTemp[i].getWarning())
        test += end % ("liping yu","liping yu","liping yu","liping yu")
        return test

    def ESIOwnedByQA(self,teName,qaButton,row,end):
        teName.resetARList()
        ARTemp=teName.getStatus()
       # ARTemp=teName.getARTurnRoundTime(ARTemp)
        
        test = qaButton
        test += row % ( "Artifact ID", "Title", "Assigned To", "Status", "Category", "Days")
        for i in range(0, len(ARTemp)):
            test +=row%(ARTemp[i].artifact_id,ARTemp[i].artifact_title,ARTemp[i].assigned_to_fullname,ARTemp[i].status,ARTemp[i].category,ARTemp[i].turnRoundTime)
        test += end
        return test

    def ESITurnAroundStatus(self,teName,start,end,qaPrefix,artfPrefix,artfStatus,artfSuffix,qaSuffix):
        teName.resetARList()
        ARTemp=teName.getActiveTRStatus(["Sandy"])
        #ARTemp=teName.getARTurnRoundTime(ARTemp)
        tr=teName.getAverageTAT(ARTemp)

        res = qaPrefix % ("Sandy", "Sandy", tr,"0", len(ARTemp),  "Sandy")
        for i in range(0,len(ARTemp)):
        #res += artfPrefix % ("artf123", "artf123","aldkshaflsjkdhfkshdfjsdhkfhs,djfsasdffffffffffffffffffffffffffffffffffffffffffffffffffffffffffhsh,f", "2", "3", "1.3", "artf123")
            res+=artfPrefix %(ARTemp[i].artifact_id,ARTemp[i].artifact_id,ARTemp[i].artifact_title,ARTemp[i].priority,ARTemp[i].severity,ARTemp[i].turnRoundTime,ARTemp[i].artifact_id)
            for j in range(0,len(ARTemp[i].statusChangeList)):
                res+=artfStatus %(time.strftime("%Y-%m-%d",ARTemp[i].statusChangeList[j].changeTime),ARTemp[i].statusChangeList[j].old+" -> "+ARTemp[i].statusChangeList[j].new,ARTemp[i].statusChangeList[j].timeSpan)
            res += artfSuffix
        res += qaSuffix

        teName.resetARList()
        ARTemp=teName.getActiveTRStatus(["tao zhou"])
        tr=teName.getAverageTAT(ARTemp)
        res += qaPrefix % ("Tao", "Tao", tr, "0",len(ARTemp),  "Tao")
        for i in range(0,len(ARTemp)):
        #res += artfPrefix % ("artf123", "artf123","aldkshaflsjkdhfkshdfjsdhkfhs,djfsasdffffffffffffffffffffffffffffffffffffffffffffffffffffffffffhsh,f", "2", "3", "1.3", "artf123")
            res+=artfPrefix %(ARTemp[i].artifact_id,ARTemp[i].artifact_id,ARTemp[i].artifact_title,ARTemp[i].priority,ARTemp[i].severity,ARTemp[i].turnRoundTime,ARTemp[i].artifact_id)
            for j in range(0,len(ARTemp[i].statusChangeList)):
                res+=artfStatus %(time.strftime("%Y-%m-%d",ARTemp[i].statusChangeList[j].changeTime),ARTemp[i].statusChangeList[j].old+" -> "+ARTemp[i].statusChangeList[j].new,ARTemp[i].statusChangeList[j].timeSpan)
            res += artfSuffix
        res += qaSuffix

        teName.resetARList()
        ARTemp=teName.getActiveTRStatus(["liping yu"])
        tr=teName.getAverageTAT(ARTemp)
        res += qaPrefix % ("Irene", "Irene", tr,  "0", len(ARTemp),"Irene")
        for i in range(0,len(ARTemp)):
        #res += artfPrefix % ("artf123", "artf123","aldkshaflsjkdhfkshdfjsdhkfhs,djfsasdffffffffffffffffffffffffffffffffffffffffffffffffffffffffffhsh,f", "2", "3", "1.3", "artf123")
            res+=artfPrefix %(ARTemp[i].artifact_id,ARTemp[i].artifact_id,ARTemp[i].artifact_title,ARTemp[i].priority,ARTemp[i].severity,ARTemp[i].turnRoundTime,ARTemp[i].artifact_id)
            for j in range(0,len(ARTemp[i].statusChangeList)):
                res+=artfStatus %(time.strftime("%Y-%m-%d",ARTemp[i].statusChangeList[j].changeTime),ARTemp[i].statusChangeList[j].old+" -> "+ARTemp[i].statusChangeList[j].new,ARTemp[i].statusChangeList[j].timeSpan)
            res += artfSuffix
        res += qaSuffix
        return res

    def ESITurnAroundTime(self,teName,u,bv,fv,tv,b,te):
        datafile = "static\\%s.data" % u
        pngfile = "static\\%s.png" % u
        scriptfile = "static\\%s.script" % u

        start=time.strptime(fv,r'%m/%d/%Y')
        end=time.strptime(tv,r'%m/%d/%Y')

        teName.resetARList()
        ARTemp1=teName.getTRStatus(start,end,["Sandy"])
       # ARTemp1=teName.getARTurnRoundTime(ARTemp)


        teName.resetARList()
        ARTemp2=teName.getTRStatus(start,end,["tao zhou"])
       # ARTemp2=teName.getARTurnRoundTime(ARTemp)

        teName.resetARList()
        ARTemp3=teName.getTRStatus(start,end,["liping yu"])
       # ARTemp3=teName.getARTurnRoundTime(ARTemp)

        start=time.strptime(fv,r'%m/%d/%Y')
        end=time.strptime(tv,r'%m/%d/%Y')
        #te.resetARList
        #te.setARListP(pList)
        #print pList
        if b=="week":
            te.getByWeekTR(start,end,datafile,ARTemp1,ARTemp2,ARTemp3)
        if b=="day":
            te.getByDayTR(start,end,datafile,ARTemp1,ARTemp2,ARTemp3)
        if b=="month":
            te.getByMonthTR(start,end,datafile,ARTemp1,ARTemp2,ARTemp3)
        
        num=te.getNumber()
        yRange=te.maxTR
        yRange=yRange+yRange*0.4
        script = "set title 'Bug Trend (by %s) %s - %s'\n" % (bv, fv, tv) \
                + "set boxwidth 0.3\n" \
                + "set style fill solid 1.000000 border -1\n" \
                + "set bmargin 5\n" \
                + "set lmargin 5\n" \
                + "set rmargin 5\n" \
                + "set pointsize 2\n" \
                + "set ylabel 'Count'\n" \
                + "set tics scale 0.0\n" \
                + "set grid y\n" \
                + "set xtics rotate by -45\n" \
                + "set term png size 800,600\n" \
                + "set output '%s'\n" % (pngfile) \
                + "set yrange [0:%d]\n"% (yRange) \
                + "set key autotitle columnhead\n" \
                + "plot '%s' using ($0-0.2):2 index 0 with boxes title '   sandy ', \\\n" % (datafile) \
                + "''                      using ($0+0.2):2 index 1 with boxes title ' Irene ', \\\n" \
                + "''                      using ($0):2 index 2 with boxes title 'Tao', \\\n" \
                + "''                      using ($0):2:xticlabels(int($0)%% %d==0 ? strcol(1):'')  index 3 with linespoints lt 4 pt 7 title 'Average Time'\n"  %(num) \
                + "set output\n"

        

        
        sf = open(scriptfile, "w+")
        sf.write(script)
        sf.close()

        progstr = "\"C:\\Program Files (x86)\\gnuplot\\bin\\gnuplot.exe\" %s" % scriptfile
        errormsg=None
        exitcode = os.system(progstr)
        if exitcode != 0:
            print exitcode
            errormsg = "error plotting data."
        return errormsg
