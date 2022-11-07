import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AssignmentsTableComponent } from 'src/app/components/assignments-table/assignments-table.component';
import { BugFormComponent } from 'src/app/components/bug-form/bug-form.component';
import { FilterBugsComponent } from 'src/app/components/tester/filter-bugs/filter-bugs.component';
import { TesterComponent } from './tester.component';

const routes: Routes = [
  {
    path: '',
    component: TesterComponent,
    children: [
      {
        path: '',
        redirectTo: 'bugs'
      },
      {
        path: 'bugs',
        component: FilterBugsComponent
      },
      {
        path: 'bug',
        component: BugFormComponent
      },
      {
        path: 'assignments',
        component: AssignmentsTableComponent
      }
    ]
  }
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TesterRoutingModule { }
