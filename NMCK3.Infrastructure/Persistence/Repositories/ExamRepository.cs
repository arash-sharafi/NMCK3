using Microsoft.EntityFrameworkCore;
using NMCK3.Application.Repositories;
using NMCK3.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NMCK3.Infrastructure.Persistence.Repositories
{
    public class ExamRepository : IExamRepository
    {
        private readonly ApplicationDbContext _context;

        public ExamRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Exam>> GetExams(CancellationToken cancellationToken = default)
        {
            return await _context.Exams.ToListAsync(cancellationToken);
        }

        public async Task<Exam> GetExamById(Guid examId, CancellationToken cancellationToken = default)
        {
            var result = await _context.Exams
                .Include(x => x.ExamReservations)
                .ThenInclude(x => x.Participant)
                .FirstOrDefaultAsync(x => x.Id == examId, cancellationToken);
            return result;
        }

        public void Add(Exam exam)
        {
            _context.Exams.Add(exam);
        }

        public void Remove(Exam exam)
        {
            _context.Exams.Remove(exam);
        }
    }
}
