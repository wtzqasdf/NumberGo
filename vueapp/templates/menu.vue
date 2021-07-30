<template>
    <div id="menu">
        <div class="pos-fixed pt-3" v-show="isShowMenu">
            <div class="container-fluid">
                <!-- Shop & Play logic -->
                <div class="row justify-content-center">
                    <div class="col-xl-3 col-lg-4 col-md-12 col-sm-12 col-12 mt-2 mb-xl-2 mb-lg-2 mb-1">
                        <!-- Shop -->
                        <div class="control-block d-flex" style="overflow-x:auto;">
                            <div class="shop-item" v-for="(data,index) in shopItems" :key="index">
                                <h6>{{ langLoader.getText('skinname:' + data.skinName) }}</h6>
                                <img :src="data.imgsrc" />
                                <div class="desc">
                                    <small title="Click after play sound effect">{{ langLoader.getText('skinhelp:soundeffect') }}</small>
                                    <small title="Special Animation">{{ langLoader.getText('skinhelp:spanima') }}</small>
                                </div>
                                <div>
                                    <button class="choose-button button-enabled" :class="{ 'bg-green': data.isSelected }" @click="selectSkinChanged(data.skinName)">{{ langLoader.getText('skinbtn:select') }}</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-xl-3 col-lg-4 col-md-12 col-sm-12 col-12 mb-2 mt-xl-2 mt-lg-2 mt-1">
                        <!-- Play -->
                        <div class="control-block d-flex flex-column justify-content-between">
                            <div>
                                <input class="form-control" type="text" :placeholder="langLoader.getText('play:ph:nickname')" v-model="userProfile.nickName" />
                            </div>
                            <div class="mt-2">
                                <select v-model="gameLevel" style="width:100%;">
                                    <option v-for="(data, index) in levelCount" :key="index" :value="data">{{ langLoader.getText('play:level') + data }}</option>
                                </select>
                            </div>
                            <div class="d-flex justify-content-end mt-2">
                                <button class="setting" :title="langLoader.getText('play:title:gamesetting')" @click="showSettingForm()">
                                    <img src="/img/gear-fill.svg" />
                                </button>
                            </div>
                            <div class="d-grid mt-2">
                                <button class="btn btn-success text-large" @click="onPlay()">{{ langLoader.getText('play:btn:play') }}</button>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Copyright & Contact -->
                <div class="row justify-content-center mt-1">
                    <div class="col-xl-3 col-lg-4 col-md-6 col-sm-6 col-6">
                        <div class="control-block d-flex flex-column align-items-center">
                            <b>©&nbsp;NumberGo</b>
                            <div>
                                <a href="mailto:service@numbergo.me" :title="langLoader.getText('contact:mail:title')">
                                    <img src="/img/envelope-fill.svg" class="icon" alt="mail" />
                                </a>
                                <a href="https://twitter.com/NumberGoGame" target="_blank" :title="langLoader.getText('contact:twitter:title')">
                                    <img src="/img/twitter.svg" class="icon" alt="twitter" />
                                </a>
                                <a href="https://www.facebook.com/numbergogame" target="_blank" :title="langLoader.getText('contact:facebook:title')">
                                    <img src="/img/facebook.svg" class="icon" alt="facebook" />
                                </a>
                            </div>
                        </div>
                    </div>
                    <!-- Buy me a coffee Donate -->
                    <div class="col-xl-3 col-lg-4 col-md-6 col-sm-6 col-6">
                        <div class="control-block d-flex flex-column justify-content-center align-items-center">
                            <a href="https://www.buymeacoffee.com/numbergo" target="_blank">
                                <button class="btn-donate">{{ langLoader.getText('donate:btn:title') }}</button>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- 遊戲設定視窗 -->
        <topform :isshow="isShowSettingForm">
            <div class="control-block">
                <div class="text-end">
                    <button class="btn-red" @click="closeSettingForm()">X</button>
                </div>
                <div>
                    <h5>{{ langLoader.getText('setting:sound:title') }}</h5>
                    <div class="form-check form-switch">
                        <input class="form-check-input" type="checkbox" id="soundSwitchCheck" v-model="settingInputs.soundEffectEnabled" @input="soundSettingChanged($event.target)" />
                        <label class="form-check-label" for="soundSwitchCheck">{{ langLoader.getText('setting:sound:help') }}</label>
                    </div>
                    <h5>{{ langLoader.getText('setting:lang:title') }}</h5>
                    <div class="form-check">
                        <input class="form-check-input" type="radio" id="langENRadio" name="langRadio" value="en" @input="languageChanged($event.target)" />
                        <label class="form-check-label" for="langENRadio">English</label>
                    </div>
                    <div class="form-check">
                        <input class="form-check-input" type="radio" id="langTCRadio" name="langRadio" value="tc" @input="languageChanged($event.target)" />
                        <label class="form-check-label" for="langTCRadio">繁體中文</label>
                    </div>
                </div>
            </div>
        </topform>
    </div>
</template>

<script>
import LangLoader from '../languages/langloader.js';
import topform from '../components/topform.vue';
import GameSetting from '../game/gamesetting.js';

export default {
    name: 'menu',
    data() {
        return {
            langLoader: LangLoader,
            userProfile: {
                nickName: '',
            },
            settingInputs: {
                soundEffectEnabled: false,
            },
            shopItems: [
                { imgsrc: '/img/logo.jpg', canSelect: true, isSelected: true, skinName: 'scale' },
                { imgsrc: '/img/clocklogo.jpg', canSelect: false, isSelected: false, skinName: 'clock' },
                { imgsrc: '/img/ghostlogo.jpg', canSelect: false, isSelected: false, skinName: 'ghost' },
                { imgsrc: '/img/gearlogo.jpg', canSelect: false, isSelected: false, skinName: 'gear' },
            ],
            levelCount: 40,
            gameLevel: '1',
            selectSkinName: 'scale',
            isShowMenu: true,
            isShowSettingForm: false,
        };
    },
    methods: {
        //events
        onPlay() {},
        onLanguageChange() {},
        //receive events
        selectSkinChanged(skinName) {
            this.selectSkinName = skinName;
            this.refreshSkinSelectButton(skinName);
        },
        soundSettingChanged(target) {
            GameSetting.setSoundEffect(target.checked);
        },
        languageChanged(target) {
            GameSetting.setLanguageCode(target.value);
            this.onLanguageChange();
        },
        //刷新皮膚選擇按鈕
        refreshSkinSelectButton(skinName) {
            for (let i = 0; i < this.shopItems.length; i++) {
                if (this.shopItems[i].skinName === skinName) {
                    this.shopItems[i].isSelected = true;
                } else {
                    this.shopItems[i].isSelected = false;
                }
            }
        },
        refreshUILanguage() {
            this.$forceUpdate();
        },
        //get, set methods
        getNickName() {
            return this.userProfile.nickName;
        },
        getGameLevel() {
            return parseInt(this.gameLevel);
        },
        getSelectSkinName() {
            return this.selectSkinName;
        },
        setNickName(name) {
            this.userProfile.nickName = name;
        },
        //設定所有皮膚按鈕是否啟用，除了Default之外
        setSkinButtonIsEnable(isEnable) {
            for (let i = 1; i < this.shopItems.length; i++) {
                this.shopItems[i].canSelect = isEnable;
                this.shopItems[i].isSelected = false;
            }
            this.shopItems[0].isSelected = true;
            this.selectSkinName = 'scale';
        },
        //show methods
        showMenu() {
            this.isShowMenu = true;
        },
        showSettingForm() {
            this.settingInputs.soundEffectEnabled = GameSetting.getSoundEffect();
            //language setting load
            let langCode = GameSetting.getLanguageCode().toUpperCase();
            document.getElementById(`lang${langCode}Radio`).checked = true;
            this.isShowSettingForm = true;
        },
        //close methods
        closeMenu() {
            this.isShowMenu = false;
        },
        closeSettingForm() {
            this.isShowSettingForm = false;
        },
    },
    components: { topform },
};
</script>

<style scoped>
h5 {
    font-size: 18px;
    font-weight: bold;
    color: darkred;
}
select {
    border: 1px solid rgba(0, 0, 0, 0.5);
    border-radius: 5px;
    padding: 5px;
}
.icon {
    width: 24px;
    height: 24px;
    margin: 0px 5px;
}

.pos-fixed {
    background-color: rgba(0, 0, 0, 0.5);
    position: fixed;
    width: 100%;
    height: 100%;
    top: 0px;
    left: 0px;
    z-index: 99;
}
.control-block {
    background-color: white;
    border-radius: 7px;
    padding: 0.5rem;
    height: 100%;
}
.shop-item {
    height: 100%;
    background-color: rgba(0, 0, 0, 0.2);
    border-radius: 5px;
    width: 100px;
    padding: 5px;
    margin: 0px 5px;
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    align-items: center;
    text-align: center;
}
.shop-item h6 {
    font-weight: bold;
    color: darkred;
    margin: 0.2rem 0;
}
.shop-item img {
    width: 32px;
    height: 32px;
}
.shop-item .desc {
    line-height: 15px;
    display: flex;
    flex-direction: column;
}
.shop-item small {
    font-size: 12px;
    cursor: default;
}
.shop-item del {
    font-size: 12px;
    cursor: default;
}
.shop-item span {
    color: blue;
    font-size: 14px;
    font-weight: bold;
}
.shop-item .choose-button {
    font-weight: bold;
    font-size: 16px;
    border: none;
    border-radius: 5px;
}
.shop-item .button-enabled {
    background-color: lightsalmon;
}
.shop-item .button-enabled:hover {
    background-color: salmon;
}

.setting {
    border: none;
    background-color: transparent;
    width: 24px;
    height: 24px;
    padding: 0;
}
.setting img {
    width: 100%;
    height: 100%;
}

.btn-red {
    border: none;
    background-color: transparent;
    font-weight: bold;
}
.btn-red:hover {
    background-color: rgba(255, 0, 0, 0.7);
}
.bg-green {
    background-color: green !important;
}

.btn-donate{
    width:100%;
    padding: 5px 10px;
    border: none;
    border-radius: 10px;
    background-color: #ffd260;
    color: black;
    font-size: 13px;
    font-weight: bold;
}

.text-normal {
    font-size: 20px;
}
.text-large {
    font-size: 24px;
}
.text-darkblue {
    color: rgb(0, 2, 124);
}
</style>
