var AttendanceService = (function iifeAttendanceService() {
    var createAttendance = function createAttendanceFunc(gigId, doneAction, failAction) {
        // WebApi read data from body. It read the key which is empty string to get data.
        $.post('/api/attendances', { GigId: gigId })
            .done(doneAction)
            .fail(failAction);
    }

    var deleteAttendance = function deleteAttendanceFunc(gigId, doneAction, failAction) {
        $.ajax({
            url: "/api/attendances/" + gigId,
            method: "DELETE"
        }).done(doneAction)
            .fail(failAction);
    }

    return {
        createAttendance: createAttendance,
        deleteAttendance: deleteAttendance
    }
})();

var GigsController = (function iifeGigsConroller(attendanceService) {
    var button;

    var init = function () {
        $('.js-toggle-attendance').click(toggleAttendances);

    };

    function toggleAttendances(e) {
        button = $(e.target);
        var gigId = button.attr("data-gig-id");

        if (button.hasClass("btn-default")) {
            attendanceService.createAttendance(gigId, doneAction, failAction);
        } else {
            attendanceService.deleteAttendance(gigId, doneAction, failAction);
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
})(AttendanceService);