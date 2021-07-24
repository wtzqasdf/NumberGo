let player = null;

export default {
    init(url) {
        if (player == null) {
            player = new Audio();
        }
        if (url !== '') {
            player.src = url;
            player.loop = false;
            player.preload = true;
        }
    },
    play() {
        if (player !== null) {
            player.currentTime = 0;
            player.play();
        }
    }
}