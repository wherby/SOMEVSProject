#import GlobalVars
import re;
class ReadFile:
    def __init__(self,fileName):
        self.fileName=fileName
        self.hands=[]
        pass

    def readAllLine(self):
        file_obj=open(self.fileName)
        try:
            self.allLines=file_obj.readlines()
        finally:
            file_obj.close()

    def splitToHand(self):
        temp="";
        count=0;
        num=0;
        for line in self.allLines:
            count=count+1
            if count==1:
                print "Line 1:"+line;
            if line.strip()=="" and temp!="":
                num=num+1;
                self.hands.append(temp);
                #print num;
                #print temp;
                temp="";
            else:
                if temp=="":
                    temp=line.strip()
                else:
                    temp=temp+line
                
    def getHands(self):
        return self.hands
    
    def __str__(self):
        return self.hands[1];


class Hand:
    def __init__(self,lines):
        self.lines=lines.splitlines(True);
        self.heros=[]
        print lines;

    def getNumber(self):
        m = re.match(r'(.*)(Hand #)(\d*)(:)(.*)', self.lines[0])
        #print "m.group(1,2):", m.group(3)
        if m!=None:
            self.ID=m.group(3)

    def getHeros(self):
        for line in self.lines:
            m = re.match(r'(Seat \d: )(\w*) (.*)',line);
            if m!=None:
                if self.heros.count(m.group(2))==0:
                    self.heros.append(m.group(2))         
            pass

    def __str__(self):
        temp="Heros :\n"
        for hero in self.heros:
            temp+="  "+hero+" \n"
        return temp+ "Number: "+self.ID

if __name__=="__main__":
    temp=ReadFile(".\HandHistory\scuipio\HH20121012 Halley - $0.01-$0.02 - USD No Limit Hold'em.txt")
    temp.readAllLine();
    temp.splitToHand();
    print temp
    hands=temp.getHands();
    print hands[1];
    a=Hand(hands[1])
    a.getNumber();
    a.getHeros();
    print a;
    
