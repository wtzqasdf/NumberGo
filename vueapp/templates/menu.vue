<template>
    <div id="menu">
        <div class="pos-fixed pt-3" v-show="isShowMenu">
            <div class="container-fluid">
                <!-- User account panel logic -->
                <div class="row justify-content-center">
                    <div class="col-xl-6 col-lg-8 col-md-12 col-sm-12 col-12">
                        <div class="control-block d-flex justify-content-between">
                            <div v-if="isLogin" class="d-flex flex-column">
                                <!-- Profile -->
                                <b>{{ userProfile.account }}</b>
                            </div>
                            <div v-if="isLogin">
                                <!-- Action -->
                                <button v-if="userProfile.canUpgradeAccount" class="btn btn-primary" @click="showUpgradeAccountForm()" title="Upgrade your account">Upgrade</button>
                                <button class="btn btn-dark" @click="onLogout()" title="Login your account">Logout</button>
                            </div>
                            <div v-if="!isLogin" class="d-flex flex-column">
                                <b>Guest</b>
                            </div>
                            <div v-if="!isLogin">
                                <!-- Action -->
                                <button class="btn btn-dark" @click="showLoginForm()" title="Login your account">Login</button>
                                <button class="btn btn-danger" @click="showRegisterForm()" title="Register a new account">Register</button>
                                <button class="btn btn-danger" @click="showForgotPWForm()" title="Find your password">Forgot</button>
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
                                    <button v-if="!data.canSelect" class="choose-button button-disabled">Disabled</button>
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
                            <div class="d-flex flex-column mt-2">
                                <select v-model="gameLevel">
                                    <option v-for="(data, index) in levelCount" :key="index" :value="data">{{ 'Level ' + data }}</option>
                                </select>
                            </div>
                            <div class="mt-2 d-grid">
                                <button class="btn btn-success text-large" @click="onPlay()">Play</button>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Copyright & Contact -->
                <div class="row justify-content-center">
                    <div class="col-xl-6 col-lg-8 col-md-12 col-sm-12 col-12">
                        <div class="control-block d-flex flex-column align-items-center">
                            <b>Copyright&nbsp;©&nbsp;NumberGo</b>
                            <small>service@numbergo.me</small>
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
                        <input class="form-control" type="password" placeholder="6 ~ 30 characters" v-model="loginInputs.password" @keydown="keyDown($event, 'enter', onLogin)" />
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
                        <input class="form-control" type="text" v-model="registerInputs.email" @keydown="keyDown($event, 'enter', onRegister)" />
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
                        <input class="form-control" type="text" placeholder="6 ~ 20 character length" />
                    </div>
                    <div class="mt-2">
                        <b>EMail</b>
                        <input class="form-control" type="text" />
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
                        <small class="text-danger">(100NT&nbsp;/&nbsp;Forever)</small>
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
    </div>
</template>

<script>
import topform from '../components/topform.vue';
import KeyBoard from '../utils/keyboard.js';
export default {
    name: 'menu',
    data() {
        return {
            userProfile: {
                account: '',
                nickName: '',
                canUpgradeAccount: true
            },
            loginInputs: {
                account: '',
                password: ''
            },
            loginErrorMessages: {
                account: '',
                password: ''
            },
            registerInputs: {
                account: '',
                password: '',
                confirmPassword: '',
                email: ''
            },
            upgradeAccountInputs: {
                name: '',
                tel: ''
            },
            registerErrorMessages: {
                account: '',
                password: '',
                confirmPassword: '',
                email: ''
            },
            upgradeAccountErrorMessages: {
                name: '',
                tel: ''
            },
            forgotPWInputs: {
                account: '',
                email: ''
            },
            shopItems: [
                { title: 'Default', imgsrc: '/img/logo.jpg', canSelect: true, isSelected: true, skinName: 'scale' },
                { title: 'Clock', imgsrc: '/img/clocklogo.jpg', canSelect: false, isSelected: false, skinName: 'clock' },
                { title: 'Ghost', imgsrc: '/img/ghostlogo.jpg', canSelect: false, isSelected: false, skinName: 'ghost' },
                { title: 'Gear', imgsrc: '/img/gearlogo.jpg', canSelect: false, isSelected: false, skinName: 'gear' }
            ],
            levelCount: 20,
            gameLevel: '1',
            selectSkinName: 'scale',
            isLogin: false,
            isShowMenu: true,
            isShowLoginForm: false,
            isShowRegisterForm: false,
            isShowForgotPWForm: false,
            isShowUpgradeAccountForm: false
        };
    },
    methods: {
        //events
        onLogin() {},
        onLogout() {},
        onRegister() {},
        onForgotPW() {},
        onPlay() {},
        onUpgradeAccount() {},
        //receive events
        selectSkinChanged(skinName) {
            this.selectSkinName = skinName;
            this.refreshSkinSelectButton(skinName);
        },
        //設定所有皮膚按鈕都可選擇
        setSkinButtonEnable() {
            for (let i = 0; i < this.shopItems.length; i++) {
                this.shopItems[i].canSelect = true;
            }
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
        setNickName(name) {
            this.userProfile.nickName = name;
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
        //panel status
        setLoginStatus(isLogin) {
            this.isLogin = isLogin;
        },
        setUserProfile(account) {
            this.userProfile.account = account;
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
        },
        closeUpgradeAccountForm() {
            this.isShowUpgradeAccountForm = false;
            this.clearUpgradeAccountInputs();
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
        //utils
        keyDown: KeyBoard.keyDown
    },
    components: { topform }
};
</script>

<style scoped>
select {
    border: 1px solid rgba(0, 0, 0, 0.5);
    border-radius: 5px;
    padding: 5px;
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
