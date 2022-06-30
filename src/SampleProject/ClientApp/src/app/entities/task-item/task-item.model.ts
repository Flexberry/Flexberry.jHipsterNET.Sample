import { IJob } from "app/entities/job/job.model";

export interface ITaskItem {
  id?: number;
  title?: string | null;
  description?: string | null;
  jobs?: IJob[] | null;
}

export class TaskItem implements ITaskItem {
  constructor(
    public id?: number,
    public title?: string | null,
    public description?: string | null,
    public jobs?: IJob[] | null
  ) {}
}

export function getTaskItemIdentifier(taskItem: ITaskItem): number | undefined {
  return taskItem.id;
}
