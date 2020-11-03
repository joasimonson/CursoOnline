export function getFullDate(concat: string = '') : string {
    var d = new Date()
    var h = d.getHours()
    var m = d.getMinutes()
    var s = d.getSeconds()
    var ms = d.getMilliseconds()
    return concat + h + ":" + m + ":" + s + ":" + ms
}