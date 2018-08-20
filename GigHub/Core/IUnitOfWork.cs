using GigHub.Core.Repositories;

namespace GigHub.Core
{
    public interface IUnitOfWork
    {
        IGigRepository Gigs { get; }
        IAttendanceRepository Attendance { get; }
        IGenreRepository Genres { get; }
        IFollowingRepository Followings { get; }
        IApplicationUserRepository Users { get; }
        void Complete();
    }
}