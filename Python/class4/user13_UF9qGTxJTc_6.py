# Implementation of classic arcade game Pong

import simplegui
import random

# initialize globals - pos and vel encode vertical info for paddles
WIDTH = 600
HEIGHT = 400       
BALL_RADIUS = 20
PAD_WIDTH = 8
PAD_HEIGHT = 80
HALF_PAD_WIDTH = PAD_WIDTH / 2
HALF_PAD_HEIGHT = PAD_HEIGHT / 2

p1=0
p2=0
init_pos=[WIDTH/2,HEIGHT/2]
ball_pos=[WIDTH/2,HEIGHT/2]
ball_vel=[2,0]
paddle1_pos=HALF_PAD_HEIGHT
paddle2_pos=HALF_PAD_HEIGHT
pad1_pos=[[0,HEIGHT/2-40],[1,HEIGHT/2-40],[1,HEIGHT/2+30],[0,HEIGHT/2+30]]
pad2_pos=[[WIDTH-1,HEIGHT/2-40],[WIDTH,HEIGHT/2-40],[WIDTH,HEIGHT/2+30],[WIDTH-1,HEIGHT/2+30]]
paddle1_vel=0
paddle2_vel=0
isRight=-1

# helper function that spawns a ball by updating the 
# ball's position vector and velocity vector
# if right is True, the ball's velocity is upper right, else upper left
def ball_init(right):
    global ball_pos, ball_vel # these are vectors stored as lists
    global p1,p2
    ball_pos[0]=init_pos[0]
    ball_pos[1]=init_pos[1]
    ball_vel[0]=random.randrange(120, 240)/60*right
    ball_vel[1]=random.randrange(60, 180)/60
    p1=0
    p2=0
# define event handlers

def new_game():
    global paddle1_pos, paddle2_pos, paddle1_vel, paddle2_vel  # these are floats
    global score1, score2  # these are ints
    global isRight
    if isRight==1:
        isRight=-1
    else:
        isRight=1
    ball_init(isRight)

def draw(c):
    global score1, score2, paddle1_pos, paddle2_pos, ball_pos, ball_vel
    global p1,p2
    # update paddle's vertical position, keep paddle on the screen
        
    # draw mid line and gutters
    c.draw_line([WIDTH / 2, 0],[WIDTH / 2, HEIGHT], 1, "White")
    c.draw_line([PAD_WIDTH, 0],[PAD_WIDTH, HEIGHT], 1, "White")
    c.draw_line([WIDTH - PAD_WIDTH, 0],[WIDTH - PAD_WIDTH, HEIGHT], 1, "White")
    
    if pad2_pos[0][1]+paddle2_vel>=0 and pad2_pos[2][1]+paddle2_vel<HEIGHT:
        pad2_pos[0][1]+=paddle2_vel
        pad2_pos[1][1]+=paddle2_vel
        pad2_pos[2][1]+=paddle2_vel
        pad2_pos[3][1]+=paddle2_vel

    if pad1_pos[0][1]+paddle1_vel>=0 and pad1_pos[2][1]+paddle1_vel<HEIGHT:
        pad1_pos[0][1]+=paddle1_vel
        pad1_pos[1][1]+=paddle1_vel
        pad1_pos[2][1]+=paddle1_vel
        pad1_pos[3][1]+=paddle1_vel
    
    # draw paddles
    c.draw_polygon(pad1_pos, 12, "Green")
    c.draw_polygon(pad2_pos, 12, "Green")
    # update ball
             
    # draw ball and scores

    paddle1_pos +=paddle1_vel
    paddle2_pos +=paddle2_vel

    ball_pos[0]+=ball_vel[0]
    ball_pos[1]+=ball_vel[1]
    if ball_pos[0]<=BALL_RADIUS or ball_pos[0]+BALL_RADIUS>WIDTH-1:
        ball_vel[0]=-ball_vel[0]
        if ball_pos[0]<=BALL_RADIUS:
            if pad1_pos[0][1]>ball_pos[1] or pad1_pos[2][1]<ball_pos[1]:
                p2+=1
            else:
                ball_vel[0]+=ball_vel[0]*0.1
                ball_vel[1]+=ball_vel[1]*0.1
        else:
            if pad2_pos[0][1]>ball_pos[1] or pad2_pos[2][1]<ball_pos[1]:
                p1+=1
            else:
                ball_vel[0]+=ball_vel[0]*0.1
                ball_vel[1]+=ball_vel[1]*0.1
                
    if ball_pos[1]<=BALL_RADIUS or ball_pos[1]+BALL_RADIUS>HEIGHT-1:
        ball_vel[1]=-ball_vel[1]        
        
    c.draw_circle(ball_pos,BALL_RADIUS,2,"Red","White")
    c.draw_text(str(p1),(50, 50), 24, "Blue")
    c.draw_text(str(p2),(350, 50), 24, "Blue")
    
def keydown(key):
    global paddle1_vel, paddle2_vel
    if key==simplegui.KEY_MAP["down"]:
        paddle2_vel+=5
    if key==simplegui.KEY_MAP["up"]:
        paddle2_vel-=5
    if key==simplegui.KEY_MAP["s"]:
        paddle1_vel+=5
    if key==simplegui.KEY_MAP["w"]:
        paddle1_vel-=5
   
def keyup(key):
    global paddle1_vel, paddle2_vel
    if key==simplegui.KEY_MAP["down"]:
        paddle2_vel-=5
    if key==simplegui.KEY_MAP["up"]:
        paddle2_vel+=5
    if key==simplegui.KEY_MAP["s"]:
        paddle1_vel-=5
    if key==simplegui.KEY_MAP["w"]:
        paddle1_vel+=5

# create frame
frame = simplegui.create_frame("Pong", WIDTH, HEIGHT)

frame.set_keydown_handler(keydown)
frame.set_keyup_handler(keyup)
frame.set_draw_handler(draw)
frame.add_button("Restart",new_game,100)


# start frame
frame.start()
