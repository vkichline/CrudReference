import httplib

def api_create(data):
    data = '{"Content":"' + data + '"}';
    conn = httplib.HTTPConnection("localhost", 47514)
    conn.connect()
    request = conn.putrequest('POST', '/api/item')
    headers = {}
    headers['Content-Type'] = 'application/json'
    headers['Content-Length'] = len(data)
    for k in headers:
        conn.putheader(k, headers[k])
    conn.endheaders()

    conn.send(data)
    resp = conn.getresponse()
    if 201 == resp.status:
        loc = resp.getheader("location")
        print "SUCCESS: Location =", loc
    else:
        print "ERROR:", resp.status, resp.reason
    conn.close()

api_create("Testing 1, 2, 3")
