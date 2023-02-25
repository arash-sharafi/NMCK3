using NMCK3.Application.Repositories;
using NMCK3.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMCK3.Infrastructure.Persistance.Repositories
{
    public class ExamRepository : IExamRepository
    {
        private readonly List<Exam> _exams = new();

        public async Task<IEnumerable<Exam>> GetExams()
        {
            await Task.CompletedTask;
            return _exams;
        }

        public async Task<Exam> GetExamById(Guid examId)
        {
            await Task.CompletedTask;
            return _exams.FirstOrDefault(x => x.Id == examId);
        }

        public void Add(Exam exam)
        {
            _exams.Add(exam);
        }

        public void Remove(Exam exam)
        {
            _exams.Remove(exam);
        }
    }
}
