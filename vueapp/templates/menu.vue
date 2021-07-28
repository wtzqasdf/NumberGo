<template>
    <div id="menu">
        <div class="pos-fixed pt-3" v-show="isShowMenu">
            <div class="container-fluid">
                <!-- User account panel logic -->
                <div class="row justify-content-center">
                    <div class="col-xl-6 col-lg-8 col-md-12 col-sm-12 col-12">
                        <div class="control-block d-flex justify-content-between">
                            <div v-if="isLogin" class="d-flex flex-column justify-content-center">
                                <!-- Profile -->
                                <b>{{ userProfile.account }}</b>
                            </div>
                            <div v-if="isLogin">
                                <!-- Action -->
                                <button v-if="userProfile.canUpgradeAccount" class="btn btn-primary btn-sm" @click="showUpgradeAccountForm()" title="Upgrade your account">Upgrade</button>
                                <button class="btn btn-danger btn-sm" @click="showChangePWForm()" title="Change password">ChangePW</button>
                                <button class="btn btn-dark btn-sm" @click="onLogout()" title="Login your account">Logout</button>
                            </div>
                            <div v-if="!isLogin" class="d-flex flex-column justify-content-center">
                                <b>Guest</b>
                            </div>
                            <div v-if="!isLogin">
                                <!-- Action -->
                                <button class="btn btn-dark btn-sm" @click="showLoginForm()" title="Login your account">Login</button>
                                <button class="btn btn-primary btn-sm" @click="showRegisterForm()" title="Register a new account">Register</button>
                                <button class="btn btn-danger btn-sm" @click="showForgotPWForm()" title="Find your password">Forgot</button>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Shop & Play logic -->
                <div class="row justify-content-center">
                    <div class="col-xl-3 col-lg-4 col-md-12 col-sm-12 col-12 mt-2 mb-xl-2 mb-lg-2 mb-1">
                        <!-- Shop -->
                        <div class="control-block d-flex" style="overflow-x:auto;">
                            <div class="shop-item" v-for="(data,index) in shopItems" :key="index">
                                <h6>{{ data.title }}</h6>
                                <img :src="data.imgsrc" />
                                <div class="desc">
                                    <small title="Click after play sound effect">Sound&nbsp;Effect</small>
                                    <small title="Special Animation">SP Anima</small>
                                </div>
                                <div>
                                    <button v-if="!data.canSelect" class="choose-button button-disabled">Select</button>
                                    <button v-if="data.canSelect" class="choose-button button-enabled" :class="{ 'bg-green': data.isSelected }" @click="selectSkinChanged(data.skinName)">Select</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-xl-3 col-lg-4 col-md-12 col-sm-12 col-12 mb-2 mt-xl-2 mt-lg-2 mt-1">
                        <!-- Play -->
                        <div class="control-block d-flex flex-column justify-content-between">
                            <div>
                                <input class="form-control" type="text" placeholder="NickName" v-model="userProfile.nickName" />
                            </div>
                            <div class="mt-2">
                                <select v-model="gameLevel" style="width:100%;">
                                    <option v-for="(data, index) in levelCount" :key="index" :value="data">{{ 'Level ' + data }}</option>
                                </select>
                            </div>
                            <div class="d-flex justify-content-end mt-2">
                                <button class="setting" title="Game setting" @click="showSettingForm()">
                                    <img src="/img/gear-fill.svg" />
                                </button>
                            </div>
                            <div class="d-grid mt-2">
                                <button class="btn btn-success text-large" @click="onPlay()">Play</button>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Copyright & Contact -->
                <div class="row justify-content-center">
                    <div class="col-xl-6 col-lg-8 col-md-12 col-sm-12 col-12">
                        <div class="control-block d-flex flex-column align-items-center">
                            <b>©&nbsp;NumberGo</b>
                            <div>
                                <a href="mailto:service@numbergo.me" title="send mail">
                                    <img src="/img/envelope-fill.svg" class="icon" alt="mail" />
                                </a>
                                <a href="https://twitter.com/NumberGoGame" target="_blank" title="follow numbergo twitter">
                                    <img src="/img/twitter.svg" class="icon" alt="twitter" />
                                </a>
                                <a href="https://www.facebook.com/numbergogame" target="_blank" title="follow numbergo facebook">
                                    <img src="/img/facebook.svg" class="icon" alt="facebook" />
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- 登入視窗 -->
        <topform :isshow="isShowLoginForm">
            <div class="control-block">
                <div class="text-end">
                    <button class="btn-red" @click="closeLoginForm()">X</button>
                </div>
                <div>
                    <div>
                        <b>Account</b>
                        <input class="form-control" type="text" placeholder="6 ~ 30 characters" v-model="loginInputs.account" />
                        <small class="text-danger">{{ loginErrorMessages.account }}</small>
                    </div>
                    <div class="mt-2">
                        <b>Password</b>
                        <input class="form-control" type="password" placeholder="6 ~ 30 characters" v-model="loginInputs.password" @keypress.enter="onLogin()" />
                        <small class="text-danger">{{ loginErrorMessages.password }}</small>
                    </div>
                    <div class="mt-2 d-grid">
                        <button class="btn btn-success" @click="onLogin()">Login</button>
                    </div>
                </div>
            </div>
        </topform>
        <!-- 註冊視窗 -->
        <topform :isshow="isShowRegisterForm">
            <div class="control-block">
                <div class="text-end">
                    <button class="btn-red" @click="closeRegisterForm()">X</button>
                </div>
                <div>
                    <div>
                        <b>Account</b>
                        <input class="form-control" type="text" placeholder="6 ~ 30 characters" v-model="registerInputs.account" />
                        <small class="text-danger">{{ registerErrorMessages.account }}</small>
                    </div>
                    <div class="mt-2">
                        <b>Password</b>
                        <input class="form-control" type="password" placeholder="6 ~ 30 characters" v-model="registerInputs.password" />
                        <small class="text-danger">{{ registerErrorMessages.password }}</small>
                    </div>
                    <div class="mt-2">
                        <b>Confirm Password</b>
                        <input class="form-control" type="password" placeholder="6 ~ 30 characters" v-model="registerInputs.confirmPassword" />
                        <small class="text-danger">{{ registerErrorMessages.confirmPassword }}</small>
                    </div>
                    <div class="mt-2">
                        <b>EMail</b>
                        <input class="form-control" type="text" v-model="registerInputs.email" @keypress.enter="onRegister()" />
                        <small class="text-danger">{{ registerErrorMessages.email }}</small>
                    </div>
                    <div class="mt-2 d-grid">
                        <button class="btn btn-success" @click="onRegister()">Register</button>
                    </div>
                </div>
            </div>
        </topform>
        <!-- 忘記密碼視窗 -->
        <topform :isshow="isShowForgotPWForm">
            <div class="control-block">
                <div class="text-end">
                    <button class="btn-red" @click="closeForgotPWForm()">X</button>
                </div>
                <div>
                    <div>
                        <b>Account</b>
                        <input class="form-control" type="text" placeholder="6 ~ 20 character length" v-model="forgotPWInputs.account" />
                        <small class="text-danger">{{ forgotPWErrorMessages.account }}</small>
                    </div>
                    <div class="mt-2">
                        <b>EMail</b>
                        <input class="form-control" type="text" v-model="forgotPWInputs.email" @keypress.enter="onForgotPW()" />
                        <small class="text-danger">{{ forgotPWErrorMessages.email }}</small>
                    </div>
                    <div class="mt-2 d-grid">
                        <button class="btn btn-success" @click="onForgotPW()">Submit</button>
                    </div>
                </div>
            </div>
        </topform>
        <!-- 升級帳號視窗 -->
        <topform :isshow="isShowUpgradeAccountForm">
            <div class="control-block">
                <div class="text-end">
                    <button class="btn-red" @click="closeUpgradeAccountForm()">X</button>
                </div>
                <div>
                    <div class="text-center">
                        <h4>Upgrade your account</h4>
                        <small class="text-danger">(60NT&nbsp;/&nbsp;Forever)</small>
                    </div>
                    <div>
                        <b>Name</b>
                        <input class="form-control" type="text" placeholder="1 ~ 20 characters" v-model="upgradeAccountInputs.name" />
                        <small class="text-danger">{{ upgradeAccountErrorMessages.name }}</small>
                    </div>
                    <div class="mt-2">
                        <b>Tel</b>
                        <input class="form-control" type="text" placeholder="7 ~ 18 characters" v-model="upgradeAccountInputs.tel" />
                        <small class="text-danger">{{ upgradeAccountErrorMessages.tel }}</small>
                    </div>
                    <div class="mt-2 d-grid">
                        <button class="btn btn-success" @click="onUpgradeAccount()">Pay</button>
                    </div>
                </div>
            </div>
        </topform>
        <!-- 變更密碼視窗 -->
        <topform :isshow="isShowChangePWForm">
            <div class="control-block">
                <div class="text-end">
                    <button class="btn-red" @click="closeChangePWForm()">X</button>
                </div>
                <div>
                    <div>
                        <b>Old&nbsp;Password</b>
                        <input class="form-control" type="password" placeholder="6 ~ 20 character length" v-model="changePWInputs.oldPassword" />
                        <small class="text-danger">{{ changePWErrorMessages.oldPassword }}</small>
                    </div>
                    <div class="mt-2">
                        <b>New&nbsp;Password</b>
                        <input class="form-control" type="password" placeholder="6 ~ 20 character length" v-model="changePWInputs.newPassword" @keypress.enter="onChangePW()" />
                        <small class="text-danger">{{ changePWErrorMessages.newPassword }}</small>
                    </div>
                    <div class="mt-2 d-grid">
                        <button class="btn btn-success" @click="onChangePW()">Submit</button>
                    </div>
                </div>
            </div>
        </topform>
        <!-- 遊戲設定視窗 -->
        <topform :isshow="isShowSettingForm">
            <div class="control-block">
                <div class="text-end">
                    <button class="btn-red" @click="closeSettingForm()">X</button>
                </div>
                <div>
                    <div class="form-check form-switch">
                        <input class="form-check-input" type="checkbox" id="soundSwitchCheck" v-model="settingInputs.soundEffectEnabled" @input="soundSettingChanged($event.target)" />
                        <label class="form-check-label" for="soundSwitchCheck">open or close sound effect.</label>
                    </div>
                </div>
            </div>
        </topform>
    </div>
</template>

<script>
import topform from '../components/topform.vue';
import GameSetting from '../game/gamesetting.js';
export default {
    name: 'menu',
    data() {
        return {
            userProfile: {
                account: '',
                nickName: '',
                canUpgradeAccount: true,
            },
            loginInputs: {
                account: '',
                password: '',
            },
            loginErrorMessages: {
                account: '',
                password: '',
            },
            registerInputs: {
                account: '',
                password: '',
                confirmPassword: '',
                email: '',
            },
            registerErrorMessages: {
                account: '',
                password: '',
                confirmPassword: '',
                email: '',
            },
            upgradeAccountInputs: {
                name: '',
                tel: '',
            },
            upgradeAccountErrorMessages: {
                name: '',
                tel: '',
            },
            changePWInputs: {
                oldPassword: '',
                newPassword: '',
            },
            changePWErrorMessages: {
                oldPassword: '',
                newPassword: '',
            },
            forgotPWInputs: {
                account: '',
                email: '',
            },
            forgotPWErrorMessages: {
                account: '',
                email: '',
            },
            settingInputs: {
                soundEffectEnabled: false,
            },
            shopItems: [
                { title: 'Default', imgsrc: '/img/logo.jpg', canSelect: true, isSelected: true, skinName: 'scale' },
                { title: 'Clock', imgsrc: '/img/clocklogo.jpg', canSelect: false, isSelected: false, skinName: 'clock' },
                { title: 'Ghost', imgsrc: '/img/ghostlogo.jpg', canSelect: false, isSelected: false, skinName: 'ghost' },
                { title: 'Gear', imgsrc: '/img/gearlogo.jpg', canSelect: false, isSelected: false, skinName: 'gear' },
            ],
            levelCount: 40,
            gameLevel: '1',
            selectSkinName: 'scale',
            isLogin: false,
            isShowMenu: true,
            isShowLoginForm: false,
            isShowRegisterForm: false,
            isShowForgotPWForm: false,
            isShowUpgradeAccountForm: false,
            isShowChangePWForm: false,
            isShowSettingForm: false,
        };
    },
    methods: {
        //events
        onLogin() {},
        onLogout() {},
        onRegister() {},
        onForgotPW() {},
        onChangePW() {},
        onPlay() {},
        onUpgradeAccount() {},
        //receive events
        selectSkinChanged(skinName) {
            this.selectSkinName = skinName;
            this.refreshSkinSelectButton(skinName);
        },
        soundSettingChanged(target) {
            GameSetting.setSoundEffect(target.checked);
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
        setCanUpgradeAccount(canUpgrade) {
            this.userProfile.canUpgradeAccount = canUpgrade;
        },
        setLoginStatus(isLogin) {
            this.isLogin = isLogin;
        },
        setUserAccount(account) {
            this.userProfile.account = account;
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
        showLoginForm() {
            this.isShowLoginForm = true;
        },
        showRegisterForm() {
            this.isShowRegisterForm = true;
        },
        showForgotPWForm() {
            this.isShowForgotPWForm = true;
        },
        showUpgradeAccountForm() {
            this.isShowUpgradeAccountForm = true;
        },
        showChangePWForm() {
            this.isShowChangePWForm = true;
        },
        showSettingForm() {
            this.settingInputs.soundEffectEnabled = GameSetting.getSoundEffect();
            this.isShowSettingForm = true;
        },
        //close methods
        closeMenu() {
            this.isShowMenu = false;
        },
        closeLoginForm() {
            this.isShowLoginForm = false;
            this.clearLoginErrorMessages();
            this.clearLoginInputs();
        },
        closeRegisterForm() {
            this.isShowRegisterForm = false;
            this.clearRegisterErrorMessages();
            this.clearRegisterInputs();
        },
        closeForgotPWForm() {
            this.isShowForgotPWForm = false;
            this.clearForgotPWErrorMessages();
            this.clearForgotPWInputs();
        },
        closeUpgradeAccountForm() {
            this.isShowUpgradeAccountForm = false;
            this.clearUpgradeAccountErrorMessages();
            this.clearUpgradeAccountInputs();
        },
        closeChangePWForm() {
            this.isShowChangePWForm = false;
            this.clearChangePWErrorMessages();
            this.clearChangePWInputs();
        },
        closeSettingForm() {
            this.isShowSettingForm = false;
        },
        //clear methods
        clearRegisterInputs() {
            this.registerInputs.account = '';
            this.registerInputs.password = '';
            this.registerInputs.confirmPassword = '';
            this.registerInputs.email = '';
        },
        clearRegisterErrorMessages() {
            this.registerErrorMessages.account = '';
            this.registerErrorMessages.password = '';
            this.registerErrorMessages.confirmPassword = '';
            this.registerErrorMessages.email = '';
        },
        clearLoginInputs() {
            this.loginInputs.account = '';
            this.loginInputs.password = '';
        },
        clearLoginErrorMessages() {
            this.loginErrorMessages.account = '';
            this.loginErrorMessages.password = '';
        },
        clearUpgradeAccountInputs() {
            this.upgradeAccountInputs.name = '';
            this.upgradeAccountInputs.tel = '';
        },
        clearUpgradeAccountErrorMessages() {
            this.upgradeAccountErrorMessages.name = '';
            this.upgradeAccountErrorMessages.tel = '';
        },
        clearChangePWInputs() {
            this.changePWInputs.oldPassword = '';
            this.changePWInputs.newPassword = '';
        },
        clearChangePWErrorMessages() {
            this.changePWErrorMessages.oldPassword = '';
            this.changePWErrorMessages.newPassword = '';
        },
        clearForgotPWInputs() {
            this.forgotPWInputs.account = '';
            this.forgotPWInputs.email = '';
        },
        clearForgotPWErrorMessages() {
            this.forgotPWErrorMessages.account = '';
            this.forgotPWErrorMessages.email = '';
        },
        //set messages methods
        setRegisterErrorMessages(errors) {
            this.registerErrorMessages.account = errors.hasOwnProperty('account') ? errors.account : '';
            this.registerErrorMessages.password = errors.hasOwnProperty('password') ? errors.password : '';
            this.registerErrorMessages.confirmPassword = errors.hasOwnProperty('confirmpassword') ? errors.confirmpassword : '';
            this.registerErrorMessages.email = errors.hasOwnProperty('email') ? errors.email : '';
        },
        setLoginErrorMessages(errors) {
            this.loginErrorMessages.account = errors.hasOwnProperty('account') ? errors.account : '';
            this.loginErrorMessages.password = errors.hasOwnProperty('password') ? errors.password : '';
        },
        setUpgradeAccountErrorMessages(errors) {
            this.upgradeAccountErrorMessages.name = errors.hasOwnProperty('name') ? errors.name : '';
            this.upgradeAccountErrorMessages.tel = errors.hasOwnProperty('tel') ? errors.tel : '';
        },
        setForgotPWErrorMessages(errors) {
            this.forgotPWErrorMessages.account = errors.hasOwnProperty('account') ? errors.account : '';
            this.forgotPWErrorMessages.email = errors.hasOwnProperty('email') ? errors.email : '';
        },
        setChangePWErrorMessages(errors) {
            this.changePWErrorMessages.oldPassword = errors.hasOwnProperty('oldpassword') ? errors.oldpassword : '';
            this.changePWErrorMessages.newPassword = errors.hasOwnProperty('newpassword') ? errors.newpassword : '';
        },
    },
    components: { topform },
};
</script>

<style scoped>
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
.shop-item .button-disabled {
    background-color: gray;
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
