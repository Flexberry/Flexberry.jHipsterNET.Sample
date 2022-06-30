import { TestBed } from "@angular/core/testing";
import {
  HttpClientTestingModule,
  HttpTestingController,
} from "@angular/common/http/testing";

import { ITaskItem, TaskItem } from "../task-item.model";

import { TaskItemService } from "./task-item.service";

describe("TaskItem Service", () => {
  let service: TaskItemService;
  let httpMock: HttpTestingController;
  let elemDefault: ITaskItem;
  let expectedResult: ITaskItem | ITaskItem[] | boolean | null;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
    });
    expectedResult = null;
    service = TestBed.inject(TaskItemService);
    httpMock = TestBed.inject(HttpTestingController);

    elemDefault = {
      id: 0,
      title: "AAAAAAA",
      description: "AAAAAAA",
    };
  });

  describe("Service methods", () => {
    it("should find an element", () => {
      const returnedFromService = Object.assign({}, elemDefault);

      service.find(123).subscribe((resp) => (expectedResult = resp.body));

      const req = httpMock.expectOne({ method: "GET" });
      req.flush(returnedFromService);
      expect(expectedResult).toMatchObject(elemDefault);
    });

    it("should create a TaskItem", () => {
      const returnedFromService = Object.assign(
        {
          id: 0,
        },
        elemDefault
      );

      const expected = Object.assign({}, returnedFromService);

      service
        .create(new TaskItem())
        .subscribe((resp) => (expectedResult = resp.body));

      const req = httpMock.expectOne({ method: "POST" });
      req.flush(returnedFromService);
      expect(expectedResult).toMatchObject(expected);
    });

    it("should update a TaskItem", () => {
      const returnedFromService = Object.assign(
        {
          id: 1,
          title: "BBBBBB",
          description: "BBBBBB",
        },
        elemDefault
      );

      const expected = Object.assign({}, returnedFromService);

      service
        .update(expected)
        .subscribe((resp) => (expectedResult = resp.body));

      const req = httpMock.expectOne({ method: "PUT" });
      req.flush(returnedFromService);
      expect(expectedResult).toMatchObject(expected);
    });

    it("should partial update a TaskItem", () => {
      const patchObject = Object.assign(
        {
          title: "BBBBBB",
        },
        new TaskItem()
      );

      const returnedFromService = Object.assign(patchObject, elemDefault);

      const expected = Object.assign({}, returnedFromService);

      service
        .partialUpdate(patchObject)
        .subscribe((resp) => (expectedResult = resp.body));

      const req = httpMock.expectOne({ method: "PATCH" });
      req.flush(returnedFromService);
      expect(expectedResult).toMatchObject(expected);
    });

    it("should return a list of TaskItem", () => {
      const returnedFromService = Object.assign(
        {
          id: 1,
          title: "BBBBBB",
          description: "BBBBBB",
        },
        elemDefault
      );

      const expected = Object.assign({}, returnedFromService);

      service.query().subscribe((resp) => (expectedResult = resp.body));

      const req = httpMock.expectOne({ method: "GET" });
      req.flush([returnedFromService]);
      httpMock.verify();
      expect(expectedResult).toContainEqual(expected);
    });

    it("should delete a TaskItem", () => {
      service.delete(123).subscribe((resp) => (expectedResult = resp.ok));

      const req = httpMock.expectOne({ method: "DELETE" });
      req.flush({ status: 200 });
      expect(expectedResult);
    });

    describe("addTaskItemToCollectionIfMissing", () => {
      it("should add a TaskItem to an empty array", () => {
        const taskItem: ITaskItem = { id: 123 };
        expectedResult = service.addTaskItemToCollectionIfMissing([], taskItem);
        expect(expectedResult).toHaveLength(1);
        expect(expectedResult).toContain(taskItem);
      });

      it("should not add a TaskItem to an array that contains it", () => {
        const taskItem: ITaskItem = { id: 123 };
        const taskItemCollection: ITaskItem[] = [
          {
            ...taskItem,
          },
          { id: 456 },
        ];
        expectedResult = service.addTaskItemToCollectionIfMissing(
          taskItemCollection,
          taskItem
        );
        expect(expectedResult).toHaveLength(2);
      });

      it("should add a TaskItem to an array that doesn't contain it", () => {
        const taskItem: ITaskItem = { id: 123 };
        const taskItemCollection: ITaskItem[] = [{ id: 456 }];
        expectedResult = service.addTaskItemToCollectionIfMissing(
          taskItemCollection,
          taskItem
        );
        expect(expectedResult).toHaveLength(2);
        expect(expectedResult).toContain(taskItem);
      });

      it("should add only unique TaskItem to an array", () => {
        const taskItemArray: ITaskItem[] = [
          { id: 123 },
          { id: 456 },
          { id: 23432 },
        ];
        const taskItemCollection: ITaskItem[] = [{ id: 123 }];
        expectedResult = service.addTaskItemToCollectionIfMissing(
          taskItemCollection,
          ...taskItemArray
        );
        expect(expectedResult).toHaveLength(3);
      });

      it("should accept varargs", () => {
        const taskItem: ITaskItem = { id: 123 };
        const taskItem2: ITaskItem = { id: 456 };
        expectedResult = service.addTaskItemToCollectionIfMissing(
          [],
          taskItem,
          taskItem2
        );
        expect(expectedResult).toHaveLength(2);
        expect(expectedResult).toContain(taskItem);
        expect(expectedResult).toContain(taskItem2);
      });

      it("should accept null and undefined values", () => {
        const taskItem: ITaskItem = { id: 123 };
        expectedResult = service.addTaskItemToCollectionIfMissing(
          [],
          null,
          taskItem,
          undefined
        );
        expect(expectedResult).toHaveLength(1);
        expect(expectedResult).toContain(taskItem);
      });

      it("should return initial array if no TaskItem is added", () => {
        const taskItemCollection: ITaskItem[] = [{ id: 123 }];
        expectedResult = service.addTaskItemToCollectionIfMissing(
          taskItemCollection,
          undefined,
          null
        );
        expect(expectedResult).toEqual(taskItemCollection);
      });
    });
  });

  afterEach(() => {
    httpMock.verify();
  });
});
