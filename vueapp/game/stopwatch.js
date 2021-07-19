let firstTick = null;
let secondTick = null;

export default {
    start() {
        firstTick = performance.now();
    },
    stop() {
        secondTick = performance.now();
    },
    getString() {
        let ms = parseInt(secondTick - firstTick);
        let time = new Date(ms).toISOString().slice(11, -1);
        return time;
    },
}