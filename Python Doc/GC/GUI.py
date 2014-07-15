from Tkinter import *

class notebook(object):
    def __init__(self,master,side=LEFT):
        self.active_fr=None
        self.count=0
        self.choice=IntVar(0)
        if side in (TOP,BOTTOM):self.side=LEFT
        else: self.side=TOP
        self.rb_fr=Frame(master,borderwidth=2,relief=RIDGE)
        self.rb_fr.pack(side=side,fill=BOTH)
        self.screen_fr=Frame(master,borderwidth=2,relief=RIDGE)
        self.screen_fr.pack(fill=BOTH)
        self.rb_fr.master.maxsize(1000, 400)


    def __call__(self):
        return self.screen_fr

    def add_screen(self,fr,title):
        b=Radiobutton(self.rb_fr,text=title,indicatoron=0,
                      variable=self.choice,value=self.count,
                      command=lambda:self.display(fr))
        b.pack(fill=BOTH,side=self.side)
        if not self.active_fr:
            fr.pack(fill=BOTH,expand=1)
            self.active_fr=fr

        self.count+=1

    def display(self,fr):
        self.active_fr.forget()
        fr.pack(fill=BOTH,expand=1)
        self.active_fr=fr

class Cont:
    def __init__(self):
        root=Tk()
        nb=notebook(root,LEFT)
        f1=Frame(nb())
        b1=Button(f1,text="button 1",command=self.say)
        self.text="mp"
        self.e1=Entry(f1)
        b1.pack(fill=BOTH,expand=1)
        self.e1.pack(fill=BOTH,expand=1)
        self.tx=Text(f1)
        self.tx.pack(fill=BOTH,expand=1)
    
        f2=Frame(nb())
        nb.add_screen(f1,"screen 1")
        nb.add_screen(f2,"screen 2")
        root.geometry('800x800+0+0')
        root.mainloop()

    def say(self):
        #print "test",self.e1.get()
        self.tx.insert(INSERT,self.e1.get())
                
if __name__=='__main__':
    control=Cont()

