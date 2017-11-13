/**
 * Created by Administrator on 16-4-21.
 */
//抛出自定义的webgl异常
function throwOnGlError(err,funcName,args){
    throw WebGLDebugUtils.glEnumToString(err)+' was caused by call to '+funcName;
}
//记录调用的函数并检测参数是否有效
function logAndValidate(funcName,args){
    console.log('gl.'+funcName+'('+WebGLDebugUtils.glFunctionArgsToString(funcName,args)+')');
    var valid = true;
    for(var i = 0; i < args.length;++i){
        if(args[i] == undefined){
            valid = false;
            break;
        }
    }
    if(!valid){
        console.error('undefined passed to gl.'+funcName+'('+WebGLDebugUtils.glFunctionArgsToString(funcName,args)+')');
    }
}