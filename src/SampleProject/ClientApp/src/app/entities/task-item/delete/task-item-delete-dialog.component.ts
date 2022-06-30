import { Component } from "@angular/core";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";

import { ITaskItem } from "../task-item.model";
import { TaskItemService } from "../service/task-item.service";

@Component({
  templateUrl: "./task-item-delete-dialog.component.html",
})
export class TaskItemDeleteDialogComponent {
  taskItem?: ITaskItem;

  constructor(
    protected taskItemService: TaskItemService,
    protected activeModal: NgbActiveModal
  ) {}

  cancel(): void {
    this.activeModal.dismiss();
  }

  confirmDelete(id: number): void {
    this.taskItemService.delete(id).subscribe(() => {
      this.activeModal.close("deleted");
    });
  }
}
