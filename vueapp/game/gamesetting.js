export default {
    setSoundEffect(isEnable) {
        localStorage.setItem('se', isEnable);
    },
    getSoundEffect() {
        let b = localStorage.getItem('se');
        //如果為null就先新增
        if (b === null) {
            localStorage.setItem('se', true);
            b = 'true';
        }
        return b.toLowerCase() === 'true';
    }
}