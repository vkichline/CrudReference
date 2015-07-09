// Test for CrudReference project, DELETE command (remove existing record)
// usage:
//  node Delete 101

var http = require('http'),
    apiHost = 'localhost',
    apiPath = '/api/items/',
    apiPort = 47514;

function DeleteData(id) {
    var options = {
        host: apiHost,
        path: apiPath + id,
        port: apiPort,
        method: 'DELETE'
    };
    var request = http.request(options, function (res) {
        if (200 == res.statusCode) {
            console.log("SUCCESS.", id, "deleted.");
            result = true;
        } else {
            console.log("ERROR. Status code =", res.statusCode);
        }
    });
    request.on('error', function (err) {
        console.log("Error:", err.code);
    });
    request.end();
}

var args = process.argv;
if (3 != args.length) {
    console.log("### Usage: node Delete id");
    console.log("### Example: node Delete 101");
} else {
    DeleteData(args[2]);
}
