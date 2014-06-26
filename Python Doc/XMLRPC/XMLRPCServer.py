import SimpleXMLRPCServer
class StringFunctions(object):
    def __ini__(self):
        import string
        self.python_string=string

    def _privateFunction(self):
        return "You'll never call this"

    def chop_in_half(self,astr):
        return astr[:len(astr)/2]

    def repeat(self,astr,time):
        return astr*time


if __name__=="__main__":
    server=SimpleXMLRPCServer.SimpleXMLRPCServer(("localhost",8000))
    server.register_instance(StringFunctions())
    server.register_function(lambda astr: '_'+astr,'_string')
    server.serve_forever()

    
    
