import { Component, Input, EventEmitter, OnInit, Output } from '@angular/core';
import { ButtonAction } from './models/buttonAction';
import { Column } from './models/column';
import { ColumnType } from './models/columnTypes';

@Component({
  selector: 'app-generic-table',
  templateUrl: './generic-table.component.html',
  styleUrls: ['./generic-table.component.css']
})
export class GenericTableComponent implements OnInit {

  @Input() columns: Column[];
  @Input() buttonsActions: Map<string, ButtonAction>;
  @Input() dataSource: any[];
  @Input() loading: boolean = true;
  displayedColumns: string[] = [];
  columnsTypes = ColumnType;
  constructor() { }

  ngOnInit(): void {
    this.displayAllColumns();

  }

  displayAllColumns() {
    this.columns.forEach((value: Column) => {
      this.displayedColumns.push(value.property);
    })
  }

}
