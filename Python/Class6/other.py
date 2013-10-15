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
dealer_one = True
outcome = ""
score = 0
player_hand = []
dealer_hand = []
deck = []
status = ""
action = ""
score = 0
status_color = "Red"

# define globals for cards
SUITS = ('C', 'S', 'H', 'D')
RANKS = ('A', '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K')
VALUES = {'A':1, '2':2, '3':3, '4':4, '5':5, '6':6, '7':7, '8':8, '9':9, 'T':10, 'J':10, 'Q':10, 'K':10}


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


    def draw_back(self, canvas, pos):
        card_loc = (CARD_BACK_CENTER[0], 
                    CARD_BACK_CENTER[1])
        canvas.draw_image(card_back, card_loc, CARD_BACK_SIZE, [pos[0] + CARD_BACK_CENTER[0], pos[1] + CARD_BACK_CENTER[1]], CARD_BACK_SIZE)


        
        
# define hand class
class Hand:
    
    
    def __init__(self):
        self.cards = []

    def __str__(self):
        str_hand = ""
        for s in self.cards :
            str_hand += str(s) + " "
        return str_hand
            

    def add_card(self,card):
        self.cards.append(card)

    def get_value(self):
        total = 0
        ace_in_play = 0
        for c in self.cards :
            total += VALUES[c.get_rank()]
            if VALUES[c.get_rank()] == 1 :
                ace_in_play = 10
        if (total + ace_in_play) <= 21 :
            total += ace_in_play
        return total
        
    def draw(self, canvas, offset):
        global dealer_one
        for c in self.cards :
#            print 1
            if dealer_one and self.cards.index(c) == 0 and in_play:
                c.draw_back(canvas, [self.cards.index(c)*100+90, offset])
                dealer_one = False
            else :
                c.draw(canvas, [self.cards.index(c)*100+90, offset])
        
# define deck class 
class Deck:
    def __init__(self):
        self.deck = []
        for s in SUITS :
            for r in RANKS :
              self.deck.append(Card(s,r))
    def shuffle(self):
        # add cards back to deck and shuffle
        random.shuffle(self.deck)

    def deal_card(self):
        return self.deck.pop(0)
    
    def __str__(self):
        str_deck = ""
        for s in self.deck :
            str_deck += str(s) + " "
        return str_deck


def you_win():
    global status, action, score, in_play, status_color
    status = "You Win"
    action = "New deal?"
    score += 1
    in_play = False    
    status_color = "Red"
def you_loose():
    global status, action, score, in_play, status_color
    status = "You Lose"
    action = "New deal?"
    score -= 1
    in_play = False
    status_color = "Blue"

#define event handlers for buttons
def deal():
    global outcome, in_play, player_hand, dealer_hand, deck
    global status, action, score, dealer_one

    # your code goes here
 
    if in_play :
        print "in_play", in_play
        status = "You Lose"
        score -= 1
    deck = Deck()
    deck.shuffle()
    print deck

    
    player_hand = Hand()
    dealer_hand = Hand()
    
    player_hand.add_card(deck.deal_card())
    dealer_hand.add_card(deck.deal_card())
    player_hand.add_card(deck.deal_card())
    dealer_hand.add_card(deck.deal_card())

    print "Player Hand",player_hand, player_hand.get_value()
    print "Dealer Hand",dealer_hand, dealer_hand.get_value()
    print deck
    
    print 

    status = ""
    action = "Hit or stand"
    in_play = True

    
def hit():
    
    global player_hand
    if in_play :
        if player_hand.get_value() <= 21 :
            player_hand.add_card(deck.deal_card())
            if player_hand.get_value() > 21 :
                you_loose()
        

       
def stand():
    
    global player_hand, dealer_hand, dealer_one
    if in_play :
        if dealer_hand.get_value() >= player_hand.get_value() :
            you_loose()
        elif dealer_hand.get_value() >= 17 and dealer_hand.get_value() < player_hand.get_value():
            you_win()
        else :
            while dealer_hand.get_value() <= 17 :
                print dealer_hand.get_value()
                dealer_hand.add_card(deck.deal_card())
                if dealer_hand.get_value() > 21 or (dealer_hand.get_value() >= 17 and dealer_hand.get_value() < player_hand.get_value()) :
                    you_win()
                    break
                elif dealer_hand.get_value() >= player_hand.get_value() :
                    you_loose()
                    break


# draw handler    
def draw(canvas):
    global status, action, score, dealer_one, status_color
    dealer_one = True
    # test to make sure that card.draw works, replace with your code below
#    card.draw(canvas, [300, 300])
    canvas.draw_text("Blackjack", (100, 100), 50, "#33CCCC")
    canvas.draw_text("Score " + str(score), (400, 100), 30, "Black")
    canvas.draw_text("Dealer", (100, 175), 24, "Black")
    canvas.draw_text(status, (230, 175), 24, status_color)
    dealer_hand.draw(canvas,200)
    canvas.draw_text("Player", (100, 375), 24, "Black")
    canvas.draw_text(action, (230, 375), 24, "Black")
    player_hand.draw(canvas,400)


# initialization frame
frame = simplegui.create_frame("Blackjack", 800, 600)
frame.set_canvas_background("Green")

#create buttons and canvas callback
frame.add_button("Deal", deal, 200)
frame.add_button("Hit",  hit, 200)
frame.add_button("Stand", stand, 200)
frame.set_draw_handler(draw)


# get things rolling
frame.start()
deal()


# remember to review the gradic rubric