<template>
    <div class="form" v-show="isshow">
        <div class="container-fluid">
            <div class="row justify-content-center">
                <div class="col-xl-3 col-lg-6 col-md-12 col-sm-12 col-12">
                    <slot></slot>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    props: ['isshow'],
};
</script>

<style scoped>
.form {
    background-color: rgba(0, 0, 0, 0.3);
    position: fixed;
    left: 0px;
    top: 0px;
    width: 100%;
    height: 100%;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    z-index: 9999;
}
</style>