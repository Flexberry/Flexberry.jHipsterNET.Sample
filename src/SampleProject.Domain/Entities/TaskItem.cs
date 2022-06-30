using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace SampleProject.Domain
{
    [Table("task_item")]
    public class TaskItem : BaseEntity<long>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public IList<Job> Jobs { get; set; } = new List<Job>();

        // jhipster-needle-entity-add-field - JHipster will add fields here, do not remove

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var taskItem = obj as TaskItem;
            if (taskItem?.Id == null || taskItem?.Id == 0 || Id == 0) return false;
            return EqualityComparer<long>.Default.Equals(Id, taskItem.Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public override string ToString()
        {
            return "TaskItem{" +
                    $"ID='{Id}'" +
                    $", Title='{Title}'" +
                    $", Description='{Description}'" +
                    "}";
        }
    }
}
