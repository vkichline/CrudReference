var querystring = require('querystring'),
    http = require('http');

var apiHost = 'localhost',
    apiPath = '/api/items?',
    apiPort = 47514;

function PostData(data) {
    data = '{"Content":"' + data + '"}';
    var post_options = {
        host: apiHost,
        path: apiPath,
        port: apiPort,
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Content-Length': data.length
        }
    };
    var post_req = http.request(post_options, function (res) {
        res.setEncoding('utf8');
        res.on('data', function (chunk) {
            console.log('Response: ' + chunk);
        });
    });
    post_req.write(data);
    post_req.end();
}

PostData("Testing 1, 2, 3, 4.");

