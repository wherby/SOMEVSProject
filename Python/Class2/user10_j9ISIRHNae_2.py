# template for "Guess the number" mini-project
# input will come from buttons and an input field
# all output for the game will be printed in the console
import simplegui
import random
# initialize global variables used in your code
times=0
rang=0
num=0
anum=0
# define event handlers for control panel
    
def range100():
    global rang,num,times,anum
    times=0
    rang=100
    anum=7
    num=random.randrange(0,100)
    # button that changes range to range [0,100) and restarts

def range1000():
    global rang,num,times,anum
    times=0
    rang=1000
    anum=10
    num=random.randrange(0,1000)
    # button that changes range to range [0,1000) and restarts
    
def get_input(guess):
    global num,times
    times=times+1
    if times>anum:
        print "you use out all changes,restart game set game to range100"
        print "\n"
        range100()
        return
    log="you tried ",times," time(s)"
    gn=int(guess)
    print num
    if(gn==num):        
        print "Right \n",log
        print "\n"
        range100()
    elif(gn>num):
        print "lower \n",log
    else:
        print "highter \n",log
    # main game logic goes here	

    
# create frame
frame=simplegui.create_frame("Guess",200,200)
frame.add_button("range100",range100,100)
frame.add_button("range1000",range1000,100)
frame.add_input("guess",get_input,100)
# register event handlers for control elements


# start frame
frame.start()

# always remember to check your completed program against the grading rubric
