export default {
    createHiddenInput(name, value) {
        let input = document.createElement('input');
        input.name = name;
        input.value = value;
        return input;
    },
    createForm(action, method) {
        let form = document.createElement('form');
        form.action = action;
        form.method = method;
        return form;
    },
}