var GigDetailController = (function iifeGigDetailController() {

    var followButton;

    var init = function initFunc() {
        $('.js-toggle-follow').click(function (e) {
            followButton = $(e.target);

            if (followButton.hasClass("btn-default")) {

                // WebApi read data from body. It read the key which is empty string to get data.
                $.post('/api/followings', { FolloweeId: followButton.attr("data-user-id") })
                    .done(doneAction)
                    .fail(failAction);
            } else {
                $.ajax({
                    url: "/api/followings/" + followButton.attr("data-user-id"),
                    method: "DELETE"
                }).done(doneAction)
                    .fail(failAction);
            }
        });
    }

    function doneAction() {
        var text = (followButton.text() == "Follow") ? "Following" : "Follow";
        followButton.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };

    function failAction() {
        alert("Something failed");
    };

    return {
        init: init
    }

})();