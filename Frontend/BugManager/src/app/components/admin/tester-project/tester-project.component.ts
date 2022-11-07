import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Tester } from 'src/app/models/Tester';
import { User } from 'src/app/models/User';
import { ProjectsService } from 'src/app/services/project.service';
import { TesterService } from 'src/app/services/tester.service';
import { Display } from 'src/app/utils/display';
import { ButtonAction } from '../../generic-table/models/buttonAction';
import { Column } from '../../generic-table/models/column';
import { ColumnType } from '../../generic-table/models/columnTypes';

@Component({
  selector: 'app-tester-project',
  templateUrl: './tester-project.component.html',
  styleUrls: ['./tester-project.component.css']
})
export class TestersProjectComponent implements OnInit {
  constructor(private service: ProjectsService, private route: ActivatedRoute, private testersService: TesterService) { }

  dataSource: Tester[] = [];
  buttonsColumns: Column[];
  loading = true;
  projectId: number;
  columns: Column[] = [
    { header: "Username", property: "username", display: Display.id, type: ColumnType.Object },
    { header: "Name", property: "name", display: Display.id, type: ColumnType.Object },
    { header: "Lastname", property: "lastname", display: Display.id, type: ColumnType.Object },
    { header: "Email", property: "email", display: Display.id, type: ColumnType.Object },
    { header: "Cost", property: "cost", display: Display.CostPerHour, type: ColumnType.Object },
    { header: "Project", property: "change", display: Display.id, type: ColumnType.Button },
  ]
  buttonsActions = new Map<string, ButtonAction>([
    ["change", { text: (u) => { return this.getButtonText(u) }, onClick: (u) => { this.changeProjectParticipance(u) }, color: (u) => this.getButtonColor(u) }],
  ]);
  userOnProject: Tester[];

  changeProjectParticipance(u) {
    if (this.userInProject(u))
      this.removeTesterFromProject(u)
    else
      this.addTesterToProject(u);
  }

  getButtonColor(u) {
    if (this.userInProject(u))
      return "warn"
    else
      return "primary"
  }

  getButtonText(u) {
    if (this.userInProject(u))
      return "Remove"
    else
      return "Add"
  }

  userInProject(u: User) {
    return this.userOnProject.find(user => user.id == u.id) != null
  }


  addTesterToProject(user: Tester) {
    let testerId = user.id || 0;
    this.service.addTesterToProject(this.projectId, testerId).subscribe(
      (response) => {
        this.updateTesters();
      }
    );
  }

  removeTesterFromProject(user: Tester) {
    let testerId = user.id || 0;
    this.service.removeTesterFromProject(this.projectId, testerId).subscribe(
      (response) => {
        this.updateTesters();
      }
    );
  }


  ngOnInit(): void {
    this.projectId = this.route.snapshot.queryParams["id"];
    this.testersService.getTesters().subscribe(

      (response: Tester[]) => {
        this.loading = false;
        this.dataSource = response;
      },

      error => {
        this.loading = false;
      }
    );
    this.updateTesters();
  }

  updateTesters() {
    this.service.getTesters(this.projectId).subscribe(

      (response: Tester[]) => {
        this.userOnProject = response;
      }
    );
  }

}
