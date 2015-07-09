// Test for CrudReference project, DELETE command (remove existing record)
// Usage:
//  node Delete 101

var http = require('http'),
    api = require('./settings.json');

function DeleteData(id) {
    var options = {
        host: api.host,
        path: api.path + '/' +id,
        port: api.port,
        method: 'DELETE'
    };
    console.log(options.path);
    var request = http.request(options, function(response) {
        if (200 == response.statusCode) {
            console.log("SUCCESS.", id, "deleted.");
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
if (3 != args.length) {
    console.log("### Usage: node Delete id");
    console.log("### Example: node Delete 101");
} else {
    DeleteData(args[2]);
}
