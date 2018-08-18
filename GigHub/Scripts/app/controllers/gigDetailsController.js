var GigDetailController = (function iifeGigDetailController() {

    var init = function initFunc() {
        $('.js-toggle-follow').click(function (e) {
            var button = $(e.target);

            if (button.hasClass("btn-default")) {

                // WebApi read data from body. It read the key which is empty string to get data.
                $.post('/api/followings', { FolloweeId: button.attr("data-user-id") })
                    .done(function () {
                        button.addClass("btn-info").removeClass("btn-default").text('Following');
                    })
                    .fail(function () {
                        alert('Something failed!');
                    });
            } else {
                $.ajax({
                    url: "/api/followings/" + button.attr("data-user-id"),
                    method: "DELETE"
                }).done(function () {
                    button.addClass("btn-default").removeClass("btn-info").text('Follow');
                })
                    .fail(function () {
                        alert('Something failed!');
                    });
            }
        });
    }

    return {
        init: init
    }

})();