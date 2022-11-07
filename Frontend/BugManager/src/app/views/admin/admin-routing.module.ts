import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BugsComponent } from 'src/app/components/bugs/bugs.component';
import { ClassicImportComponent } from 'src/app/components/admin/classic-import/classic-import.component';
import { CreateUserComponent } from 'src/app/components/admin/create-user/create-user.component';
import { CustomImportComponent } from 'src/app/components/admin/custom-import/custom-import.component';
import { DevsScoreboardComponent } from 'src/app/components/admin/devs-scoreboard/devs-scoreboard.component';
import { ProjectsComponent } from 'src/app/components/admin/projects/projects.component';
import { AdminComponent } from './admin.component';
import { BugFormComponent } from 'src/app/components/bug-form/bug-form.component';
import { DevsProjectComponent } from 'src/app/components/admin/devs-project/devs-project.component';
import { CreateProjectComponent } from 'src/app/components/admin/create-project/create-project.component';
import { TestersProjectComponent } from 'src/app/components/admin/tester-project/tester-project.component';
import { AssignmentsTableComponent } from 'src/app/components/assignments-table/assignments-table.component';
import { AssignmentsWithCreateComponent } from 'src/app/components/admin/assignments-with-create/assignments-with-create.component';
import { CreateAssignmentComponent } from 'src/app/components/admin/create-assignment/create-assignment.component';

const routes: Routes = [
  {
    path: '',
    component: AdminComponent,
    children: [
      {
        path: '',
        redirectTo: 'bugs'
      },
      {
        path: 'bugs',
        component: BugsComponent
      },
      {
        path: 'add-user',
        component: CreateUserComponent
      },
      {
        path: 'projects',
        component: ProjectsComponent
      },
      {
        path: 'devs-scoreboard',
        component: DevsScoreboardComponent
      },
      {
        path: 'bug-import-classic',
        component: ClassicImportComponent
      },
      {
        path: 'bug-import-custom',
        component: CustomImportComponent
      }
      ,
      {
        path: 'bug',
        component: BugFormComponent
      },
      {
        path: 'project/devs',
        component: DevsProjectComponent
      },
      {
        path: 'project/testers',
        component: TestersProjectComponent
      },
      {
        path: 'project',
        component: CreateProjectComponent
      },
      {
        path: 'assignments',
        component: AssignmentsWithCreateComponent
      },
      {
        path: 'assignment',
        component: CreateAssignmentComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
