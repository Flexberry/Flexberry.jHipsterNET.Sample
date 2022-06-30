import { ComponentFixture, TestBed } from "@angular/core/testing";
import { ActivatedRoute } from "@angular/router";
import { of } from "rxjs";

import { TaskItemDetailComponent } from "./task-item-detail.component";

describe("TaskItem Management Detail Component", () => {
  let comp: TaskItemDetailComponent;
  let fixture: ComponentFixture<TaskItemDetailComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TaskItemDetailComponent],
      providers: [
        {
          provide: ActivatedRoute,
          useValue: { data: of({ taskItem: { id: 123 } }) },
        },
      ],
    })
      .overrideTemplate(TaskItemDetailComponent, "")
      .compileComponents();
    fixture = TestBed.createComponent(TaskItemDetailComponent);
    comp = fixture.componentInstance;
  });

  describe("OnInit", () => {
    it("Should load taskItem on init", () => {
      // WHEN
      comp.ngOnInit();

      // THEN
      expect(comp.taskItem).toEqual(expect.objectContaining({ id: 123 }));
    });
  });
});
