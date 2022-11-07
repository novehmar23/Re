import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Developer } from 'src/app/models/Developer';
import { User } from 'src/app/models/User';
import { ProjectsService } from 'src/app/services/project.service';
import { Display } from 'src/app/utils/display';
import { ButtonAction } from '../../generic-table/models/buttonAction';
import { Column } from '../../generic-table/models/column';
import { ColumnType } from '../../generic-table/models/columnTypes';

@Component({
  selector: 'app-devs-project',
  templateUrl: './devs-project.component.html',
  styleUrls: ['./devs-project.component.css']
})
export class DevsProjectComponent implements OnInit {
  constructor(private service: ProjectsService, private route: ActivatedRoute) { }
  projectId: number;

  buttonsColumns: Column[] = [
    { header: "Project", property: "change", display: Display.id, type: ColumnType.Button },
  ]

  buttonsActions = new Map<string, ButtonAction>([
    ["change", { text: (u) => { return this.getButtonText(u) }, onClick: (u) => { this.changeProjectParticipance(u) }, color: (u) => this.getButtonColor(u) }],
  ]);

  userOnProject: Developer[] = [];

  changeProjectParticipance(u) {
    if (this.userInProject(u))
      this.removeDevToProject(u)
    else
      this.addDevToProject(u);
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


  addDevToProject(user: Developer) {
    let devId = user.id || 1;
    this.service.addDevToProject(this.projectId, devId).subscribe(
      (response) => {
        this.updateDevelopers();
      }
    );
  }

  removeDevToProject(user: Developer) {
    let devId = user.id || 1;
    this.service.removeDevFromProject(this.projectId, devId).subscribe(
      (response) => {
        this.updateDevelopers();
      }
    );
  }


  ngOnInit(): void {
    this.projectId = this.route.snapshot.queryParams["id"];
    this.updateDevelopers();
  }

  updateDevelopers() {
    this.service.getDevelopers(this.projectId).subscribe(

      (response: Developer[]) => {
        this.userOnProject = response;
      }
    );
  }

}
