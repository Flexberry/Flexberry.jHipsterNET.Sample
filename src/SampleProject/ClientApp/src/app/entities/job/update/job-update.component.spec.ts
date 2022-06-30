import { ComponentFixture, TestBed } from "@angular/core/testing";
import { HttpResponse } from "@angular/common/http";
import { HttpClientTestingModule } from "@angular/common/http/testing";
import { FormBuilder } from "@angular/forms";
import { ActivatedRoute } from "@angular/router";
import { RouterTestingModule } from "@angular/router/testing";
import { of, Subject, from } from "rxjs";

import { JobService } from "../service/job.service";
import { IJob, Job } from "../job.model";
import { ITaskItem } from "app/entities/task-item/task-item.model";
import { TaskItemService } from "app/entities/task-item/service/task-item.service";
import { IEmployee } from "app/entities/employee/employee.model";
import { EmployeeService } from "app/entities/employee/service/employee.service";

import { JobUpdateComponent } from "./job-update.component";

describe("Job Management Update Component", () => {
  let comp: JobUpdateComponent;
  let fixture: ComponentFixture<JobUpdateComponent>;
  let activatedRoute: ActivatedRoute;
  let jobService: JobService;
  let taskItemService: TaskItemService;
  let employeeService: EmployeeService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule, RouterTestingModule.withRoutes([])],
      declarations: [JobUpdateComponent],
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
      .overrideTemplate(JobUpdateComponent, "")
      .compileComponents();

    fixture = TestBed.createComponent(JobUpdateComponent);
    activatedRoute = TestBed.inject(ActivatedRoute);
    jobService = TestBed.inject(JobService);
    taskItemService = TestBed.inject(TaskItemService);
    employeeService = TestBed.inject(EmployeeService);

    comp = fixture.componentInstance;
  });

  describe("ngOnInit", () => {
    it("Should call TaskItem query and add missing value", () => {
      const job: IJob = { id: 456 };
      const tasks: ITaskItem[] = [{ id: 54681 }];
      job.tasks = tasks;

      const taskItemCollection: ITaskItem[] = [{ id: 87383 }];
      jest
        .spyOn(taskItemService, "query")
        .mockReturnValue(of(new HttpResponse({ body: taskItemCollection })));
      const additionalTaskItems = [...tasks];
      const expectedCollection: ITaskItem[] = [
        ...additionalTaskItems,
        ...taskItemCollection,
      ];
      jest
        .spyOn(taskItemService, "addTaskItemToCollectionIfMissing")
        .mockReturnValue(expectedCollection);

      activatedRoute.data = of({ job });
      comp.ngOnInit();

      expect(taskItemService.query).toHaveBeenCalled();
      expect(
        taskItemService.addTaskItemToCollectionIfMissing
      ).toHaveBeenCalledWith(taskItemCollection, ...additionalTaskItems);
      expect(comp.taskItemsSharedCollection).toEqual(expectedCollection);
    });

    it("Should call Employee query and add missing value", () => {
      const job: IJob = { id: 456 };
      const employee: IEmployee = { id: 75146 };
      job.employee = employee;

      const employeeCollection: IEmployee[] = [{ id: 78248 }];
      jest
        .spyOn(employeeService, "query")
        .mockReturnValue(of(new HttpResponse({ body: employeeCollection })));
      const additionalEmployees = [employee];
      const expectedCollection: IEmployee[] = [
        ...additionalEmployees,
        ...employeeCollection,
      ];
      jest
        .spyOn(employeeService, "addEmployeeToCollectionIfMissing")
        .mockReturnValue(expectedCollection);

      activatedRoute.data = of({ job });
      comp.ngOnInit();

      expect(employeeService.query).toHaveBeenCalled();
      expect(
        employeeService.addEmployeeToCollectionIfMissing
      ).toHaveBeenCalledWith(employeeCollection, ...additionalEmployees);
      expect(comp.employeesSharedCollection).toEqual(expectedCollection);
    });

    it("Should update editForm", () => {
      const job: IJob = { id: 456 };
      const tasks: ITaskItem = { id: 75047 };
      job.tasks = [tasks];
      const employee: IEmployee = { id: 21743 };
      job.employee = employee;

      activatedRoute.data = of({ job });
      comp.ngOnInit();

      expect(comp.editForm.value).toEqual(expect.objectContaining(job));
      expect(comp.taskItemsSharedCollection).toContain(tasks);
      expect(comp.employeesSharedCollection).toContain(employee);
    });
  });

  describe("save", () => {
    it("Should call update service on save for existing entity", () => {
      // GIVEN
      const saveSubject = new Subject<HttpResponse<Job>>();
      const job = { id: 123 };
      jest.spyOn(jobService, "update").mockReturnValue(saveSubject);
      jest.spyOn(comp, "previousState");
      activatedRoute.data = of({ job });
      comp.ngOnInit();

      // WHEN
      comp.save();
      expect(comp.isSaving).toEqual(true);
      saveSubject.next(new HttpResponse({ body: job }));
      saveSubject.complete();

      // THEN
      expect(comp.previousState).toHaveBeenCalled();
      expect(jobService.update).toHaveBeenCalledWith(job);
      expect(comp.isSaving).toEqual(false);
    });

    it("Should call create service on save for new entity", () => {
      // GIVEN
      const saveSubject = new Subject<HttpResponse<Job>>();
      const job = new Job();
      jest.spyOn(jobService, "create").mockReturnValue(saveSubject);
      jest.spyOn(comp, "previousState");
      activatedRoute.data = of({ job });
      comp.ngOnInit();

      // WHEN
      comp.save();
      expect(comp.isSaving).toEqual(true);
      saveSubject.next(new HttpResponse({ body: job }));
      saveSubject.complete();

      // THEN
      expect(jobService.create).toHaveBeenCalledWith(job);
      expect(comp.isSaving).toEqual(false);
      expect(comp.previousState).toHaveBeenCalled();
    });

    it("Should set isSaving to false on error", () => {
      // GIVEN
      const saveSubject = new Subject<HttpResponse<Job>>();
      const job = { id: 123 };
      jest.spyOn(jobService, "update").mockReturnValue(saveSubject);
      jest.spyOn(comp, "previousState");
      activatedRoute.data = of({ job });
      comp.ngOnInit();

      // WHEN
      comp.save();
      expect(comp.isSaving).toEqual(true);
      saveSubject.error("This is an error!");

      // THEN
      expect(jobService.update).toHaveBeenCalledWith(job);
      expect(comp.isSaving).toEqual(false);
      expect(comp.previousState).not.toHaveBeenCalled();
    });
  });

  describe("Tracking relationships identifiers", () => {
    describe("trackTaskItemById", () => {
      it("Should return tracked TaskItem primary key", () => {
        const entity = { id: 123 };
        const trackResult = comp.trackTaskItemById(0, entity);
        expect(trackResult).toEqual(entity.id);
      });
    });

    describe("trackEmployeeById", () => {
      it("Should return tracked Employee primary key", () => {
        const entity = { id: 123 };
        const trackResult = comp.trackEmployeeById(0, entity);
        expect(trackResult).toEqual(entity.id);
      });
    });
  });

  describe("Getting selected relationships", () => {
    describe("getSelectedTaskItem", () => {
      it("Should return option if no TaskItem is selected", () => {
        const option = { id: 123 };
        const result = comp.getSelectedTaskItem(option);
        expect(result === option).toEqual(true);
      });

      it("Should return selected TaskItem for according option", () => {
        const option = { id: 123 };
        const selected = { id: 123 };
        const selected2 = { id: 456 };
        const result = comp.getSelectedTaskItem(option, [selected2, selected]);
        expect(result === selected).toEqual(true);
        expect(result === selected2).toEqual(false);
        expect(result === option).toEqual(false);
      });

      it("Should return option if this TaskItem is not selected", () => {
        const option = { id: 123 };
        const selected = { id: 456 };
        const result = comp.getSelectedTaskItem(option, [selected]);
        expect(result === option).toEqual(true);
        expect(result === selected).toEqual(false);
      });
    });
  });
});
