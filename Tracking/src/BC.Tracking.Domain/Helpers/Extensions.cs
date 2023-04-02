using System.Collections.Generic;
using System.Linq;

namespace Connexity.BC.Tracking.Domain.Helpers
{
    using Connexity.BC.Tracking.Domain.Interfaces;
    using static Connexity.BC.Tracking.Domain.Events.Models.TrackingDto;

    public static class Extensions
    {
        public static List<TDest> GetCopyOfDimmingProgramWeekDay<TSource, TDest>(this List<TSource> sources)
            where TSource : IDimmingProgramWeekDay
            where TDest : IDimmingProgramWeekDay, new()
        {
            return sources.Select(s => s.GetCopyOfDimmingProgramWeekDay<TSource, TDest>()).ToList();
        }

        public static List<DimmingProgramWeekDayDto> GetCopyOfDimmingProgramWeekDay<TSource>(this List<TSource> sources)
            where TSource : IDimmingProgramWeekDay
        {
            return sources.Select(s => s.GetCopyOfDimmingProgramWeekDay()).ToList();
        }

        public static TDest GetCopyOfDimmingProgramWeekDay<TSource, TDest>(this TSource source)
            where TSource : IDimmingProgramWeekDay
            where TDest : IDimmingProgramWeekDay, new()
        {
            return new TDest() { DimmingProgramId = source.DimmingProgramId, DayOfWeek = source.DayOfWeek };
        }

        public static DimmingProgramWeekDayDto GetCopyOfDimmingProgramWeekDay<TSource>(this TSource source)
            where TSource : IDimmingProgramWeekDay
        {
            return new DimmingProgramWeekDayDto()
            {
                DayOfWeek = source.DayOfWeek,
                DimmingProgramId = source.DimmingProgramId,
            };
        }

        public static List<TDest> GetCopyOfDimmingProgramExceptionDay<TSource, TDest>(this List<TSource> sources)
            where TSource : IDimmingProgramExceptionDay
            where TDest : IDimmingProgramExceptionDay, new()
        {
            return sources.Select(s => s.GetCopyOfDimmingProgramExceptionDay<TSource, TDest>()).ToList();
        }

        public static List<DimmingProgramExceptionDayDto> GetCopyOfDimmingProgramExceptionDay<TSource>(this List<TSource> sources)
            where TSource : IDimmingProgramExceptionDay
        {
            return sources.Select(s => s.GetCopyOfDimmingProgramExceptionDay()).ToList();
        }

        public static TDest GetCopyOfDimmingProgramExceptionDay<TSource, TDest>(this TSource source)
            where TSource : IDimmingProgramExceptionDay
            where TDest : IDimmingProgramExceptionDay, new()
        {
            return new TDest()
            {
                StartDate = source.StartDate,
                EndDate = source.EndDate,
                DimmingProgramId = source.DimmingProgramId,
                ExceptionType = source.ExceptionType
            };
        }

        public static DimmingProgramExceptionDayDto GetCopyOfDimmingProgramExceptionDay<TSource>(this TSource source)
            where TSource : IDimmingProgramExceptionDay
        {
            return new DimmingProgramExceptionDayDto()
            {
                StartDate = source.StartDate,
                EndDate = source.EndDate,
                DimmingProgramId = source.DimmingProgramId,
                ExceptionType = source.ExceptionType
            };
        }
    }
}