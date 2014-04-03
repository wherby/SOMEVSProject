#This is function which will return the result with multiply by zero
#

#include <Python.h>
from decimal import *

def MultiplyWithZero(ob):
    return 0


assert(MultiplyWithZero(5)==5*0)
assert(MultiplyWithZero(Decimal('NaN'))==Decimal('NaN')*0)
