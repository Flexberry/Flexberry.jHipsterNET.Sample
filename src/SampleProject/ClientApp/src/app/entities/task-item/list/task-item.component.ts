import { Component, OnInit } from "@angular/core";
import { HttpResponse } from "@angular/common/http";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";

import { ITaskItem } from "../task-item.model";
import { TaskItemService } from "../service/task-item.service";
import { TaskItemDeleteDialogComponent } from "../delete/task-item-delete-dialog.component";

@Component({
  selector: "jhi-task-item",
  templateUrl: "./task-item.component.html",
})
export class TaskItemComponent implements OnInit {
  taskItems?: ITaskItem[];
  isLoading = false;

  constructor(
    protected taskItemService: TaskItemService,
    protected modalService: NgbModal
  ) {}

  loadAll(): void {
    this.isLoading = true;

    this.taskItemService.query().subscribe({
      next: (res: HttpResponse<ITaskItem[]>) => {
        this.isLoading = false;
        this.taskItems = res.body ?? [];
      },
      error: () => {
        this.isLoading = false;
      },
    });
  }

  ngOnInit(): void {
    this.loadAll();
  }

  trackId(_index: number, item: ITaskItem): number {
    return item.id!;
  }

  delete(taskItem: ITaskItem): void {
    const modalRef = this.modalService.open(TaskItemDeleteDialogComponent, {
      size: "lg",
      backdrop: "static",
    });
    modalRef.componentInstance.taskItem = taskItem;
    // unsubscribe not needed because closed completes on modal close
    modalRef.closed.subscribe((reason) => {
      if (reason === "deleted") {
        this.loadAll();
      }
    });
  }
}
