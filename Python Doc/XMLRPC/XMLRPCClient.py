import xmlrpclib
server=xmlrpclib.Server('http://localhost:8000')
print server.chop_in_half("I am a configure")
print server.repeat("Repeat aa",3)
print server._string("<=underscore")
