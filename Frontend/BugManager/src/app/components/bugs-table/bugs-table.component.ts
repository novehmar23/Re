import { Component, Input, OnInit, EventEmitter, Output } from '@angular/core';
import { Bug } from 'src/app/models/Bug';
import { BugsService } from 'src/app/services/bug.service';
import { Display } from 'src/app/utils/display';
import { ButtonAction } from '../generic-table/models/buttonAction';
import { Column } from '../generic-table/models/column';
import { ColumnType } from '../generic-table/models/columnTypes';

@Component({
  selector: 'app-bugs-table',
  templateUrl: './bugs-table.component.html',
  styleUrls: ['./bugs-table.component.css']
})
export class BugsTableComponent implements OnInit {

  @Input() dataSource: Bug[];
  @Input() buttonsColumns: Column[];
  @Input() buttonsActions: Map<string, ButtonAction>;
  @Output() sendBugs = new EventEmitter();

  loading: boolean;
  bugsColumn: Column[] = [
    { header: "No.", property: "id", display: Display.id, type: ColumnType.Object },
    { header: "Name", property: "name", display: Display.id, type: ColumnType.Object },
    { header: "Description", property: "description", display: Display.id, type: ColumnType.Object },
    { header: "Project", property: "projectName", display: Display.id, type: ColumnType.Object },
    { header: "Version", property: "version", display: Display.id, type: ColumnType.Object },
    { header: "Time", property: "time", display: Display.id, type: ColumnType.Object },
    { header: "Status", property: "isActive", display: Display.IsActiveAsResolve, type: ColumnType.Object },
    { header: "Completed By", property: "completedByUsername", display: Display.NullableString, type: ColumnType.Object },
  ]

  constructor(private bugsServices: BugsService) { }

  ngOnInit(): void {
    this.loading = true;
    this.declareButtonsInHeader();
    this.bugsServices.getBugs().subscribe(

      (response: Bug[]) => {
        this.dataSource = response;
        this.sendBugs.emit(response);
        this.loading = false;
      },

      error => {
        this.loading = false;
      }
    );;
  }

  declareButtonsInHeader() {
    this.buttonsColumns.forEach((column: Column) => {
      this.bugsColumn.push(column);
    })
  }


}
