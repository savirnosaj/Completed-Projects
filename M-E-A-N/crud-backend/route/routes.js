// Adding express in Route.js
var express = require('express');
// Router for adding all routes; creates constructor
var router = express.Router();

// Importing DB schema
const Item = require('../model/shoppingItem');

// CRUD operations

// Getting - Retreieving data from database
router.get('/items', (req, res, next)=>{
    // Mongo Lirbary - find quieres DB
    Item.find(function(err, items){
        if(err){
            res.json(err);
        }
        else {
            res.json(items);
        }
    });
});

// Posting - creating new data
router.post('/item', (req, res, next) => {
    let newShoppingItem = new Item ({
        itemName: req.body.itemName,
        itemQuantity: req.body.itemQuantity,
        itemBought: req.body.itemBought
    });
    newShoppingItem.save((err, item)=> {
        if(err){
            res.json(err);
        }
        else{
            res.json({msg: 'Item has been successfully saved!'});
        }
    });
});

// Updating - updating saved data
router.put('/item/:id', (req, res, next) => {
    Item.findOneAndUpdate({_id: req.params.id}, {
        // Set functino will set old content with new
        $set: {
            itemName: req.body.itemName,
            itemQuantity: req.body.itemQuantity,
            itemBought: req.body.itemBought
        }
    },
    // Call-Back
    function(err, result){
        if(err){
            res.json(err);
        }
        else {
            res.json(result);
        }
    });
});

// Deleting - deleteing  saved data
router.delete('/item/:id', (req, res, next) => {
    Item.remove({_id: req.params.id}, function(err, result) {
        if(err){
            res.json(err);
        }
        else {
            res.json(result);
        }
    });
});

// Explorting
module.exports = router;