import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { Bug } from 'src/app/models/Bug';
import { BugsService } from 'src/app/services/bug.service';
import { Display } from 'src/app/utils/display';
import { DeleteDialogComponent } from '../delete-dialog/delete-dialog.component';
import { ButtonAction } from '../generic-table/models/buttonAction';
import { Column } from '../generic-table/models/column';
import { ColumnType } from '../generic-table/models/columnTypes';

@Component({
  selector: 'app-bugs',
  templateUrl: './bugs.component.html',
  styleUrls: ['./bugs.component.css'],
})
export class BugsComponent implements OnInit {
  @Input() dataSource: Bug[];
  @Output() sendBugs = new EventEmitter<any>();

  buttonsColumns: Column[] = [
    {
      header: 'Edit',
      property: 'edit',
      display: Display.id,
      type: ColumnType.Button,
    },
    {
      header: 'Delete',
      property: 'delete',
      display: Display.id,
      type: ColumnType.Button,
    },
    {
      header: 'Classify',
      property: 'classify',
      display: Display.id,
      type: ColumnType.Button,
    },
  ];
  constructor(
    private router: Router,
    private r: ActivatedRoute,
    public dialog: MatDialog,
    private serviceBugs: BugsService
  ) {}

  buttonsActions = new Map<string, ButtonAction>([
    [
      'edit',
      {
        text: () => 'Edit',
        onClick: (b) => {
          this.editBug(b);
        },
        color: () => 'primary',
      },
    ],
    [
      'delete',
      {
        text: () => 'Delete',
        onClick: (b) => {
          this.deleteBug(b);
        },
        color: () => 'warn',
      },
    ],
    [
      'classify',
      {
        text: () => 'Classify',
        onClick: (b) => {
          this.classifyBug(b);
        },
        color: () => 'warn',
      },
    ],
  ]);

  editBug(bug) {
    this.router.navigate(['../bug'], {
      relativeTo: this.r,
      queryParams: { id: String(bug.id) },
    });
  }

  deleteBug(bug) {
    let dialogRef = this.dialog.open(DeleteDialogComponent);
    dialogRef.afterClosed().subscribe((result) => {
      if (!result) return;
      this.serviceBugs.deleteBug(bug.id).subscribe((response) =>
        this.serviceBugs.getBugs().subscribe(
          (response: Bug[]) => {
            this.dataSource = response;
            this.sendBugToNextTable(response);
          },
          (error) => {}
        )
      );
    });
  }

  classifyBug(bug) {
    this.router.navigate(['../bug/classify'], {
      relativeTo: this.r,
      queryParams: { id: String(bug.id) },
    });
  }

  sendBugToNextTable(bugs) {
    this.sendBugs.emit(bugs);
  }
  ngOnInit(): void {}

  createBug() {
    this.router.navigate(['../bug'], { relativeTo: this.r });
  }
}
