import { Component, OnInit } from '@angular/core';
import { Bug } from 'src/app/models/Bug';
import { BugsService } from 'src/app/services/bug.service';
import { Display } from 'src/app/utils/display';
import { ButtonAction } from '../../generic-table/models/buttonAction';
import { Column } from '../../generic-table/models/column';
import { ColumnType } from '../../generic-table/models/columnTypes';

@Component({
  selector: 'app-resolve-bugs',
  templateUrl: './resolve-bugs.component.html',
  styleUrls: ['./resolve-bugs.component.css']
})
export class ResolveBugsComponent implements OnInit {

  constructor(private service: BugsService) { }
  dataSource: Bug[];

  buttonsColumns: Column[] = [
    { header: "Change State", property: "changeState", display: Display.id, type: ColumnType.Button },
  ]

  buttonsActions = new Map<string, ButtonAction>([
    ["changeState", { text: (b) => this.getButtonText(b), onClick: (b) => this.changeState(b), color: (b) => this.getButtonColor(b) }],
  ]);


  changeState(b: Bug) {
    if (b.isActive)
      this.resolveBug(b)
    else
      this.unresolveBug(b);
  }

  getButtonColor(b) {
    if (b.isActive)
      return "primary"
    else
      return "accent"
  }

  getButtonText(b) {
    if (b.isActive)
      return "Resolve"
    else
      return "Unresolve"
  }

  resolveBug(bug: Bug) {
    this.service.resolveBug(bug.id || 0).subscribe(
      (response) => {
        this.reloadBugs();
      }
    );
  }

  unresolveBug(bug: Bug) {
    this.service.unresolveBug(bug.id || 0).subscribe(
      (response) => {
        this.reloadBugs();
      }
    );
  }

  reloadBugs() {
    this.service.getBugs().subscribe(

      (response: Bug[]) => {
        this.dataSource = response;
      });
  }



  ngOnInit(): void {
  }

}
