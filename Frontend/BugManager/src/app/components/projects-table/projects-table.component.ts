import { Component, Input, OnInit } from '@angular/core';
import { Project } from 'src/app/models/Project';
import { Display } from 'src/app/utils/display';
import { ButtonAction } from '../generic-table/models/buttonAction';
import { Column } from '../generic-table/models/column';
import { ColumnType } from '../generic-table/models/columnTypes';

@Component({
  selector: 'app-projects-table',
  templateUrl: './projects-table.component.html',
  styleUrls: ['./projects-table.component.css']
})
export class ProjectsTableComponent implements OnInit {

  @Input() dataSource: Project[];
  @Input() loading: boolean;
  @Input() buttonsColumns: Column[];
  @Input() buttonsActions: Map<string, ButtonAction>;


  columns: Column[] = [
    { header: "Nº", property: "id", display: Display.id, type: ColumnType.Object },
    { header: "Name", property: "name", display: Display.id, type: ColumnType.Object },
    { header: "Total cost", property: "totalCost", display: Display.TotalCost, type: ColumnType.Object },
    { header: "Total duration (hr.)", property: "totalDuration", display: Display.id, type: ColumnType.Object },
    { header: "Total bugs quantity", property: "bugsQuantity", display: Display.id, type: ColumnType.Object },
  ]

  constructor() { }

  ngOnInit(): void {
    this.declareButtonsInHeader();
  }

  declareButtonsInHeader() {
    this.buttonsColumns.forEach((column: Column) => {
      this.columns.push(column);
    })
  }

}
