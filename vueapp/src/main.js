import Vue from 'vue'
import Ajax from '../utils/ajax.js'
import Element from '../utils/element.js'
import MenuVue from '../templates/menu.vue'
import GameVue from '../templates/game.vue'
import ResultVue from '../templates/result.vue'
import LoadingVue from '../templates/loading.vue'
let menuChild, gameChild, resultChild, loadingChild;

//-------帳號相關方法--------
function onLogin(child) {
  loadingChild.showLoading();
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
    loadingChild.closeLoading();
  }, function () {
    toastr.error('Login failed.');
    loadingChild.closeLoading();
  });
}
function onRegister(child) {
  loadingChild.showLoading();
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
    loadingChild.closeLoading();
  }, function () {
    toastr.error('Register failed.');
    loadingChild.closeLoading();
  });
}
function onForgotPW(child) {

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
function onGetProfile(child) {
  loadingChild.showLoading();
  Ajax.ajax('user/getprofile', {
  }, function (res) {
    child.setLoginStatus(res.haslogin);
    if (res.haslogin) {
      child.setUserProfile(res.account);
      child.setCanUpgradeAccount(!res.ispremium);
      if (res.ispremium) {
        child.setSkinButtonEnable();
      }
    }
    else {
      child.setUserProfile('');
      child.setCanUpgradeAccount(false);
    }
    loadingChild.closeLoading();
  }, function () {
    toastr.error('Get user profile failed.');
    loadingChild.closeLoading();
  });
}
function onUpgradeAccount(child) {
  loadingChild.showLoading();
  Ajax.ajax('trade/create', {
    name: child.upgradeAccountInputs.name,
    tel: child.upgradeAccountInputs.tel
  }, function (res) {
    if (res.status) {
      let form = Element.createForm(res.objects.tradeUrl, 'POST');
      form.append(Element.createHiddenInput('WebNo', res.objects.webNo));
      form.append(Element.createHiddenInput('PassCode', res.objects.passCode));
      form.append(Element.createHiddenInput('OrderNo', res.objects.orderNo));
      form.append(Element.createHiddenInput('ECPlatform', res.objects.ecPlatform));
      form.append(Element.createHiddenInput('TotalPrice', res.objects.totalPrice));
      form.append(Element.createHiddenInput('OrderInfo', res.objects.orderinfo));
      form.append(Element.createHiddenInput('ReceiverTel', res.objects.receiverTel));
      form.append(Element.createHiddenInput('ReceiverName', res.objects.receiverName));
      form.append(Element.createHiddenInput('ReceiverEmail', res.objects.receiverEmail));
      form.append(Element.createHiddenInput('ReceiverID', res.objects.receiverID));
      form.append(Element.createHiddenInput('PayType', res.objects.payType));
      form.append(Element.createHiddenInput('PayEN', res.objects.payEN));
      document.body.append(form);
      form.submit();
    }
    else {
      if (res.errormsgs !== undefined) {
        child.setUpgradeAccountErrorMessages(res.errormsgs);
      }
      else {
        toastr.error(res.msg);
      }
      loadingChild.closeLoading();
    }
  }, function () {
    toastr.error('Upgrade check failed.');
    loadingChild.closeLoading();
  });
}
//-----------遊戲相關
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
    loadingChild.closeLoading();
  });
}
function onPlay(child) {
  if (child.getNickName().length == 0) {
    child.setNickName('Guest');
  }
  menuChild.closeMenu();
  closeBackground();
  let skinName = menuChild.getSelectSkinName();
  //                                                                       audio url
  gameChild.startGame(child.getGameLevel(), `${skinName}-anima`, `/sound/${skinName}`);
}
function onRestart(child) {
  child.closeForm();
  gameChild.restartGame();
}
function onGameOver(child, ispass) {
  resultChild.setElapsedTimeText(child.getElapsedTime());
  if (ispass) {
    resultChild.showPassForm();
  }
  else {
    resultChild.showFailForm();
  }
}
function onToMenu(child) {
  child.closeForm();
  gameChild.stopGame();
  showBackground();
  menuChild.showMenu();
}
function onShareLink(child) {
  if (child.hasShareLink())
    return;
  onWriteScore(
    menuChild.getNickName(),
    menuChild.getGameLevel(),
    gameChild.getElapsedTime(),
    (url) => {
      child.setShareLink(url);
    });
}

//-----------背景方法-----------------
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
    child.onPlay = () => { onPlay(child); };
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
    child.onGameOver = (ispass) => { onGameOver(child, ispass); }
    gameChild = child;
  },
  render: h => h(GameVue)
});

//結算畫面
new Vue({
  el: '#result',
  mounted() {
    let child = this.$children[0];
    child.onRestart = () => { onRestart(child); };
    child.onToMenu = () => { onToMenu(child); };
    child.onShareLink = () => { onShareLink(child); };
    resultChild = child;
  },
  render: h => h(ResultVue)
});

$(document).ready(function () {
  showBackground();
  Ajax.setCSRFToken(document.getElementsByName('c_token')[0].value);
  onGetProfile(menuChild);
});