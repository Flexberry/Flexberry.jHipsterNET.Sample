import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import { UserRouteAccessService } from "app/core/auth/user-route-access.service";
import { TaskItemComponent } from "../list/task-item.component";
import { TaskItemDetailComponent } from "../detail/task-item-detail.component";
import { TaskItemUpdateComponent } from "../update/task-item-update.component";
import { TaskItemRoutingResolveService } from "./task-item-routing-resolve.service";

const taskItemRoute: Routes = [
  {
    path: "",
    component: TaskItemComponent,
    canActivate: [UserRouteAccessService],
  },
  {
    path: ":id/view",
    component: TaskItemDetailComponent,
    resolve: {
      taskItem: TaskItemRoutingResolveService,
    },
    canActivate: [UserRouteAccessService],
  },
  {
    path: "new",
    component: TaskItemUpdateComponent,
    resolve: {
      taskItem: TaskItemRoutingResolveService,
    },
    canActivate: [UserRouteAccessService],
  },
  {
    path: ":id/edit",
    component: TaskItemUpdateComponent,
    resolve: {
      taskItem: TaskItemRoutingResolveService,
    },
    canActivate: [UserRouteAccessService],
  },
];

@NgModule({
  imports: [RouterModule.forChild(taskItemRoute)],
  exports: [RouterModule],
})
export class TaskItemRoutingModule {}
