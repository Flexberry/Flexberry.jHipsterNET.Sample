import { Injectable } from "@angular/core";
import { HttpResponse } from "@angular/common/http";
import { Resolve, ActivatedRouteSnapshot, Router } from "@angular/router";
import { Observable, of, EMPTY } from "rxjs";
import { mergeMap } from "rxjs/operators";

import { ITaskItem, TaskItem } from "../task-item.model";
import { TaskItemService } from "../service/task-item.service";

@Injectable({ providedIn: "root" })
export class TaskItemRoutingResolveService implements Resolve<ITaskItem> {
  constructor(protected service: TaskItemService, protected router: Router) {}

  resolve(
    route: ActivatedRouteSnapshot
  ): Observable<ITaskItem> | Observable<never> {
    const id = route.params["id"];
    if (id) {
      return this.service.find(id).pipe(
        mergeMap((taskItem: HttpResponse<TaskItem>) => {
          if (taskItem.body) {
            return of(taskItem.body);
          } else {
            this.router.navigate(["404"]);
            return EMPTY;
          }
        })
      );
    }
    return of(new TaskItem());
  }
}
