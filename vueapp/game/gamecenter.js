let buttons;
let buttonAttributes = {
    class: '',
    bgColor: '',
    textColor: ''
};

let count = 0;
let maxCount = 0;
let totalNumber = 0;
let targetNumber = 0;

function onButtonClick(e) {
    let nums = createNumbers(parseInt(e.target.textContent));
    pushButtonTo(nums);
}

function createButtonElement(text) {
    let btn = {};
    btn.text = text;
    btn.class = buttonAttributes.class;
    btn.style = `background-color:${buttonAttributes.bgColor}; color:${buttonAttributes.textColor};`;
    btn.onclick = (e) => { onButtonClick(e); };
    return btn;
}

function createNumbers(num) {
    let btns = [];
    if (num === 0) {
        btns.push(createButtonElement(1));
        count++;
        totalNumber += 1;
    }
    else {
        let resultNum = num + 1;
        for (let i = 0; i < num; i++) {
            if (count >= maxCount)
                break;
            btns.push(createButtonElement(resultNum));
            count++;
            totalNumber += resultNum;
        }
    }
    exports.default.onCreateButton();
    return btns;
}

function pushButtonTo(btns) {
    for (let i = 0; i < btns.length; i++) {
        buttons.push(btns[i]);
    }
}

//---------這邊可能需要有資源網址參數行為--------
export default {
    init(array) {
        buttons = array;
    },
    /**
     * 重置目前的遊戲狀態
     * @param {*} max 最大數量
     * @param {*} target 目標數字
     */
    reset(max, target) {
        count = 0;
        maxCount = max;
        totalNumber = 0;
        targetNumber = target;

        buttons.splice(0, buttons.length);
        pushButtonTo(createNumbers(0));
    },
    setButtonAnimationClass(s) {
        buttonAttributes.class = s;
    },
    setButtonBGColor(color) {
        buttonAttributes.bgColor = color;
    },
    setButtonTextColor(color) {
        buttonAttributes.textColor = color;
    },
    //--------------calc methods---------------
    /**
     * 產生指定數量的數字，這會從2開始產生
     * @param {*} maxCount 最大數量
     * @param {*} min 數字的下限
     * @param {*} max 數字的上限
     * @returns
     */
    randTargetNumber(maxCount) {
        let count = 0;
        let totalNum = 0;
        let min = 2;
        let maxTarget = min;
        while (count < maxCount) {
            //紀錄最大目標數字，以便讓系統產生有順序的數字，並且也可以動態自動調整上限
            //random範圍為0 ~ 0.999，而加1是因為random範圍不會超過1，例如3 - 2 + 1，rand為0.98，結果是2 * 0.98 = 1.96(1)
            let target = min + parseInt((maxTarget - min + 1) * Math.random());
            if (target >= maxTarget) {
                maxTarget = target + 1;
            }
            //減1是因為產生n(t - 1)個t的數字，例如按2產生2個3
            for (let i = 0; i < target - 1; i++) {
                count++;
                totalNum += target;
                if (count >= maxCount) {
                    break;
                }
            }
        }
        return totalNum;
    },
    //--------------game methods---------------
    /**
     * 指出目前數量是否到達或超過最大數量
     * @returns 返回boolean
     */
    isOverCount() {
        return count >= maxCount;
    },
    /**
     * 指出目前累計數字是否與目標數字一致
     * @returns 返回boolean
     */
    isTargetReached() {
        return totalNumber === targetNumber;
    },
    /**
     * 指出目前數字是否超過目標數字
     * @returns 返回boolean
     */
    isNumberOverTarget() {
        return totalNumber > targetNumber;
    },
    getCurrentCount() {
        return count;
    },
    getMaxCount() {
        return maxCount;
    },
    getCurrentNumber() {
        return totalNumber;
    },
    getTargetNumber() {
        return targetNumber;
    },
    //------------output events------------
    onCreateButton() { },
}