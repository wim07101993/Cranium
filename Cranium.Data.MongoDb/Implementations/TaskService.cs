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
        
        protected override async Task<Models.Task> CreateAsync(Models.Task model, bool generateNewId)
        {
            var dbTask = await ModelToDbModel(model);

            var attachmentTask = _attachmentService.UpdateOrCreateAsync(model.Attachments);
            var taskTypeTask = _taskTypeService.UpdateOrCreateAsync(model.TaskType);

            await Task.WhenAll(attachmentTask, taskTypeTask);

            var exceptions = new List<Exception>();
            if (attachmentTask.Exception != null)
                exceptions.Add(attachmentTask.Exception);
            if (taskTypeTask.Exception != null)
                exceptions.Add(taskTypeTask.Exception);

            if (exceptions.Count > 0)
                throw new AggregateException("Some exceptions happened whil trying to create a new task", exceptions);

            dbTask = await CreateInDbAsync(dbTask, generateNewId);
            return await DbModelToModelAsync(dbTask);
        }
        
        public override async Task UpdateAsync(Models.Task newModel)
        {
            var dbTask = await ModelToDbModel(newModel);

            var attachmentTask = _attachmentService.UpdateAsync(newModel.Attachments);
            var taskTypeTask = _taskTypeService.UpdateAsync(newModel.TaskType);

            await Task.WhenAll(attachmentTask, taskTypeTask);

            var exceptions = new List<Exception>();
            if (attachmentTask.Exception != null)
                exceptions.Add(attachmentTask.Exception);
            if (taskTypeTask.Exception != null)
                exceptions.Add(taskTypeTask.Exception);

            if (exceptions.Count > 0)
                throw new AggregateException("Some exceptions happened whil trying to upate a task", exceptions);

            await UpdateInDbAsync(dbTask);
        }

        public override async Task RemoveAsync(Guid id)
        {
            var dbModel = GetOneFromDbAsync(id);



            await RemoveFromDbAsync(id);
        }

        #endregion delete

        public override async Task<Models.Task> DbModelToModelAsync(TaskModel dbModel)
        {
            return new Models.Task(
               await GetAttachmentsAsync(dbModel.Attachments),
               await GetSolutionsAsync(dbModel))
            {
                Id = dbModel.Id,
                TaskType = await _taskTypeService.GetOneAsync(dbModel.TaskType),
                Tip = dbModel.Tip,
                Value = dbModel.Value
            };
        }

        public override Task<TaskModel> ModelToDbModel(Models.Task model)
        {
            return Task.FromResult(new TaskModel
            {
                Id = model.Id,
                TaskType = model.TaskType.Id,
                Tip = model.Tip,
                Value = model.Value,

                Attachments = model.Attachments
                    .Select(x => x.Id)
                    .ToList(),

                Solutions = model.Solutions
                    .Select(solution => new SolutionModel
                    {
                        Id = solution.Id,
                        Attachments = solution.Attachments.Select(x => x.Id).ToList(),
                        Info = solution.Info,
                        IsCorrect = solution.IsCorrect,
                        Value = solution.Value,
                    })
                    .ToList()
            });
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
