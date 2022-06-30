import { entityItemSelector } from "../../support/commands";
import {
  entityTableSelector,
  entityDetailsButtonSelector,
  entityDetailsBackButtonSelector,
  entityCreateButtonSelector,
  entityCreateSaveButtonSelector,
  entityCreateCancelButtonSelector,
  entityEditButtonSelector,
  entityDeleteButtonSelector,
  entityConfirmDeleteButtonSelector,
} from "../../support/entity";

describe("TaskItem e2e test", () => {
  const taskItemPageUrl = "/task-item";
  const taskItemPageUrlPattern = new RegExp("/task-item(\\?.*)?$");
  const username = Cypress.env("E2E_USERNAME") ?? "user";
  const password = Cypress.env("E2E_PASSWORD") ?? "user";
  const taskItemSample = {};

  let taskItem: any;

  beforeEach(() => {
    cy.login(username, password);
  });

  beforeEach(() => {
    cy.intercept("GET", "/api/task-items+(?*|)").as("entitiesRequest");
    cy.intercept("POST", "/api/task-items").as("postEntityRequest");
    cy.intercept("DELETE", "/api/task-items/*").as("deleteEntityRequest");
  });

  afterEach(() => {
    if (taskItem) {
      cy.authenticatedRequest({
        method: "DELETE",
        url: `/api/task-items/${taskItem.id}`,
      }).then(() => {
        taskItem = undefined;
      });
    }
  });

  it("TaskItems menu should load TaskItems page", () => {
    cy.visit("/");
    cy.clickOnEntityMenuItem("task-item");
    cy.wait("@entitiesRequest").then(({ response }) => {
      if (response!.body.length === 0) {
        cy.get(entityTableSelector).should("not.exist");
      } else {
        cy.get(entityTableSelector).should("exist");
      }
    });
    cy.getEntityHeading("TaskItem").should("exist");
    cy.url().should("match", taskItemPageUrlPattern);
  });

  describe("TaskItem page", () => {
    describe("create button click", () => {
      beforeEach(() => {
        cy.visit(taskItemPageUrl);
        cy.wait("@entitiesRequest");
      });

      it("should load create TaskItem page", () => {
        cy.get(entityCreateButtonSelector).click();
        cy.url().should("match", new RegExp("/task-item/new$"));
        cy.getEntityCreateUpdateHeading("TaskItem");
        cy.get(entityCreateSaveButtonSelector).should("exist");
        cy.get(entityCreateCancelButtonSelector).click();
        cy.wait("@entitiesRequest").then(({ response }) => {
          expect(response!.statusCode).to.equal(200);
        });
        cy.url().should("match", taskItemPageUrlPattern);
      });
    });

    describe("with existing value", () => {
      beforeEach(() => {
        cy.authenticatedRequest({
          method: "POST",
          url: "/api/task-items",
          body: taskItemSample,
        }).then(({ body }) => {
          taskItem = body;

          cy.intercept(
            {
              method: "GET",
              url: "/api/task-items+(?*|)",
              times: 1,
            },
            {
              statusCode: 200,
              body: [taskItem],
            }
          ).as("entitiesRequestInternal");
        });

        cy.visit(taskItemPageUrl);

        cy.wait("@entitiesRequestInternal");
      });

      it("detail button click should load details TaskItem page", () => {
        cy.get(entityDetailsButtonSelector).first().click();
        cy.getEntityDetailsHeading("taskItem");
        cy.get(entityDetailsBackButtonSelector).click();
        cy.wait("@entitiesRequest").then(({ response }) => {
          expect(response!.statusCode).to.equal(200);
        });
        cy.url().should("match", taskItemPageUrlPattern);
      });

      it("edit button click should load edit TaskItem page", () => {
        cy.get(entityEditButtonSelector).first().click();
        cy.getEntityCreateUpdateHeading("TaskItem");
        cy.get(entityCreateSaveButtonSelector).should("exist");
        cy.get(entityCreateCancelButtonSelector).click();
        cy.wait("@entitiesRequest").then(({ response }) => {
          expect(response!.statusCode).to.equal(200);
        });
        cy.url().should("match", taskItemPageUrlPattern);
      });

      it("last delete button click should delete instance of TaskItem", () => {
        cy.get(entityDeleteButtonSelector).last().click();
        cy.getEntityDeleteDialogHeading("taskItem").should("exist");
        cy.get(entityConfirmDeleteButtonSelector).click();
        cy.wait("@deleteEntityRequest").then(({ response }) => {
          expect(response!.statusCode).to.equal(204);
        });
        cy.wait("@entitiesRequest").then(({ response }) => {
          expect(response!.statusCode).to.equal(200);
        });
        cy.url().should("match", taskItemPageUrlPattern);

        taskItem = undefined;
      });
    });
  });

  describe("new TaskItem page", () => {
    beforeEach(() => {
      cy.visit(`${taskItemPageUrl}`);
      cy.get(entityCreateButtonSelector).click();
      cy.getEntityCreateUpdateHeading("TaskItem");
    });

    it("should create an instance of TaskItem", () => {
      cy.get(`[data-cy="title"]`)
        .type("вычислить frame протокол")
        .should("have.value", "вычислить frame протокол");

      cy.get(`[data-cy="description"]`)
        .type("data-warehouse")
        .should("have.value", "data-warehouse");

      cy.get(entityCreateSaveButtonSelector).click();

      cy.wait("@postEntityRequest").then(({ response }) => {
        expect(response!.statusCode).to.equal(201);
        taskItem = response!.body;
      });
      cy.wait("@entitiesRequest").then(({ response }) => {
        expect(response!.statusCode).to.equal(200);
      });
      cy.url().should("match", taskItemPageUrlPattern);
    });
  });
});
