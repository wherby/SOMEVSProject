# Mini-project #6 - Blackjack

import simplegui
import random

# load card sprite - 949x392 - source: jfitz.com
CARD_SIZE = (73, 98)
CARD_CENTER = (36.5, 49)
card_images = simplegui.load_image("http://commondatastorage.googleapis.com/codeskulptor-assets/cards.jfitz.png")

CARD_BACK_SIZE = (71, 96)
CARD_BACK_CENTER = (35.5, 48)
card_back = simplegui.load_image("http://commondatastorage.googleapis.com/codeskulptor-assets/card_back.png")    

# initialize some useful global variables
in_play = False
outcome = ""
score = 0

# define globals for cards
SUITS = ('C', 'S', 'H', 'D')
RANKS = ('A', '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K')
VALUES = {'A':1, '2':2, '3':3, '4':4, '5':5, '6':6, '7':7, '8':8, '9':9, 'T':10, 'J':10, 'Q':10, 'K':10}
isDes=False

# define card class
class Card:
    def __init__(self, suit, rank):
        if (suit in SUITS) and (rank in RANKS):
            self.suit = suit
            self.rank = rank
        else:
            self.suit = None
            self.rank = None
            print "Invalid card: ", suit, rank

    def __str__(self):
        return self.suit + self.rank

    def get_suit(self):
        return self.suit

    def get_rank(self):
        return self.rank

    def draw(self, canvas, pos):
        card_loc = (CARD_CENTER[0] + CARD_SIZE[0] * RANKS.index(self.rank), 
                    CARD_CENTER[1] + CARD_SIZE[1] * SUITS.index(self.suit))
        canvas.draw_image(card_images, card_loc, CARD_SIZE, [pos[0] + CARD_CENTER[0], pos[1] + CARD_CENTER[1]], CARD_SIZE)
        
class Hand:
    def __init__(self):
        self.CList=[]
        

    def __str__(self):
        strTemp="Hand contains"
        for a in self.CList:
            strTemp+=" "+a.suit+a.rank
        return strTemp

    def add_card(self, card):
        self.CList.append(card)

    def get_value(self):
        num=0
        for a in self.CList:
            num+=VALUES[a.rank]
        isAceExist=False
        for a in self.CList:
            if a.rank=='A':
                isAceExist=True
        if isAceExist==True:
            if num+10<=21:
                num=num+10
        
        return num
        
    def draw(self, canvas, pos):
        pass	# draw a hand on the canvas, use the draw method for cards
 
        
# define deck class 
class Deck:
    def __init__(self):
        self.CList=[]
        for a in SUITS:
            for b in RANKS:
                temp=Card(a,b)
                self.CList.append(temp)

    def shuffle(self):
        random.shuffle(self.CList)

    def deal_card(self):
        a=self.CList.pop()
        return a
       
    
    def __str__(self):
        strTemp="Deck contains"
        for a in self.CList:
            strTemp+=" "+a.suit+a.rank
        return strTemp

pHand=Hand()
dHand=Hand()
deck=Deck()
msg1=""
msg2=""

#define event handlers for buttons
def deal():
    global outcome, in_play,pHand,dHand,msg1,msg2,isDes
    deck=Deck()
    deck.shuffle()
    pHand=Hand()
    pHand.add_card(deck.deal_card())
    pHand.add_card(deck.deal_card())
    dHand=Hand()
    dHand.add_card(deck.deal_card())
    dHand.add_card(deck.deal_card())
    msg1="hit or stand ?"
    msg2=""
    isDes=False

    # your code goes here
    
    in_play = True

def hit():
    global pHand,msg1,deck
    if pHand.get_value()<=21:
        pHand.add_card(deck.deal_card())
    else:
        msg1="You busted"    
    pass	# replace with your code below
 
    # if the hand is in play, hit the player
   
    # if busted, assign a message to outcome, update in_play and score
       
def stand():
    global pHand,dHand,deck,msg2,msg1,score,isDes
    isDes=True
    num1=pHand.get_value()
    if num1>21:
        msg2="player busted,dealer win"
        score+=1
    else:
        num2=dHand.get_value()
        while num2<17:
            dHand.add_card(deck.deal_card())
            num2=dHand.get_value()
        if num2>21:
            msg2="deal busted, player win"
            score-=1
        else:
            if num1>num2:
                msg2="player win"
                score-=1
            else:
                msg2="dealer win"
                score+=1
    print msg2
    msg1="New deal ?"
    pass	# replace with your code below
   
    # if hand is in play, repeatedly hit dealer until his hand has value 17 or more

    # assign a message to outcome, update in_play and score

# draw handler    
def draw(canvas):
    # test to make sure that card.draw works, replace with your code below
    global pHand,dHand,isDes
    pos1=[10,400]
    pos2=[10,200]
    for a in pHand.CList:
        a.draw(canvas,pos1)
        pos1[0]+=100
    
    for b in dHand.CList:
        if isDes==False and pos2[0]==10:
            canvas.draw_image(card_back, CARD_BACK_CENTER, CARD_BACK_SIZE, [pos2[0] + CARD_CENTER[0], pos2[1] + CARD_CENTER[1]], CARD_SIZE)
            pos2[0]+=100
        else:
            b.draw(canvas,pos2)
            pos2[0]+=100

    
    canvas.draw_text("player "+msg1,[10,360],20,"yellow")
    canvas.draw_text("dealer "+msg2,[10,160],20,"blue")
    canvas.draw_text("Blackjack    Score:"+str(score),[10,60],20,"red")

# initialization frame
frame = simplegui.create_frame("Blackjack", 600, 600)
frame.set_canvas_background("Green")

#create buttons and canvas callback
frame.add_button("Deal", deal, 200)
frame.add_button("Hit",  hit, 200)
frame.add_button("Stand", stand, 200)
frame.set_draw_handler(draw)


# get things rolling
frame.start()


# remember to review the gradic rubric