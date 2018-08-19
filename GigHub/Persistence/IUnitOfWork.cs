using GigHub.Repositories;

namespace GigHub.Persistence
{
    public interface IUnitOfWork
    {
        IGigRepository Gigs { get; }
        IAttendanceRepository Attendance { get; }
        IGenreRepository Genres { get; }
        IFollowingRepository Followings { get; }
        void Complete();
    }
}