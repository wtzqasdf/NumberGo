export default {
    setSoundEffect(isEnable) {
        localStorage.setItem('se', isEnable);
    },
    setLanguageCode(langCode) {
        localStorage.setItem('lang', langCode);
    },
    getSoundEffect() {
        let b = localStorage.getItem('se');
        //如果為null就先新增
        if (b === null) {
            localStorage.setItem('se', true);
            b = 'true';
        }
        return b.toLowerCase() === 'true';
    },
    getLanguageCode() {
        let item = localStorage.getItem('lang');
        if (item === null) {
            localStorage.setItem('lang', 'en');
            item = 'en';
        }
        return item;
    }
}