export default {
    keyDown(event, key, callback) {
        if (event.key.toLowerCase() == key.toLowerCase()) {
            callback();
        }
    }
}