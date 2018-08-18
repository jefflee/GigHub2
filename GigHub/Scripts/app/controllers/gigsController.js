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