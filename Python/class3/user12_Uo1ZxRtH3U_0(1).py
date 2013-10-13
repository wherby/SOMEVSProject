# template for "Stopwatch: The Game"
import simplegui

# define global variables
width=500
height=500
ticks=0
nt=0
nr=0
isStop=True

# define helper function format that converts time
# in tenths of seconds into formatted string A:BC.D
def format(t):
    ticksT=int(ticks)
    a=int(ticksT/600)
    b=int(ticksT%600/10)
    c=ticksT%10
    print a,b,c
   # return str(c)
    return "%d:%02d:%d"%(a,b,c)
    
# define event handlers for buttons; "Start", "Stop", "Reset"
def Start():
    global isStop
    isStop=False
    timer.start()
    
def Stop():
    global isStop
    if isStop==True:
        return 
    isStop=True
    global nt,nr
    timer.stop()
    nt=nt+1
    if ticks%10==0:
        nr=nr+1
    
    
def Restart():
    global ticks,nt,nr
    ticks=0
    nt=0
    nr=0
    timer.stop()

# define event handler for timer with 0.1 sec interval
def tick():
    global ticks
    ticks=ticks+1
    

# define draw handler
def draw(canvas):
    canvas.draw_text(format(ticks),[200,300],36,"White")
    canvas.draw_text(str(nr)+"/"+str(nt),[200,200],36,"White")
    
# create frame
frame=simplegui.create_frame("home",width,height)
timer=simplegui.create_timer(1,tick)


# register event handlers
frame.add_button("Start",Start,100)
frame.add_button("Stop",Stop,100)
frame.add_button("Restart",Restart,100)
frame.set_draw_handler(draw)

# start frame
frame.start()

# Please remember to review the grading rubric
