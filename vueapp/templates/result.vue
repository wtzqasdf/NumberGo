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
            <div v-if="hasShareLink()" class="d-flex justify-content-center mt-2">
                <input class="input-share-link" type="text" v-model="shareLink" readonly />
                <button class="btn-copy" data-clipboard-target="#sharelink" @click="copyLink()">Copy</button>
            </div>
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
        copyLink() {
            let input = document.getElementsByTagName('input')[1];
            input.select();
            let result = document.execCommand('copy');
            if (result) {
                toastr.success('Copy link success.')
            } else {
                toastr.error('Copy link fail.')
            }
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
.input-share-link {
    border: 0px solid darkgray;
    border-radius: 5px;
    padding: 5px;
    font-weight: bold;
    font-size: 14px;
    width: 90%;
}
.btn-copy {
    border: none;
    border-radius: 5px;
    background-color: lightblue;
    padding: 5px;
    font-weight: bold;
}
.btn-copy:hover {
    background-color: rgb(143, 143, 255);
}
</style>