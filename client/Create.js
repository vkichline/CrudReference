// Test for CrudReference project, POST command
// Usage:
//  node Create "post this data"

var http = require('http'),
    api = require('./settings.json');

function PostData(data) {
    data = '{"Content":"' + data + '"}';
    var options = {
        host: api.host,
        path: api.path,
        port: api.port,
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Content-Length': data.length
        }
    };
    var request = http.request(options, function (response) {
        console.log("SUCCESS. Location:", response.headers.location);
    });
    request.on('error', function (err) {
        console.log("Error:", err.code);
    });
    request.write(data);
    request.end();
}

var args = process.argv;
if (3 != args.length) {
    console.log("### Usage: node Create data");
    console.log("### Example: node Create 'Test Data'");
} else {
    PostData(args[2]);
}


