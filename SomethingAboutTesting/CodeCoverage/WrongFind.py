# Silly program which return the index of value if the value in the array
# The testcases are 100% code covered,
# But the program is buggy
def find(lst,num,v,low):
    temp=lst[num/2+low]
    #print "temp: ",temp,low,num
    if temp==v:
        print "Branch 4"
        return num/2
    if num==0:
        print "Branch 3"
        return -1-low
    if temp<v:
        print "Branch 2"
        return find(lst,num/2,v,low+num/2)+num/2
    else:
        print "Branch 1"
        return find(lst,num/2,v,low)


lst=[1,2,3,4,5,6,7,8,9]
assert(find(lst,9,3,0)==2)
assert(find(lst,9,5,0)==4)
assert(find(lst,9,7,0)==6)
assert(find(lst,9,10,0)==-1)
print "Every thing seems OK"

assert(find(lst,9,9,0)==8)






