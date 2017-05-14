var io = require('socket.io')(process.env.PORT || 3000);
var shortid=require('shortid')
console.log('server is start');


var players=[]; 
 
io.on('connection',function(socket){
    var thisplayerid=shortid.generate();
   players.push(thisplayerid);
     console.log('Client connected,broadcasting spawn,id:',thisplayerid); 
    
    socket.broadcast.emit('spawn',{id : thisplayerid});
    socket.broadcast.emit('requestpostion');
    
  players.forEach(function(playerid){
      if(playerid==thisplayerid)
          return;
      socket.emit('spawn',{id:playerid});
             console.log('sending spawn to new player for id',playerid); 
  });


socket.on('move',function(data)
{   
    data.id=thisplayerid;
console.log('Client move',JSON.stringify(data)); 
socket.broadcast.emit('move',data);
});
    
 socket.on('updatePostion',function(data){   
    console.log('update postion',data); 
    data.id=thisplayerid;
    socket.broadcast.emit('updatePostion',data);
});
    
    
socket.on('disconnect',function()
{        
console.log('Client disconnect'); 
 players.splice(players.indexOf(thisplayerid),1);
    socket.broadcast.emit('disconnected',{id:thisplayerid});
});
})