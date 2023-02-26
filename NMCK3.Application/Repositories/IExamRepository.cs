using NMCK3.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NMCK3.Application.Repositories
{
    public interface IExamRepository
    {
        Task<IEnumerable<Exam>> GetExams(CancellationToken cancellationToken = default);
        Task<Exam> GetExamById(Guid examId, CancellationToken cancellationToken = default);
        void Add(Exam exam);
        void Remove(Exam exam);
    }
}
