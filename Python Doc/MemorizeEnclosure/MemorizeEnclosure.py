#using memorize enclosure to solve fib,
#
#
def memorize(fn):
    memo={}
    def memorizer(*param_tuple,**kwds_dict):
        if kwds_dict:
            return fn(*param_tuple,**kwds_dict)
        try:
            try:
                return memo[param_tuple]
            except KeyError:
                memo[param_tuple]=result=fn(*param_tuple)
                return result
        except TypeError:
            return fn(*param_tuple)
    return memorizer




def fib(n):
    if n<2:
        print "try to get fib(2)"
        return 1
    return fib(n-1)+fib(n-2)

@memorize
def fib2(n):
    if n<2:
        print "try to get fib(2)"
        return 1
    return fib(n-1)+fib(n-2)


if __name__=="__main__":
    fib=memorize(fib)
    print fib(10)
    print fib2(11)
    
    
