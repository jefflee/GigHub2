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