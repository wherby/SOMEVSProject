def packetitem(item,pkg=None):
    if pkg is None:
        pkg=[]
    pkg.append(item)
    return pkg


if __name__ =='__main__':
    a=packetitem("aa");
    print a;
    b=packetitem("bb");
    print b




#result wil be
#['aa']
#['bb']

