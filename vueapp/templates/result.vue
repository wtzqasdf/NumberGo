<template>
    <div id="result" class="base-layer" v-show="isShowResultForm">
        <div class="result-board" :class="[animaStyleClass]">
            <h1 class="font-weight-bold" :class="[titleProperties.styleClass]">{{ titleProperties.text }}</h1>
            <div>Total elapsed time is</div>
            <b>{{ elapsedTimeText }}</b>
            <!-- 按鈕樣式待更改 2021-07-14 -->
            <div v-if="isPassOrFail" class="mt-2">
                <button class="btn btn-danger" @click="onRestart()">Restart</button>
                <button class="btn btn-dark" @click="onToMenu()">Menu</button>
                <button class="btn btn-primary" @click="onShareLink()">Share</button>
            </div>
            <div v-if="!isPassOrFail" class="mt-2">
                <button class="btn btn-danger" @click="onRestart()">Restart</button>
                <button class="btn btn-dark" @click="onToMenu()">Menu</button>
            </div>
            <div class="text-center share-link mt-2">{{ shareLink }}</div>
        </div>
    </div>
</template>

<script>
export default {
    name: 'result',
    data() {
        return {
            isShowResultForm: false,
            isPassOrFail: true,
            animaStyleClass: '',
            titleProperties: {
                styleClass: '',
                text: '',
            },
            elapsedTimeText: '',
            shareLink: '',
        };
    },
    methods: {
        setElapsedTimeText(text) {
            this.elapsedTimeText = text;
        },
        setShareLink(link) {
            this.shareLink = link;
        },
        hasShareLink() {
            return this.shareLink !== '';
        },
        showPassForm() {
            this.titleProperties.styleClass = 'text-success';
            this.titleProperties.text = 'PASS';
            this.isPassOrFail = true;
            this.isShowResultForm = true;
            this.animaStyleClass = 'scale-anima';
        },
        showFailForm() {
            this.titleProperties.styleClass = 'text-danger';
            this.titleProperties.text = 'FAIL';
            this.isPassOrFail = false;
            this.isShowResultForm = true;
            this.animaStyleClass = 'scale-anima';
        },
        closeForm() {
            this.animaStyleClass = '';
            this.elapsedTimeText = '';
            this.shareLink = '';
            this.isShowResultForm = false;
        },
        //events
        onRestart() {},
        onToMenu() {},
        onShareLink() {},
    },
};
</script>

<style scoped>
h1 {
    font-size: 36px;
    font-weight: bold;
}
b {
    font-size: 28px;
}
button {
    margin: 0px 5px;
}
.base-layer {
    position: fixed;
    background-color: rgba(0, 0, 0, 0.5);
    width: 100%;
    height: 100%;
    left: 0px;
    top: 0px;
    z-index: 99999;
    display: flex;
    justify-content: center;
    align-items: center;
}
.share-link {
    padding: 0px 10px;
    word-break: break-all;
    font-weight: bold;
    font-size: 14px;
}
</style>