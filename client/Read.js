// Test for CrudReference project, GET command (fetch existing record)
// With param: get item by that id
// Without paramL get all items
// usage:
//  node Read
//  node Read 101

var http = require('http'),
    StringDecoder = require('string_decoder').StringDecoder,
    apiHost = 'localhost',
    apiPath = '/api/items/',
    apiPort = 47514;

// Poss "" for id if get all is desired
function GetData(id) {
    var options = {
        host: apiHost,
        path: apiPath + id,
        port: apiPort,
        method: 'GET'
    };
    var request = http.request(options, function (res) {
        res.on('data', function (data) {
            var decoder = new StringDecoder('utf8');
            var text = decoder.write(data);
            console.log("SUCCESS.", text);
            result = true;
        });
    });
    request.on('error', function (err) {
        console.log("Error:", err.code);
    });
    request.end();
}

var args = process.argv;
if (2 != args.length && 3 != args.length) {
    console.log("### Usage: 'node Read' or 'node Read 101'");
} else {
    var id = '';
    if (3 == args.length) {
        id = args[2];
    }
    GetData(id);
}
