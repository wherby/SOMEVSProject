

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
#['aa', 'bb']
