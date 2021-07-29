let langPack = [];

export default {
    loadPackage(pack) {
        if (langPack !== null) {
            langPack = null;
        }
        langPack = pack;
    },
    getText(key) {
        return langPack[key] == undefined ? '' : langPack[key];
    },
}