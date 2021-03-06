// Test for CrudReference project, GET command (fetch existing record)
// With param: get item by id
// Without param: get all items
// Usage:
//  node Read
//  node Read 101

var http = require('http'),
    stringDecoder = require('string_decoder').StringDecoder,
    api = require('./settings.json');

// Poss "" for id if get all is desired
function GetData(id) {
    var options = {
        host: api.host,
        path: api.path + '/' + id,
        port: api.port,
        method: 'GET'
    };
    var request = http.request(options, function (response) {
        if (200 == response.statusCode) {
            response.on('data', function (data) {
                // data is returned as a buffer, must be converted to a string
                var decoder = new stringDecoder('utf8');
                var text = decoder.write(data);
                console.log("SUCCESS.", text);
        });
        } else {
            console.log("ERROR. Status code =", response.statusCode);
        }
    });
    request.on('error', function (err) {
        console.log("Error:", err.code);
    });
    request.end();
}

var args = process.argv;
if (2 != args.length && 3 != args.length) {
    console.log("### Usage: 'node Read' or 'node Read id'");
    console.log("### Example: 'node Read' or 'node Read 101'");
} else {
    var id = (3 == args.length) ? args[2] : '';
    GetData(id);
}
