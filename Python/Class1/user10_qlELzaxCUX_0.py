# Rock-paper-scissors-lizard-Spock template
import random

# The key idea of this program is to equate the strings
# "rock", "paper", "scissors", "lizard", "Spock" to numbers
# as follows:
#
# 0 - rock
# 1 - Spock
# 2 - paper
# 3 - lizard
# 4 - scissors

# helper functions
nameList=["rock","Spock","paper","lizard","scissors"]

def number_to_name(number):
    # fill in your code below
    return nameList[number]
        
    # convert number to a name using if/elif/else
    # don't forget to return the result!

    
def name_to_number(name):
    # fill in your code below
    for i in range(0,len(nameList)):
        if nameList[i]==name:
            return i
    # convert name to number using if/elif/else
    # don't forget to return the result!


def rpsls(name): 
    # fill in your code below

    # convert name to player_number using name_to_number
    player_number=name_to_number(name)
    
    # compute random guess for comp_number using random.randrange()
    comp_number=random.randrange(0,5)
    
    # compute difference of player_number and comp_number modulo five
    diff=(player_number-comp_number +5)%5
    # use if/elif/else to determine winner
    if diff==0:
        result="Player and computer tie!"
    elif diff==3 or diff==4:
        result="Computer wins!"
    else:
        result="Player wins!"
         
        

    # convert comp_number to name using number_to_name
    comp_name=number_to_name(comp_number)
    # print results
    print "Player chooses "+name
    print "Computer chooses "+comp_name
    print result+"\n"
    
# test your code
rpsls("rock")
rpsls("Spock")
rpsls("paper")
rpsls("lizard")
rpsls("scissors")

# always remember to check your completed program against the grading rubric


