var FollowingService = (function () {
    var createFollowing = function (followeeId, done, fail) {

        // WebApi read data from body. It read the key which is empty string to get data.
        $.post("/api/followings", { followeeId: followeeId })
            .done(done)
            .fail(fail);

    };

    var deleteFollowing = function (followeeId, done, fail) {
        $.ajax({
                url: "/api/followings/" + followeeId,
                method: "DELETE"
            })
            .done(done)
            .fail(fail);
    };

    return {
        createFollowing: createFollowing,
        deleteFollowing: deleteFollowing
    }
})();