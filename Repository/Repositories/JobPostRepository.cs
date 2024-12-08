using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.Repository.Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.DataTransferObjects.JobPostDTOs;

namespace Repository.Repositories
{
    public class JobPostRepository : RepositoryBase<JobPost>, IJobPostRepository
    {
        public JobPostRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<JobPost>> GetAllJobPostsAsync(bool trackChanges)
               => await FindAll(trackChanges)
                .ToListAsync();

        public async Task<JobPost> GetJobPostAsync(string companyId, bool trackChanges) =>
            await FindByCondition(c => c.Id.ToString().Equals(companyId), trackChanges).Include(p => p.Company).SingleOrDefaultAsync();


        public void CreateJobPost(JobPost jobPost) => Create(jobPost);

        public void DeleteJobPost(JobPost jobPost) => Delete(jobPost);

        public void CreateJobPost(JobPostRegisterDTO jobPostRegisterDto)
        {
            if (jobPostRegisterDto == null)
                throw new ArgumentNullException(nameof(jobPostRegisterDto));

            var jobPost = new JobPost
            {
                Id = Guid.NewGuid(),
                CompanyId = jobPostRegisterDto.CompanyId,
                JobTitle = jobPostRegisterDto.JobTitle,
                JobDescription = jobPostRegisterDto.JobDescription,
                Location = jobPostRegisterDto.Location,
                SalaryRange = jobPostRegisterDto.SalaryRange,
                ApplicationDeadLine = jobPostRegisterDto.ApplicationDeadLine,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // Map enums (JobType and Experience)
            jobPost.GetType()
                .GetProperty(name: "Type")
                .SetValue(jobPost, jobPostRegisterDto.Type);

            jobPost.GetType()
                .GetProperty(name: "Experience")
                .SetValue(jobPost, jobPostRegisterDto.Level);

            Create(jobPost); // Add to the database context
        }

    }
}
