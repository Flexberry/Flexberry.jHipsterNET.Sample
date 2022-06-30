import { Component, OnInit } from "@angular/core";
import { HttpResponse } from "@angular/common/http";
import { FormBuilder } from "@angular/forms";
import { ActivatedRoute } from "@angular/router";
import { Observable } from "rxjs";
import { finalize } from "rxjs/operators";

import { ITaskItem, TaskItem } from "../task-item.model";
import { TaskItemService } from "../service/task-item.service";

@Component({
  selector: "jhi-task-item-update",
  templateUrl: "./task-item-update.component.html",
})
export class TaskItemUpdateComponent implements OnInit {
  isSaving = false;

  editForm = this.fb.group({
    id: [],
    title: [],
    description: [],
  });

  constructor(
    protected taskItemService: TaskItemService,
    protected activatedRoute: ActivatedRoute,
    protected fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.activatedRoute.data.subscribe(({ taskItem }) => {
      this.updateForm(taskItem);
    });
  }

  previousState(): void {
    window.history.back();
  }

  save(): void {
    this.isSaving = true;
    const taskItem = this.createFromForm();
    if (taskItem.id !== undefined) {
      this.subscribeToSaveResponse(this.taskItemService.update(taskItem));
    } else {
      this.subscribeToSaveResponse(this.taskItemService.create(taskItem));
    }
  }

  protected subscribeToSaveResponse(
    result: Observable<HttpResponse<ITaskItem>>
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

  protected updateForm(taskItem: ITaskItem): void {
    this.editForm.patchValue({
      id: taskItem.id,
      title: taskItem.title,
      description: taskItem.description,
    });
  }

  protected createFromForm(): ITaskItem {
    return {
      ...new TaskItem(),
      id: this.editForm.get(["id"])!.value,
      title: this.editForm.get(["title"])!.value,
      description: this.editForm.get(["description"])!.value,
    };
  }
}
