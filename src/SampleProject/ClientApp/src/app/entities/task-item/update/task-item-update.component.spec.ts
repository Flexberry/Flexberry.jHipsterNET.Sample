import { ComponentFixture, TestBed } from "@angular/core/testing";
import { HttpResponse } from "@angular/common/http";
import { HttpClientTestingModule } from "@angular/common/http/testing";
import { FormBuilder } from "@angular/forms";
import { ActivatedRoute } from "@angular/router";
import { RouterTestingModule } from "@angular/router/testing";
import { of, Subject, from } from "rxjs";

import { TaskItemService } from "../service/task-item.service";
import { ITaskItem, TaskItem } from "../task-item.model";

import { TaskItemUpdateComponent } from "./task-item-update.component";

describe("TaskItem Management Update Component", () => {
  let comp: TaskItemUpdateComponent;
  let fixture: ComponentFixture<TaskItemUpdateComponent>;
  let activatedRoute: ActivatedRoute;
  let taskItemService: TaskItemService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule, RouterTestingModule.withRoutes([])],
      declarations: [TaskItemUpdateComponent],
      providers: [
        FormBuilder,
        {
          provide: ActivatedRoute,
          useValue: {
            params: from([{}]),
          },
        },
      ],
    })
      .overrideTemplate(TaskItemUpdateComponent, "")
      .compileComponents();

    fixture = TestBed.createComponent(TaskItemUpdateComponent);
    activatedRoute = TestBed.inject(ActivatedRoute);
    taskItemService = TestBed.inject(TaskItemService);

    comp = fixture.componentInstance;
  });

  describe("ngOnInit", () => {
    it("Should update editForm", () => {
      const taskItem: ITaskItem = { id: 456 };

      activatedRoute.data = of({ taskItem });
      comp.ngOnInit();

      expect(comp.editForm.value).toEqual(expect.objectContaining(taskItem));
    });
  });

  describe("save", () => {
    it("Should call update service on save for existing entity", () => {
      // GIVEN
      const saveSubject = new Subject<HttpResponse<TaskItem>>();
      const taskItem = { id: 123 };
      jest.spyOn(taskItemService, "update").mockReturnValue(saveSubject);
      jest.spyOn(comp, "previousState");
      activatedRoute.data = of({ taskItem });
      comp.ngOnInit();

      // WHEN
      comp.save();
      expect(comp.isSaving).toEqual(true);
      saveSubject.next(new HttpResponse({ body: taskItem }));
      saveSubject.complete();

      // THEN
      expect(comp.previousState).toHaveBeenCalled();
      expect(taskItemService.update).toHaveBeenCalledWith(taskItem);
      expect(comp.isSaving).toEqual(false);
    });

    it("Should call create service on save for new entity", () => {
      // GIVEN
      const saveSubject = new Subject<HttpResponse<TaskItem>>();
      const taskItem = new TaskItem();
      jest.spyOn(taskItemService, "create").mockReturnValue(saveSubject);
      jest.spyOn(comp, "previousState");
      activatedRoute.data = of({ taskItem });
      comp.ngOnInit();

      // WHEN
      comp.save();
      expect(comp.isSaving).toEqual(true);
      saveSubject.next(new HttpResponse({ body: taskItem }));
      saveSubject.complete();

      // THEN
      expect(taskItemService.create).toHaveBeenCalledWith(taskItem);
      expect(comp.isSaving).toEqual(false);
      expect(comp.previousState).toHaveBeenCalled();
    });

    it("Should set isSaving to false on error", () => {
      // GIVEN
      const saveSubject = new Subject<HttpResponse<TaskItem>>();
      const taskItem = { id: 123 };
      jest.spyOn(taskItemService, "update").mockReturnValue(saveSubject);
      jest.spyOn(comp, "previousState");
      activatedRoute.data = of({ taskItem });
      comp.ngOnInit();

      // WHEN
      comp.save();
      expect(comp.isSaving).toEqual(true);
      saveSubject.error("This is an error!");

      // THEN
      expect(taskItemService.update).toHaveBeenCalledWith(taskItem);
      expect(comp.isSaving).toEqual(false);
      expect(comp.previousState).not.toHaveBeenCalled();
    });
  });
});
