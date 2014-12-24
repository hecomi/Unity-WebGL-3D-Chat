var express  = require('express');
var app      = express();
var http     = require('http').Server(app);
var io       = require('socket.io')(http);

app.use(express.static(__dirname + '/webgl-chat'));

io.on('connection', function(socket) {
	var id = socket.id;
	console.log(id);

	socket.on('move', function(x, y, z) {
		socket.broadcast.emit('move', id, x, y, z);
	});

	socket.on('rotate', function(x, y, z, w) {
		socket.broadcast.emit('rotate', id, x, y, z, w);
	});

	socket.on('talk', function(message) {
		socket.broadcast.emit('talk', id, message);
	});

	socket.on('disconnect', function(message) {
		console.log(id);
		socket.broadcast.emit('destroy', id);
	});
});

http.listen(3000, function(){
	console.log('listening on *:3000');
});
