import os
import re

def gengraph(viewby, f, t):

    r = re.compile('\d\d/\d\d/\d\d\d\d')

    errormsg = None
    
    if not r.match(fv):
        errormsg = 'From/To must be conformant to mm/dd/yy format.'
        return (None, errormsg)

    if not r.match(tv):
        errormsg = 'From/To must be conformant to mm/dd/yy format.'
        return (None, errormsg)

    f = time.strptime(fv, '%M/%d/%Y')
    t = time.strptime(tv, '%M/%d/%Y')

    if f >= t:
        errormsg = 'From must be < To.'
        return (None, errormsg)

    if not viewby or viewby == '':
        viewby = 'week' 

    voption = "-%c" % bv[0]

    u = uuid.uuid1()
    datafile = "static\\%s.data" % u
    pngfile = "static\\%s.png" % u
    scriptfile = "static\\%s.script" % u

    progstr = "D:\\SRC\\tfplot\\bin\\Debug\\tfplot.exe bugstatus %s -release 2.1 -startdate %s -enddate %s > %s" % (viewby, fv, tv, datafile) 
    exitcode = os.system(progstr)
    if exitcode != 0:
        errormsg = "error getting data."
        return (None, errormsg)

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
            + "set key autotitle columnhead\n" \
            + "plot '%s' using ($0-0.2):2 index 0 with boxes title '   Incoming Bugs', \\\n" % (datafile) \
            + "''                      using ($0+0.2):2 index 1 with boxes title '     Closed Bugs', \\\n" \
            + "''                      using ($0):2:xticlabels(1) index 2 with linespoints lw 3 title 'Active P1/P2 Bugs'\n" \
            + "set output\n"
    
    sf = open(scriptfile, "w+")
    sf.write(script)
    sf.close()

    progstr = "\"C:\\Program Files (x86)\\gnuplot\\bin\\gnuplot.exe\" %s" % scriptfile
    exitcode = os.system(progstr)
    if exitcode != 0:
        errormsg = "error plotting data."
        return (None, errormsg)

    return (uuid, None)


