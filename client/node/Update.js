// Test for CrudReference project, PUT command (update existing record)
// Usage:
//  node Update 101 "Changed Data"

var http = require('http'),
    api = require('./settings.json');

function PutData(id, data) {
    data = '{"Content":"' + data + '"}';
    var options = {
        host: api.host,
        path: api.path + '/' + id,
        port: api.port,
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
            'Content-Length': data.length
        }
    };
    var request = http.request(options, function(response) {
        if (200 == response.statusCode) {
            console.log("SUCCESS. Location:", response.headers.location);
            result = true;
        } else {
            console.log("ERROR. Status code =", response.statusCode);
        }
    });
    request.on('error', function (err) {
        console.log("Error:", err.code);
    });
    request.write(data);
    request.end();
}

var args = process.argv;
if (4 != args.length) {
    console.log("### Usage: node Update id data");
    console.log("### Example: node Update 101 'new data'");
} else {
    PutData(args[2], args[3]);
}
