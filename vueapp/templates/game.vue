<template>
    <div id="game" v-show="isRunGame">
        <!-- Score -->
        <div class="score-board">
            <div class="score-block">
                <b>Count</b>
                <small>{{ scoreStatistics.count }}</small>
            </div>
            <div class="score-block">
                <b>Total</b>
                <small>{{ scoreStatistics.total }}</small>
            </div>
            <div class="score-block">
                <b>Target</b>
                <small>{{ scoreStatistics.target }}</small>
            </div>
        </div>
        <div class="container-fluid number-block-mt">
            <div class="row justify-content-center">
                <div class="col-12">
                    <!-- numbers -->
                    <button v-for="(data,index) in buttons" :key="index" class="number" :class="[data.class]" :style="data.style" @click="data.onclick($event)">{{ data.text }}</button>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import GameCenter from '../game/gamecenter.js';
import StopWatch from '../game/stopwatch.js';
import AudioPlayer from '../game/audioplayer.js';

export default {
    name: 'game',
    data() {
        return {
            isRunGame: false,
            scoreStatistics: {
                count: ' ',
                total: ' ',
                target: ' ',
            },
            buttonAnimaClass: '',
            buttons: [],
            maxCount: 0,
            soundEffectEnabled: true,
        };
    },
    mounted() {
        GameCenter.init(this.buttons);
        GameCenter.onCreateButton = () => {
            this.buttonCreated();
        };
    },
    methods: {
        startGame(level, btnAnimaClass, audioUrl) {
            this.maxCount = this.levelToCount(level);
            this.buttonAnimaClass = btnAnimaClass;
            AudioPlayer.init(audioUrl);
            this.resetGame(this.maxCount);
        },
        setSoundEffectEnabled(isEnable) {
            this.soundEffectEnabled = isEnable;
        },
        restartGame() {
            this.resetGame(this.maxCount);
        },
        resetGame(max) {
            //maxCount - 1因為要排除"1唯一數"，方法後面+1是將"1唯一數"加回去
            let targetNum = GameCenter.randTargetNumber(this.maxCount - 1) + 1;
            GameCenter.setButtonAnimationClass(this.buttonAnimaClass); //number scale-anima
            GameCenter.setButtonBGColor('black');
            GameCenter.setButtonTextColor('white');
            GameCenter.reset(max, targetNum);

            this.updateStatistics(GameCenter.getCurrentCount(), max, GameCenter.getCurrentNumber(), targetNum);
            this.isRunGame = true;
            //開始遊戲計時
            StopWatch.start();
        },
        stopGame() {
            this.isRunGame = false;
        },
        updateStatistics(count, maxCount, total, target) {
            this.scoreStatistics.count = `${count}/${maxCount}`;
            this.scoreStatistics.total = total;
            this.scoreStatistics.target = target;
        },
        levelToCount(level) {
            switch (level) {
                case 1:
                    return 8;
                case 2:
                    return 10;
                case 3:
                    return 12;
                case 4:
                    return 15;
                case 5:
                    return 18;
                case 6:
                    return 20;
                case 7:
                    return 25;
                case 8:
                    return 30;
                case 9:
                    return 38;
                case 10:
                    return 45;
                case 11:
                    return 50;
                case 12:
                    return 60;
                case 13:
                    return 75;
                case 14:
                    return 92;
                case 15:
                    return 105;
                case 16:
                    return 118;
                case 17:
                    return 130;
                case 18:
                    return 155;
                case 19:
                    return 174;
                case 20:
                    return 200;
                default:
                    //default as case '1'
                    return 8;
            }
        },
        getElapsedTime() {
            return StopWatch.getString();
        },
        //event methods
        /**
         *當遊戲結束時引發
         *@param ispass 是否通關
         *@param elapsedTime 耗時
         */
        onGameOver(ispass) {},
        //Receive event methods
        buttonCreated() {
            //指出是否啟用音效
            if (this.soundEffectEnabled) {
                AudioPlayer.play();
            }
            if (GameCenter.isOverCount()) {
                if (GameCenter.isTargetReached()) {
                    //success
                    StopWatch.stop();
                    this.onGameOver(true);
                } else {
                    //fail
                    StopWatch.stop();
                    this.onGameOver(false);
                }
            } else {
                if (GameCenter.isNumberOverTarget()) {
                    //fail
                    StopWatch.stop();
                    this.onGameOver(false);
                }
            }
            this.updateStatistics(GameCenter.getCurrentCount(), GameCenter.getMaxCount(), GameCenter.getCurrentNumber(), GameCenter.getTargetNumber());
        },
    },
};
</script>

<style>
.clock-anima {
    animation-name: rotate-frames;
    animation-timing-function: step-start;
    animation-iteration-count: infinite;
    animation-duration: 15s;
    border-radius: 50px;
}
.ghost-anima {
    animation-name: fade-frames;
    animation-timing-function: linear;
    animation-iteration-count: infinite;
    animation-duration: 10s;
}
.gear-anima {
    animation-name: rotate-half-frames;
    animation-timing-function: steps(20, jump-none);
    animation-iteration-count: infinite;
    animation-duration: 10s;
}
@keyframes rotate-frames {
    0% {
        transform: rotate(0deg);
    }
    10% {
        transform: rotate(36deg);
    }
    20% {
        transform: rotate(72deg);
    }
    30% {
        transform: rotate(108deg);
    }
    40% {
        transform: rotate(144deg);
    }
    50% {
        transform: rotate(180deg);
    }
    60% {
        transform: rotate(216deg);
    }
    70% {
        transform: rotate(252deg);
    }
    80% {
        transform: rotate(288deg);
    }
    90% {
        transform: rotate(324deg);
    }
    100% {
        transform: rotate(360deg);
    }
}
@keyframes fade-frames {
    0% {
        opacity: 1;
    }
    50% {
        opacity: 0;
    }
    60% {
        opacity: 0;
    }
    100% {
        opacity: 1;
    }
}
@keyframes rotate-half-frames {
    0% {
        transform: rotate(0deg);
    }
    20% {
        transform: rotate(45deg);
    }
    30% {
        transform: rotate(45deg);
    }
    50% {
        transform: rotate(0deg);
    }
    70% {
        transform: rotate(-45deg);
    }
    80% {
        transform: rotate(-45deg);
    }
    100% {
        transform: rotate(0deg);
    }
}
</style>