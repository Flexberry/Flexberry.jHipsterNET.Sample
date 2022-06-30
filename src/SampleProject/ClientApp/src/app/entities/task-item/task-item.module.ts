import { NgModule } from "@angular/core";
import { SharedModule } from "app/shared/shared.module";
import { TaskItemComponent } from "./list/task-item.component";
import { TaskItemDetailComponent } from "./detail/task-item-detail.component";
import { TaskItemUpdateComponent } from "./update/task-item-update.component";
import { TaskItemDeleteDialogComponent } from "./delete/task-item-delete-dialog.component";
import { TaskItemRoutingModule } from "./route/task-item-routing.module";

@NgModule({
  imports: [SharedModule, TaskItemRoutingModule],
  declarations: [
    TaskItemComponent,
    TaskItemDetailComponent,
    TaskItemUpdateComponent,
    TaskItemDeleteDialogComponent,
  ],
  entryComponents: [TaskItemDeleteDialogComponent],
})
export class TaskItemModule {}
