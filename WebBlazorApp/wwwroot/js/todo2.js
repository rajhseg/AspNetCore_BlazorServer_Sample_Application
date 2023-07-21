
function callCsharpInstanceMethod(instance, obj) {    
    console.log(obj);
    instance.invokeMethodAsync("GetMessageFromInstanceMethod").then((result) => {
        console.log(result);
        //alert(result);
    });
}

export { callCsharpInstanceMethod };