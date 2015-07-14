import httplib

def api_read():
    conn = httplib.HTTPConnection("localhost", 47514)
    conn.request("GET", "/api/item")
    resp = conn.getresponse()
    if 200 == resp.status:
        data = resp.read()
        return "Response:", data
    else:
        return "ERROR:", resp.status, resp.reason

print api_read()

# Do I need conn.close()?  where?
# Need GetById
