import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";

import { ITaskItem } from "../task-item.model";

@Component({
  selector: "jhi-task-item-detail",
  templateUrl: "./task-item-detail.component.html",
})
export class TaskItemDetailComponent implements OnInit {
  taskItem: ITaskItem | null = null;

  constructor(protected activatedRoute: ActivatedRoute) {}

  ngOnInit(): void {
    this.activatedRoute.data.subscribe(({ taskItem }) => {
      this.taskItem = taskItem;
    });
  }

  previousState(): void {
    window.history.back();
  }
}
