import Vue from 'vue'
import Ajax from '../utils/ajax.js'
import GameSetting from '../game/gamesetting.js'
import LangLoader from '../languages/langloader.js'
import ENLangPackage from '../languages/enpackage.js'
import TCLangPackage from '../languages/tcpackage.js'
import Element from '../utils/element.js'
import MenuVue from '../templates/menu.vue'
import GameVue from '../templates/game.vue'
import ResultVue from '../templates/result.vue'
import LoadingVue from '../templates/loading.vue'
let menuChild, gameChild, resultChild, loadingChild;

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
  gameChild.setSoundEffectEnabled(GameSetting.getSoundEffect());
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
//-----------遊戲語言-----------------
function onLanguageChange(langCode) {
  if (langCode === 'en') {
    LangLoader.loadPackage(ENLangPackage.create());
  }
  else {
    LangLoader.loadPackage(TCLangPackage.create());
  }
}
//-----------背景方法-----------------
function showBackground() {
  document.getElementById('bg-image').className = 'bg-image';
}
function closeBackground() {
  document.getElementById('bg-image').className = '';
}
//-----------------Vue初始化-----------------
//先載入語言以避免文字空白
onLanguageChange(GameSetting.getLanguageCode());

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
    child.onLanguageChange = () => {
      onLanguageChange(GameSetting.getLanguageCode());
      //寫在這裡是因為使用者一變更語言設定就馬上刷新UI語言
      menuChild.refreshUILanguage();
      gameChild.refreshUILanguage();
      resultChild.refreshUILanguage();
    };
    //child.initPayPalButton();
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
});