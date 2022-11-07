import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { GenericTableComponent } from './generic-table/generic-table.component';
import { MessageComponent } from './message/message.component';
import { AngularMaterialModule } from './angular-material/angular-material.module';
import { BugsTableComponent } from './bugs-table/bugs-table.component';
import { BugsComponent } from './bugs/bugs.component';
import { ProjectsTableComponent } from './projects-table/projects-table.component';
import { ProjectsComponent } from './admin/projects/projects.component';
import { DevsScoreboardComponent } from './admin/devs-scoreboard/devs-scoreboard.component';
import { GenericFormComponent } from './generic-form/generic-form.component';
import { CreateUserComponent } from './admin/create-user/create-user.component';
import { ClassicImportComponent } from './admin/classic-import/classic-import.component';
import { CustomImportComponent } from './admin/custom-import/custom-import.component';
import { FilterBugsComponent } from './tester/filter-bugs/filter-bugs.component';
import { NavComponent } from './nav/nav.component';
import { EditBugsComponent } from './edit-bugs/edit-bugs.component';
import { BugFormComponent } from './bug-form/bug-form.component';
import { DeleteDialogComponent } from './delete-dialog/delete-dialog.component';
import { DevsProjectComponent } from './admin/devs-project/devs-project.component';
import { CreateProjectComponent } from './admin/create-project/create-project.component';
import { ResolveBugsComponent } from './dev/resolve-bugs/resolve-bugs.component';
import { TestersProjectComponent } from './admin/tester-project/tester-project.component';
import { AssignmentsTableComponent } from './assignments-table/assignments-table.component';
import { AssignmentsWithCreateComponent } from './admin/assignments-with-create/assignments-with-create.component';
import { CreateAssignmentComponent } from './admin/create-assignment/create-assignment.component';
import { BugClassifyFormComponent } from './bug-classify-form/bug-classify-form.component';

@NgModule({
  imports: [CommonModule, RouterModule, NgbModule, AngularMaterialModule],
  declarations: [
    GenericTableComponent,
    MessageComponent,
    BugsTableComponent,
    BugsComponent,
    ProjectsTableComponent,
    ProjectsComponent,
    DevsScoreboardComponent,
    GenericFormComponent,
    CreateUserComponent,
    ClassicImportComponent,
    CustomImportComponent,
    FilterBugsComponent,
    NavComponent,
    EditBugsComponent,
    BugFormComponent,
    DeleteDialogComponent,
    TestersProjectComponent,
    DevsProjectComponent,
    CreateProjectComponent,
    ResolveBugsComponent,
    AssignmentsTableComponent,
    AssignmentsWithCreateComponent,
    CreateAssignmentComponent,
    BugClassifyFormComponent,
  ],
  exports: [
    MessageComponent,
    GenericFormComponent,
    GenericTableComponent,
    DeleteDialogComponent,
    NavComponent,
  ],
})
export class ComponentsModule {}
