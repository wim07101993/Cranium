using Cranium.Data.DbModels;
using Cranium.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace Cranium.Data.MongoDb
{
    public class TaskService : AModelService<TaskModel, Models.Task>
    {
        #region FIELDS

        private readonly IAttachmentService _attachmentService;
        private readonly ISolutionService _solutionService;
        private readonly ITaskTypeService _taskTypeService;

        #endregion FIELDS


        #region CONSTRUCTORS

        public TaskService(IDataServiceSettings settings, 
            IAttachmentService attachmentService, ISolutionService solutionService, ITaskTypeService taskTypeService) 
            : base(settings)
        {
            _attachmentService = attachmentService;
            _solutionService = solutionService;
            _taskTypeService = taskTypeService;
        }

        #endregion CONSTRUCTORS


        #region METHODS

        #region create
        
        protected override async Task<Models.Task> CreateAsync(Models.Task item, bool generateNewId)
        {
            var dbTask = new TaskModel
            {
                Id = item.Id,
                TaskType = item.TaskType.Id,
                Tip = item.Tip,
                Value = item.Value,

                Attachments = item.Attachments
                    .Select(x => x.Id)
                    .ToList(),

                Solutions = item.Solutions
                    .Select(solution => new SolutionModel
                    {
                        Id = solution.Id,
                        Attachments = solution.Attachments.Select(x => x.Id).ToList(),
                        Info = solution.Info,
                        IsCorrect = solution.IsCorrect,
                        Value = solution.Value,
                    })
                    .ToList()
            };

            dbTask = await CreateInDbAsync(dbTask, generateNewId);
            return await DbModelToModelAsync(dbTask);
        }

        #endregion create

        #region get

        public override async Task<IList<Models.Task>> GetAsync()
        {
            var dbTasks = await GetFromDbAsync();

            var tasks = new List<Models.Task>(dbTasks.Count);
            foreach (var dbTask in dbTasks)
                tasks.Add(await DbModelToModelAsync(dbTask));

            return tasks;
        }

        public override async Task<Models.Task> GetOneAsync(Guid id)
        {
            var dbTask = await GetOneFromDbAsync(id);
            return await DbModelToModelAsync(dbTask);
        }
        
        #endregion get

        #region update

        public override async Task UpdateAsync(Models.Task newItem)
        {
            var dbTask = await ModelToDbModelAsync(newItem);
            await UpdateInDbAsync(dbTask);
        }

        #endregion update

        #region delete

        public override async Task RemoveAsync(Guid id)
        {
            await RemoveFromDbAsync(id);
        }

        #endregion delete

        public override async Task<Models.Task> DbModelToModelAsync(TaskModel dbTask)
        {
            return new Models.Task(
               await GetAttachmentsAsync(dbTask.Attachments),
               await GetSolutionsAsync(dbTask))
            {
                Id = dbTask.Id,
                TaskType = await _taskTypeService.GetOneAsync(dbTask.TaskType),
                Tip = dbTask.Tip,
                Value = dbTask.Value
            };
        }

        public override async Task<TaskModel> ModelToDbModelAsync(Models.Task task)
        {

            var dbTask = new TaskModel
            {
                Id = task.Id,
                TaskType = task.TaskType.Id,
                Tip = task.Tip,
                Value = task.Value,

                Attachments = task.Attachments
                    .Select(x => x.Id)
                    .ToList(),

                Solutions = task.Solutions
                    .Select(solution => new SolutionModel
                    {
                        Id = solution.Id,
                        Attachments = solution.Attachments.Select(x => x.Id).ToList(),
                        Info = solution.Info,
                        IsCorrect = solution.IsCorrect,
                        Value = solution.Value,
                    })
                    .ToList()
            };
            return dbTask;
        }

        private async Task<IList<Solution>> GetSolutionsAsync(TaskModel task)
        {
            var list = new List<Solution>();

            foreach (var solutionModel in task.Solutions)
            {
                list.Add(new Solution(await GetAttachmentsAsync(solutionModel.Attachments))
                {
                    Id = solutionModel.Id,
                    Info = solutionModel.Info,
                    IsCorrect = solutionModel.IsCorrect,
                    Value = solutionModel.Value
                });
            }

            return list;
        }

        private async Task<IList<Attachment>> GetAttachmentsAsync(IEnumerable<Guid> ids)
        {
            var list = new List<Attachment>();

            foreach (var id in ids)
                list.Add(await _attachmentService.GetOneAsync(id));

            return list;
        }

        #endregion METHODS
    }
}
