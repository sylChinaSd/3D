/**
 * Created by Administrator on 16-4-22.
 */
window.requestAnimFrame = (function(){
    return window.requestAnimationFrame||
        window.webkitRequestAnimationFrame||
        window.mozRequestAnimationFrame||
        window.oRequestAnimationFrame||
        window.msRequestAnimationFrame||
        function(callback,element){
            return window.setTimeout(callback,1000/60);
        }
})();

window.cancelRequestAnimFrame = (function(){
    return window.cancelRequestAnimationFrame||
        window.webkitCancelRequestAnimationFrame||
        window.mozCancelRequestAnimationFrame||
        window.oCancelRequestAnimationFrame||
        window.msCancelRequestAnimationFrame||
        function(tid,element){
            return window.clearTimeout(tid);
        }
})();