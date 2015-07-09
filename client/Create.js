// Test for CrudReference project, POST command
// usage:
//  node Create "post this data"

var http = require('http')
apiHost = 'localhost',
apiPath = '/api/items',
apiPort = 47514;

function PostData(data) {
    data = '{"Content":"' + data + '"}';
    var options = {
        host: apiHost,
        path: apiPath,
        port: apiPort,
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Content-Length': data.length
        }
    };
    var request = http.request(options, function (res) {
        var loc = res.headers.location;
        console.log("SUCCESS. Location:", loc);
        result = true;
    });
    request.on('error', function (err) {
        console.log("Error:", err.code);
    });

    request.write(data);
    request.end();
}

if (3 != process.argv.length) {
    console.log("### Usage: node Create data");
    console.log("### Example: node Create 'Test Data'");
} else {
    PostData(process.argv[2]);
}


