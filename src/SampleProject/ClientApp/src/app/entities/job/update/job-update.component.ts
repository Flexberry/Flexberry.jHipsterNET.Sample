import { Component, OnInit } from "@angular/core";
import { HttpResponse } from "@angular/common/http";
import { FormBuilder } from "@angular/forms";
import { ActivatedRoute } from "@angular/router";
import { Observable } from "rxjs";
import { finalize, map } from "rxjs/operators";

import { IJob, Job } from "../job.model";
import { JobService } from "../service/job.service";
import { ITaskItem } from "app/entities/task-item/task-item.model";
import { TaskItemService } from "app/entities/task-item/service/task-item.service";
import { IEmployee } from "app/entities/employee/employee.model";
import { EmployeeService } from "app/entities/employee/service/employee.service";

@Component({
  selector: "jhi-job-update",
  templateUrl: "./job-update.component.html",
})
export class JobUpdateComponent implements OnInit {
  isSaving = false;

  taskItemsSharedCollection: ITaskItem[] = [];
  employeesSharedCollection: IEmployee[] = [];

  editForm = this.fb.group({
    id: [],
    jobTitle: [],
    minSalary: [],
    maxSalary: [],
    tasks: [],
    employee: [],
  });

  constructor(
    protected jobService: JobService,
    protected taskItemService: TaskItemService,
    protected employeeService: EmployeeService,
    protected activatedRoute: ActivatedRoute,
    protected fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.activatedRoute.data.subscribe(({ job }) => {
      this.updateForm(job);

      this.loadRelationshipsOptions();
    });
  }

  previousState(): void {
    window.history.back();
  }

  save(): void {
    this.isSaving = true;
    const job = this.createFromForm();
    if (job.id !== undefined) {
      this.subscribeToSaveResponse(this.jobService.update(job));
    } else {
      this.subscribeToSaveResponse(this.jobService.create(job));
    }
  }

  trackTaskItemById(_index: number, item: ITaskItem): number {
    return item.id!;
  }

  trackEmployeeById(_index: number, item: IEmployee): number {
    return item.id!;
  }

  getSelectedTaskItem(
    option: ITaskItem,
    selectedVals?: ITaskItem[]
  ): ITaskItem {
    if (selectedVals) {
      for (const selectedVal of selectedVals) {
        if (option.id === selectedVal.id) {
          return selectedVal;
        }
      }
    }
    return option;
  }

  protected subscribeToSaveResponse(
    result: Observable<HttpResponse<IJob>>
  ): void {
    result.pipe(finalize(() => this.onSaveFinalize())).subscribe({
      next: () => this.onSaveSuccess(),
      error: () => this.onSaveError(),
    });
  }

  protected onSaveSuccess(): void {
    this.previousState();
  }

  protected onSaveError(): void {
    // Api for inheritance.
  }

  protected onSaveFinalize(): void {
    this.isSaving = false;
  }

  protected updateForm(job: IJob): void {
    this.editForm.patchValue({
      id: job.id,
      jobTitle: job.jobTitle,
      minSalary: job.minSalary,
      maxSalary: job.maxSalary,
      tasks: job.tasks,
      employee: job.employee,
    });

    this.taskItemsSharedCollection =
      this.taskItemService.addTaskItemToCollectionIfMissing(
        this.taskItemsSharedCollection,
        ...(job.tasks ?? [])
      );
    this.employeesSharedCollection =
      this.employeeService.addEmployeeToCollectionIfMissing(
        this.employeesSharedCollection,
        job.employee
      );
  }

  protected loadRelationshipsOptions(): void {
    this.taskItemService
      .query()
      .pipe(map((res: HttpResponse<ITaskItem[]>) => res.body ?? []))
      .pipe(
        map((taskItems: ITaskItem[]) =>
          this.taskItemService.addTaskItemToCollectionIfMissing(
            taskItems,
            ...(this.editForm.get("tasks")!.value ?? [])
          )
        )
      )
      .subscribe(
        (taskItems: ITaskItem[]) => (this.taskItemsSharedCollection = taskItems)
      );

    this.employeeService
      .query()
      .pipe(map((res: HttpResponse<IEmployee[]>) => res.body ?? []))
      .pipe(
        map((employees: IEmployee[]) =>
          this.employeeService.addEmployeeToCollectionIfMissing(
            employees,
            this.editForm.get("employee")!.value
          )
        )
      )
      .subscribe(
        (employees: IEmployee[]) => (this.employeesSharedCollection = employees)
      );
  }

  protected createFromForm(): IJob {
    return {
      ...new Job(),
      id: this.editForm.get(["id"])!.value,
      jobTitle: this.editForm.get(["jobTitle"])!.value,
      minSalary: this.editForm.get(["minSalary"])!.value,
      maxSalary: this.editForm.get(["maxSalary"])!.value,
      tasks: this.editForm.get(["tasks"])!.value,
      employee: this.editForm.get(["employee"])!.value,
    };
  }
}
