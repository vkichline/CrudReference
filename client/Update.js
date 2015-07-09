// Test for CrudReference project, PUT command (update existing record)
// usage:
//  node Update 101 "Changed Data"

var http = require('http'),
    apiHost = 'localhost',
    apiPath = '/api/items/',
    apiPort = 47514;

function PutData(id, data) {
    data = '{"Content":"' + data + '"}';
    var options = {
        host: apiHost,
        path: apiPath + id,
        port: apiPort,
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
            'Content-Length': data.length
        }
    };
    var request = http.request(options, function (res) {
        if (200 == res.statusCode) {
            var loc = res.headers.location;
            console.log("SUCCESS. Location:", loc);
            result = true;
        } else {
            console.log("ERROR. Status code =", res.statusCode);
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
