import { Component, OnInit } from '@angular/core';
import { Assignment } from 'src/app/models/Assignment';
import { AssignmentService } from 'src/app/services/assignment.service';
import { Display } from 'src/app/utils/display';
import { Column } from '../generic-table/models/column';
import { ColumnType } from '../generic-table/models/columnTypes';

@Component({
  selector: 'app-assignments-table',
  templateUrl: './assignments-table.component.html',
  styleUrls: ['./assignments-table.component.css']
})
export class AssignmentsTableComponent implements OnInit {

  dataSource: Assignment[];
  buttonsColumns: Column[];
  loading: boolean = false;

  assignmentColumn: Column[] = [
    { header: "No.", property: "id", display: Display.id, type: ColumnType.Object },
    { header: "Name", property: "name", display: Display.id, type: ColumnType.Object },
    { header: "Cost", property: "cost", display: Display.id, type: ColumnType.Object },
    { header: "Time", property: "time", display: Display.id, type: ColumnType.Object },
    { header: "Project name", property: "projectName", display: Display.id, type: ColumnType.Object },
  ]
  constructor(private assignmentServices: AssignmentService) { }

  ngOnInit(): void {
    this.loading = true;
    this.assignmentServices.getAssignments().subscribe(

      (response: Assignment[]) => {
        this.dataSource = response;
        this.loading = false;
      },

      error => {
        this.loading = false;
      }
    );;
  }


}
