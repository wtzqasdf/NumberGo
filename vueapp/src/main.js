import Vue from 'vue'
import Ajax from '../utils/ajax.js'
import Element from '../utils/element.js'
import MenuVue from '../templates/menu.vue'
import GameVue from '../templates/game.vue'
import ResultVue from '../templates/result.vue'
import LoadingVue from '../templates/loading.vue'
let menuChild, gameChild, resultChild, loadingChild;

function onLogin(child) {
  child.loginButtonStatusSwitch(true);
  Ajax.ajax('user/login', {
    account: child.loginInputs.account,
    password: child.loginInputs.password
  }, function (res) {
    if (res.status) {
      onGetProfile(child);
      child.closeLoginForm();
    }
    else {
      child.setLoginErrorMessages(res.errormsgs);
    }
    child.loginButtonStatusSwitch(false);
  }, function () {
    toastr.error('Login failed.');
    child.loginButtonStatusSwitch(false);
  });
}

function onGetProfile(child) {
  loadingChild.showLoading();
  Ajax.ajax('user/getprofile', {
  }, function (res) {
    child.setLoginStatus(res.haslogin);
    child.setUserProfile(res.account, res.point);
    child.setCanUpgradeAccount(res.canupgrade);
    child.refreshSkinCanSelectButton(['ghost', 'jelly']);
    loadingChild.closeLoading();
  }, function () {
    toastr.error('Get user profile failed.');
  });
}

function onRegister(child) {
  child.registerButtonStatusSwitch(true);
  Ajax.ajax('user/register', {
    account: child.registerInputs.account,
    password: child.registerInputs.password,
    confirmPassword: child.registerInputs.confirmPassword,
    email: child.registerInputs.email
  }, function (res) {
    if (res.status) {
      child.closeRegisterForm();
      //可以設定跳出成功動畫
    }
    else {
      child.setRegisterErrorMessages(res.errormsgs);
    }
    child.registerButtonStatusSwitch(false);
  }, function () {
    toastr.error('Register failed.');
    child.registerButtonStatusSwitch(false);
  });
}

function onForgotPW(child) {

}

function onUpgradeAccount(child) {
  child.upgradeAccountButtonStatusSwitch(true);
  Ajax.ajax('trade/create', {
  }, function (res) {
    if (res.status) {
      let form = Element.createForm(res.msg.tradeUrl, 'POST');
      form.append(Element.createHiddenInput('WebNo', res.msg.webno));
      form.append(Element.createHiddenInput('PassCode', res.msg.passcode));
      form.append(Element.createHiddenInput('OrderNo', res.msg.ordermo));
      form.append(Element.createHiddenInput('ECPlatform', res.msg.ecplatform));
      form.append(Element.createHiddenInput('TotalPrice', res.msg.totalprice));
      form.append(Element.createHiddenInput('OrderInfo', res.msg.orderinfo));
      form.append(Element.createHiddenInput('ReceiverTel', res.msg.receivertel));
      form.append(Element.createHiddenInput('ReceiverName', res.msg.receivername));
      form.append(Element.createHiddenInput('ReceiverEmail', res.msg.receiveremail));
      form.append(Element.createHiddenInput('ReceiverID', res.msg.receiverid));
      form.append(Element.createHiddenInput('PayType', res.msg.paytype));
      form.append(Element.createHiddenInput('PayEN', res.msg.payen));
      form.append(Element.createHiddenInput('EPT', res.msg.ept));
      document.body.append(form);
      form.submit();
    }
    else {
      child.setUpgradeAccountErrorMessages(res.errormsgs);
    }
    child.upgradeAccountButtonStatusSwitch(false);
  }, function () {
    toastr.error('Upgrade check failed.');
  });
}

function onLogout(child) {
  Ajax.ajax('user/logout', {
  }, function (res) {
    if (res.status) {
      onGetProfile(child);
    }
  }, function () {
    toastr.error('Logout failed.');
  });
}

function onWriteScore(nickName, level, elapsedTime, success) {
  loadingChild.showLoading();
  Ajax.ajax('score/record', {
    nickname: nickName,
    level: level,
    elapsedtime: elapsedTime
  }, function (res) {
    success(res.url);
    loadingChild.closeLoading();
  }, function () {
    toastr.error('Score share failed.');
  });
}

//-----------背景設定-----------------
function showBackground() {
  document.getElementById('bg-image').className = 'bg-image';
}
function closeBackground() {
  document.getElementById('bg-image').className = '';
}

//--------------------分隔線--------------------
//讀取畫面
new Vue({
  el: '#loading',
  mounted() {
    let child = this.$children[0];
    loadingChild = child;
  },
  render: h => h(LoadingVue),
});

//主選單畫面
new Vue({
  el: '#menu',
  mounted() {
    let child = this.$children[0];
    child.onPlay = () => {
      if (child.getNickName().length == 0) {
        child.setNickName('Guest');
      }
      menuChild.closeMenu();
      closeBackground();
      gameChild.startGame(child.getGameLevel(), 'gear-anima');
    };
    child.onRegister = () => { onRegister(child); };
    child.onLogin = () => { onLogin(child); };
    child.onForgotPW = () => { onForgotPW(child); };
    child.onLogout = () => { onLogout(child); };
    child.onUpgradeAccount = () => { onUpgradeAccount(child); };
    menuChild = child;
  },
  render: h => h(MenuVue)
})

//遊戲畫面
new Vue({
  el: '#game',
  mounted() {
    let child = this.$children[0];
    child.onGameOver = (ispass) => {
      resultChild.setElapsedTimeText(child.getElapsedTime());
      if (ispass) {
        resultChild.showPassForm();
      }
      else {
        resultChild.showFailForm();
      }
    }
    gameChild = child;
  },
  render: h => h(GameVue)
});

//結算畫面
new Vue({
  el: '#result',
  mounted() {
    let child = this.$children[0];
    child.onRestart = () => {
      child.closeForm();
      gameChild.restartGame();
    };
    child.onToMenu = () => {
      child.closeForm();
      gameChild.stopGame();
      showBackground();
      menuChild.showMenu();
    };
    child.onShareLink = () => {
      if (child.hasShareLink())
        return;
      onWriteScore(
        menuChild.getNickName(),
        menuChild.getGameLevel(),
        gameChild.getElapsedTime(),
        (url) => {
          child.setShareLink(url);
        });
    };
    resultChild = child;
  },
  render: h => h(ResultVue)
});

$(document).ready(function () {
  showBackground();
  Ajax.setCSRFToken(document.getElementsByName('c_token')[0].value);
  onGetProfile(menuChild);
});