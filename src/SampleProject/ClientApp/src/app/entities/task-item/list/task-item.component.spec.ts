import { ComponentFixture, TestBed } from "@angular/core/testing";
import { HttpHeaders, HttpResponse } from "@angular/common/http";
import { HttpClientTestingModule } from "@angular/common/http/testing";
import { of } from "rxjs";

import { TaskItemService } from "../service/task-item.service";

import { TaskItemComponent } from "./task-item.component";

describe("TaskItem Management Component", () => {
  let comp: TaskItemComponent;
  let fixture: ComponentFixture<TaskItemComponent>;
  let service: TaskItemService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      declarations: [TaskItemComponent],
    })
      .overrideTemplate(TaskItemComponent, "")
      .compileComponents();

    fixture = TestBed.createComponent(TaskItemComponent);
    comp = fixture.componentInstance;
    service = TestBed.inject(TaskItemService);

    const headers = new HttpHeaders();
    jest.spyOn(service, "query").mockReturnValue(
      of(
        new HttpResponse({
          body: [{ id: 123 }],
          headers,
        })
      )
    );
  });

  it("Should call load all on init", () => {
    // WHEN
    comp.ngOnInit();

    // THEN
    expect(service.query).toHaveBeenCalled();
    expect(comp.taskItems?.[0]).toEqual(expect.objectContaining({ id: 123 }));
  });
});
