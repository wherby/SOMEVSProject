#import GlobalVars

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
        for line in self.allLines:
            if line.strip()=="":
                self.hands.append(temp);
                temp='';
            else:
                if temp=="":
                    #print line
                    temp=line
                    print temp
                else:
                    temp=temp+line
                

    def __str__(self):
        return self.hands[0];



if __name__=="__main__":
    temp=ReadFile(".\HandHistory\scuipio\HH20121012 Halley - $0.01-$0.02 - USD No Limit Hold'em.txt")
    temp.readAllLine();
    temp.splitToHand();
    print temp
    
    
