
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using SampleProject.Infrastructure.Data;
using SampleProject.Domain;
using SampleProject.Domain.Repositories.Interfaces;
using SampleProject.Test.Setup;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Xunit;

namespace SampleProject.Test.Controllers
{
    public class TaskItemsControllerIntTest
    {
        public TaskItemsControllerIntTest()
        {
            _factory = new AppWebApplicationFactory<TestStartup>().WithMockUser();
            _client = _factory.CreateClient();

            _taskItemRepository = _factory.GetRequiredService<ITaskItemRepository>();


            InitTest();
        }

        private const string DefaultTitle = "AAAAAAAAAA";
        private const string UpdatedTitle = "BBBBBBBBBB";

        private const string DefaultDescription = "AAAAAAAAAA";
        private const string UpdatedDescription = "BBBBBBBBBB";

        private readonly AppWebApplicationFactory<TestStartup> _factory;
        private readonly HttpClient _client;
        private readonly ITaskItemRepository _taskItemRepository;

        private TaskItem _taskItem;


        private TaskItem CreateEntity()
        {
            return new TaskItem
            {
                Title = DefaultTitle,
                Description = DefaultDescription,
            };
        }

        private void InitTest()
        {
            _taskItem = CreateEntity();
        }

        [Fact]
        public async Task CreateTaskItem()
        {
            var databaseSizeBeforeCreate = await _taskItemRepository.CountAsync();

            // Create the TaskItem
            var response = await _client.PostAsync("/api/task-items", TestUtil.ToJsonContent(_taskItem));
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            // Validate the TaskItem in the database
            var taskItemList = await _taskItemRepository.GetAllAsync();
            taskItemList.Count().Should().Be(databaseSizeBeforeCreate + 1);
            var testTaskItem = taskItemList.Last();
            testTaskItem.Title.Should().Be(DefaultTitle);
            testTaskItem.Description.Should().Be(DefaultDescription);
        }

        [Fact]
        public async Task CreateTaskItemWithExistingId()
        {
            var databaseSizeBeforeCreate = await _taskItemRepository.CountAsync();
            // Create the TaskItem with an existing ID
            _taskItem.Id = 1L;

            // An entity with an existing ID cannot be created, so this API call must fail
            var response = await _client.PostAsync("/api/task-items", TestUtil.ToJsonContent(_taskItem));
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            // Validate the TaskItem in the database
            var taskItemList = await _taskItemRepository.GetAllAsync();
            taskItemList.Count().Should().Be(databaseSizeBeforeCreate);
        }

        [Fact]
        public async Task GetAllTaskItems()
        {
            // Initialize the database
            await _taskItemRepository.CreateOrUpdateAsync(_taskItem);
            await _taskItemRepository.SaveChangesAsync();

            // Get all the taskItemList
            var response = await _client.GetAsync("/api/task-items?sort=id,desc");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var json = JToken.Parse(await response.Content.ReadAsStringAsync());
            json.SelectTokens("$.[*].id").Should().Contain(_taskItem.Id);
            json.SelectTokens("$.[*].title").Should().Contain(DefaultTitle);
            json.SelectTokens("$.[*].description").Should().Contain(DefaultDescription);
        }

        [Fact]
        public async Task GetTaskItem()
        {
            // Initialize the database
            await _taskItemRepository.CreateOrUpdateAsync(_taskItem);
            await _taskItemRepository.SaveChangesAsync();

            // Get the taskItem
            var response = await _client.GetAsync($"/api/task-items/{_taskItem.Id}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var json = JToken.Parse(await response.Content.ReadAsStringAsync());
            json.SelectTokens("$.id").Should().Contain(_taskItem.Id);
            json.SelectTokens("$.title").Should().Contain(DefaultTitle);
            json.SelectTokens("$.description").Should().Contain(DefaultDescription);
        }

        [Fact]
        public async Task GetNonExistingTaskItem()
        {
            var maxValue = long.MaxValue;
            var response = await _client.GetAsync("/api/task-items/" + maxValue);
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task UpdateTaskItem()
        {
            // Initialize the database
            await _taskItemRepository.CreateOrUpdateAsync(_taskItem);
            await _taskItemRepository.SaveChangesAsync();
            var databaseSizeBeforeUpdate = await _taskItemRepository.CountAsync();

            // Update the taskItem
            var updatedTaskItem = await _taskItemRepository.QueryHelper().GetOneAsync(it => it.Id == _taskItem.Id);
            // Disconnect from session so that the updates on updatedTaskItem are not directly saved in db
            //TODO detach
            updatedTaskItem.Title = UpdatedTitle;
            updatedTaskItem.Description = UpdatedDescription;

            var response = await _client.PutAsync($"/api/task-items/{_taskItem.Id}", TestUtil.ToJsonContent(updatedTaskItem));
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // Validate the TaskItem in the database
            var taskItemList = await _taskItemRepository.GetAllAsync();
            taskItemList.Count().Should().Be(databaseSizeBeforeUpdate);
            var testTaskItem = taskItemList.Last();
            testTaskItem.Title.Should().Be(UpdatedTitle);
            testTaskItem.Description.Should().Be(UpdatedDescription);
        }

        [Fact]
        public async Task UpdateNonExistingTaskItem()
        {
            var databaseSizeBeforeUpdate = await _taskItemRepository.CountAsync();

            // If the entity doesn't have an ID, it will throw BadRequestAlertException
            var response = await _client.PutAsync("/api/task-items/1", TestUtil.ToJsonContent(_taskItem));
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            // Validate the TaskItem in the database
            var taskItemList = await _taskItemRepository.GetAllAsync();
            taskItemList.Count().Should().Be(databaseSizeBeforeUpdate);
        }

        [Fact]
        public async Task DeleteTaskItem()
        {
            // Initialize the database
            await _taskItemRepository.CreateOrUpdateAsync(_taskItem);
            await _taskItemRepository.SaveChangesAsync();
            var databaseSizeBeforeDelete = await _taskItemRepository.CountAsync();

            var response = await _client.DeleteAsync($"/api/task-items/{_taskItem.Id}");
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);

            // Validate the database is empty
            var taskItemList = await _taskItemRepository.GetAllAsync();
            taskItemList.Count().Should().Be(databaseSizeBeforeDelete - 1);
        }

        [Fact]
        public void EqualsVerifier()
        {
            TestUtil.EqualsVerifier(typeof(TaskItem));
            var taskItem1 = new TaskItem
            {
                Id = 1L
            };
            var taskItem2 = new TaskItem
            {
                Id = taskItem1.Id
            };
            taskItem1.Should().Be(taskItem2);
            taskItem2.Id = 2L;
            taskItem1.Should().NotBe(taskItem2);
            taskItem1.Id = 0;
            taskItem1.Should().NotBe(taskItem2);
        }
    }
}
