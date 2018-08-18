var GigDetailController = (function iifeGigDetailController(followingService) {

    var followButton;

    var init = function initFunc() {
        $('.js-toggle-follow').click(toggleFollowing);
    }

    function toggleFollowing(e) {
        followButton = $(e.target);
        var followeeId = followButton.attr("data-user-id");

        if (followButton.hasClass("btn-default")) {
            followingService.createFollowing(followeeId, doneAction, failAction);
        } else {
            followingService.deleteFollowing(followeeId, doneAction, failAction);
        }
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

})(FollowingService);