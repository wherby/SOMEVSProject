import copy
def freshdefaults(f):
    fdefaults=f.func_defaults
    def refresher(*args,**kwds):
        f.func_defaults=copy.deepcopy(fdefaults)
        return f(*args,**kwds)
    return refresher

@freshdefaults
def packetitem(item,pkg=[]):
    pkg.append(item)
    return pkg


if __name__ =='__main__':
    a=packetitem("aa");
    print a;
    b=packetitem("bb");
    print b


#result will be
#['aa']
#['bb']
