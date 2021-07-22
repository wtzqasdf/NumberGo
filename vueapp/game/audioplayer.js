let player = null;

export default {
    init(url) {
        if (player !== null) {
            player = null;
        }
        if (url !== '') {
            player = new Audio(url);
            player.loop = false;
            player.preload = true;
        }
    },
    play() {
        if (player !== null) {
            player.play();
        }
    }
}