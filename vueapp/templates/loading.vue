<template>
    <div id="loading" class="base-layer" v-show="isShowLoading">
        <img src="img/loading.png" class="rotate-anima"/>
    </div>
</template>

<script>
export default {
    name: 'loading',
    data() {
        return {
            isShowLoading: false,
        };
    },
    methods: {
        showLoading() {
            this.isShowLoading = true;
        },
        closeLoading() {
            this.isShowLoading = false;
        },
    },
};
</script>

<style scoped>
.base-layer {
    position: fixed;
    width: 100%;
    height: 100%;
    top: 0px;
    left: 0px;
    background-color: rgba(0, 0, 0, 0.5);
    z-index: 999999;
    display: flex;
    justify-content: center;
    align-items: center;
}
</style>