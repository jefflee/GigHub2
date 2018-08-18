var GigsController = (function GigsConrollerIIFE() {
    var button;

    var init = function () {
        $('.js-toggle-attendance').click(toggleAttendances);

    };

    function toggleAttendances(e) {
        button = $(e.target);
        if (button.hasClass("btn-default")) {
            // WebApi read data from body. It read the key which is empty string to get data.
            $.post('/api/attendances', { GigId: button.attr("data-gig-id") })
                .done(doneAction)
                .fail(failAction);
        } else {
            $.ajax({
                url: "/api/attendances/" + button.attr("data-gig-id"),
                method: "DELETE"
            }).done(doneAction).fail(failAction);
        }

    }

    function doneAction() {
        var text = (button.text() == "Going") ? "Going?" : "Going";
        button.toggleClass("btn-info").toggleClass("btn-default").text(text);
    }

    function failAction() {
        alert("Somthing failed");
    }

    return {
        init: init
    }
})();