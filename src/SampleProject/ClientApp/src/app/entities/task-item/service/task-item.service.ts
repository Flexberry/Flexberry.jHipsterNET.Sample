import { Injectable } from "@angular/core";
import { HttpClient, HttpResponse } from "@angular/common/http";
import { Observable } from "rxjs";

import { isPresent } from "app/core/util/operators";
import { ApplicationConfigService } from "app/core/config/application-config.service";
import { createRequestOption } from "app/core/request/request-util";
import { ITaskItem, getTaskItemIdentifier } from "../task-item.model";

export type EntityResponseType = HttpResponse<ITaskItem>;
export type EntityArrayResponseType = HttpResponse<ITaskItem[]>;

@Injectable({ providedIn: "root" })
export class TaskItemService {
  protected resourceUrl =
    this.applicationConfigService.getEndpointFor("api/task-items");

  constructor(
    protected http: HttpClient,
    protected applicationConfigService: ApplicationConfigService
  ) {}

  create(taskItem: ITaskItem): Observable<EntityResponseType> {
    return this.http.post<ITaskItem>(this.resourceUrl, taskItem, {
      observe: "response",
    });
  }

  update(taskItem: ITaskItem): Observable<EntityResponseType> {
    return this.http.put<ITaskItem>(
      `${this.resourceUrl}/${getTaskItemIdentifier(taskItem) as number}`,
      taskItem,
      { observe: "response" }
    );
  }

  partialUpdate(taskItem: ITaskItem): Observable<EntityResponseType> {
    return this.http.patch<ITaskItem>(
      `${this.resourceUrl}/${getTaskItemIdentifier(taskItem) as number}`,
      taskItem,
      { observe: "response" }
    );
  }

  find(id: number): Observable<EntityResponseType> {
    return this.http.get<ITaskItem>(`${this.resourceUrl}/${id}`, {
      observe: "response",
    });
  }

  query(req?: any): Observable<EntityArrayResponseType> {
    const options = createRequestOption(req);
    return this.http.get<ITaskItem[]>(this.resourceUrl, {
      params: options,
      observe: "response",
    });
  }

  delete(id: number): Observable<HttpResponse<{}>> {
    return this.http.delete(`${this.resourceUrl}/${id}`, {
      observe: "response",
    });
  }

  addTaskItemToCollectionIfMissing(
    taskItemCollection: ITaskItem[],
    ...taskItemsToCheck: (ITaskItem | null | undefined)[]
  ): ITaskItem[] {
    const taskItems: ITaskItem[] = taskItemsToCheck.filter(isPresent);
    if (taskItems.length > 0) {
      const taskItemCollectionIdentifiers = taskItemCollection.map(
        (taskItemItem) => getTaskItemIdentifier(taskItemItem)!
      );
      const taskItemsToAdd = taskItems.filter((taskItemItem) => {
        const taskItemIdentifier = getTaskItemIdentifier(taskItemItem);
        if (
          taskItemIdentifier == null ||
          taskItemCollectionIdentifiers.includes(taskItemIdentifier)
        ) {
          return false;
        }
        taskItemCollectionIdentifiers.push(taskItemIdentifier);
        return true;
      });
      return [...taskItemsToAdd, ...taskItemCollection];
    }
    return taskItemCollection;
  }
}
