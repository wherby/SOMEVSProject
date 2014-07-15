
decent="    "
endline="\r\n"
class ParseForClass:
    def __init__(self):
        self._vdict=dict();
        self._mdict=dict();
        self.className="";
        pass

    def processString(self,strIn):
        if strIn.find("=")<0 and strIn.strip().find("def")<0:
            self.className=strIn
            print "new obiect"
            pass
        if strIn.strip().find("def")==0:
            print "new method"
            indexleft=strIn.find('(')
            indexright=strIn.find(')')
            res=strIn.split(':',2)
            value=res[1];
            start=strIn.find(" ")
            key=res[0][start+1:]
            self._mdict[key]=value
            #for key1 in self._mdict.keys():print "Key:",key1, " value:",self._mdict[key1]
            pass
        if strIn.find("=")>0 and strIn.strip().find("def")<0:
            res=strIn.split("=",2)
            start=res[0].find('.')
            key=res[0][start+1:]
            self._vdict[key]=res[1]
            #for key1 in self._vdict.keys():print "Key:",key1, " value:",self._vdict[key1]
            print "new value"
            pass
        pass

    def process(self,StrIn):
        pass

    def processing(self):
        temp=""
        temp+="class %s:"%self.className+endline
        temp+=decent+"def __init__(self):"+endline
        #print temp
        for tk in self._vdict.keys():
            temp+=decent*2+ "self."+tk+"=%s"%self._vdict[tk]+endline
        temp+=endline
        for tk in self._mdict.keys():
            temp+=decent+"def %s:"%tk+endline
            temp+=decent*2+self._mdict[tk]
            temp+=endline
        
        print temp
        pass















if __name__=='__main__':
    pa=ParseForClass();
    pa.processString("abc");
    pa.processString('def test(self):{print "test"}');
    pa.processString('def test(self):{print "test1"}');
    pa.processString("abc.id=10");
    pa.processing();
