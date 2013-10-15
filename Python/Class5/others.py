# implementation of card game - Memory

import simplegui
import random
image = {}
image[0] = simplegui.load_image("https://coursera-instructor-photos.s3.amazonaws.com/a1/6188f0b9efa70f7865ba862575445c/Bill-Howe.jpg")
image[1] = simplegui.load_image("https://coursera-instructor-photos.s3.amazonaws.com/98/5115520ba6f050e1e231549e8576ca/RandyLevequeImage.jpg")
image[2] = simplegui.load_image("https://coursera-instructor-photos.s3.amazonaws.com/6a/14f7b085d5146feed5880da38093a4/thaddeus.jpg")
image[3] = simplegui.load_image("https://coursera-instructor-photos.s3.amazonaws.com/6f/f2256add985d715e6ac5d12c3ec790/Eric-Zivot.jpg")
image[4] = simplegui.load_image("https://coursera-instructor-photos.s3.amazonaws.com/22/e4e013b97d49fb81ff3dab0b8bc8a8/SWong_cropped.jpg")
image[5] = simplegui.load_image("https://coursera-instructor-photos.s3.amazonaws.com/d4/d9f8582627975a8722a232d862cef8/John-Greiner.jpg")
image[6] = simplegui.load_image("https://coursera-instructor-photos.s3.amazonaws.com/79/36fc6aa708f83866a0c0f8cdae5b7c/SRixner.jpg")
image[7] = simplegui.load_image("https://coursera-instructor-photos.s3.amazonaws.com/5e/fc4d05adc74aa6e254a655ce874f61/JWarren.jpg")
image_center = (90, 90)
image_size = (180, 180)
image_shown = (100, 100)
# helper function to initialize globals
def init():
    global deck2, exp, state, count
    state = 0
    count = 0
    label.set_text("Moves = "+str(count))
    list_a = range(8)
    list_b = range(8)
    random.shuffle(list_a)
    random.shuffle(list_b)
    deck = list_a + list_b
    deck2 = [deck[i:i+4] for i in range(0,len(deck),4)]
    exp = [[False]*4 for x in range(4)]

     
# define event handlers
def mouseclick(pos):
    # add game state logic here
    global state, last_i, last_k, this_i, this_k, count
    pos = list(pos)
    i = int(pos[0]/100)
    k = int(pos[1]/100)
    if not exp[i][k]:
        if state == 0:
            exp[i][k] = True
            state = 1
            last_i = i
            last_k = k
        elif state == 1:
            exp[i][k] = True
            this_i = i
            this_k = k
            state = 2
            count += 1
            label.set_text("Moves ="+str(count))
        else:
            if deck2[last_i][last_k] != deck2[this_i][this_k]:
                exp[last_i][last_k] = False
                exp[this_i][this_k] = False
                exp[i][k] = True
                last_i = i
                last_k = k
                state = 1
            else:
                exp[i][k] = True
                last_i = i
                last_k = k
                state = 1
    
                        
# cards are logically 100x100 pixels in size    
def draw(canvas):
    for i in range(4):
        for k in range(4):
            if not exp[i][k]:
                canvas.draw_polygon([(100*i, 100*k), (100*i, 100+100*k), (i*100+100, 100+100*k), (100+100*i, 100*k)], 3, 'red', 'green')
            else:
                canvas.draw_image(image[deck2[i][k]], image_center, image_size, [50+100*i,50+100*k], image_shown)


# create frame and add a button and labels
frame = simplegui.create_frame("Memory", 400, 400)
frame.add_button("Restart", init)
label = frame.add_label("Moves = 0")

# initialize global variables
init()

# register event handlers
frame.set_mouseclick_handler(mouseclick)
frame.set_draw_handler(draw)

# get things rolling
frame.start()


# Always remember to review the grading rubric