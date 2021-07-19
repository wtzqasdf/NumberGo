let csrfToken = '';
export default {
    setCSRFToken(token) {
        csrfToken = token;
    },
    ajax(url, data, success, error) {
        data['c_token'] = csrfToken;
        $.ajax({
            type: 'POST',
            async: true,
            url: url,
            data: data,
            dataType: 'json',
            success: function (res) {
                success(res);
            },
            error: function () {
                error();
            }
        });
    }
}