// Import Modules
var express = require('express');
var mongoose = require('mongoose');
var bodyparser = require('body-parser');
var cors = require('cors');

const route = require('./route/routes');

// Instantiate Express; contructor;
var app = express();

// Connect to Database;
mongoose.connect('mongodb://localhost:27017/shoppinglist');

// on connection
mongoose.connection.on('connected', () => {
    console.log('MongoDB connected to port 27017');
});

// on connection Error
mongoose.connection.on('error', (err) => {
    console.log(err);
});

// Specify which port number our server will be running on;
const PORT = 3000;

// Adding Middleware - cors (allows responses to be exchanged between two different domains (localhost:3000, localhost:4200))
app.use(cors());
// Adding Middleware - body-parser (used around json data)
app.use(bodyparser.json());

app.use('/api', route);

// Specifing port number
app.listen(PORT, () => {
    console.log('Server has been started at port: ' + PORT);
    console.log('http://localhost:' + PORT);
});

// Check if server is running;
app.get('/', (req, res) => {
    res.send('Hello World!');
});
