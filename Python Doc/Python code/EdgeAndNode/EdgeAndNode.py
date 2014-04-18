#The code create the feature node and edge mapping


nodes={}

def getnode(name):
    if name in nodes:
        node1=nodes[name]
    else:
        node1=nodes[name]=node(name)
    return node1


class node(object):
    def __init__(self,name):
        self.name=name
        self.edgelist=[]

class edge(object):
    def __init__(self,name1,name2):
        self.nodes=getnode(name1),getnode(name2)
        for n in self.nodes:
            n.edgelist.append(self)

    def __repr__(self):
        return self.nodes[0].name+self.nodes[1].name


if __name__=="__main__":
    edge('A','B')
    edge('B','C')
    edge('C','D')
    edge('D','A')
    print getnode('A').edgelist

