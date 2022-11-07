import { Component, OnInit } from '@angular/core';
import { Column } from '../../generic-table/models/column';
import { ColumnType } from '../../generic-table/models/columnTypes';
import { ButtonAction } from '../../generic-table/models/buttonAction';
import { Project } from 'src/app/models/Project';
import { ActivatedRoute, Router } from '@angular/router';
import { ProjectsService } from 'src/app/services/project.service';
import { Display } from 'src/app/utils/display';
import { MatDialog } from '@angular/material/dialog';
import { DeleteDialogComponent } from '../../delete-dialog/delete-dialog.component';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.css']
})
export class ProjectsComponent implements OnInit {

  dataSource: Project[];
  loading = true;

  buttonsColumns: Column[] = [
    { header: "Testers", property: "testers", display: Display.id, type: ColumnType.Button },
    { header: "Developers", property: "devs", display: Display.id, type: ColumnType.Button },
    { header: "Edit Name", property: "edit", display: Display.id, type: ColumnType.Button },
    { header: "Delete", property: "delete", display: Display.id, type: ColumnType.Button },
  ]

  buttonsActions = new Map<string, ButtonAction>([
    ["testers", { text: () => "Testers", onClick: (p) => { this.tester(p) }, color: () => "accent" }],
    ["devs", { text: () => "Developers", onClick: (p) => { this.developers(p) }, color: () => "accent" }],
    ["edit", { text: () => "Edit", onClick: (p) => { this.edit(p) }, color: () => "primary" }],
    ["delete", { text: () => "Delete", onClick: (p) => { this.delete(p) }, color: () => "warn" }],
  ]);

  constructor(private router: Router, private projectService: ProjectsService, private r: ActivatedRoute, public dialog: MatDialog) { }

  edit(project) {
    this.router.navigate(["../project"], { relativeTo: this.r, queryParams: { id: String(project.id) } });
  }

  tester(project) {
    this.router.navigate(["../project/testers"], { relativeTo: this.r, queryParams: { id: String(project.id) } });
  }

  developers(project) {
    this.router.navigate(["../project/devs"], { relativeTo: this.r, queryParams: { id: String(project.id) } });
  }


  delete(project) {
    let dialogRef = this.dialog.open(DeleteDialogComponent);
    dialogRef.afterClosed().subscribe(result => {

      if (!result)
        return

      this.projectService.deleteProject(project.id).subscribe(
        (response) => { this.loadProjects() },
        (error) => { }
      );
    });
  }

  createProject() {
    this.router.navigate(["../project"], { relativeTo: this.r });
  }

  ngOnInit(): void {
    this.loadProjects();
  }

  loadProjects() {
    this.loading = true;
    this.projectService.getProjects().subscribe(

      (response: Project[]) => {
        this.loading = false;
        this.dataSource = response;
      },

      error => {
        this.loading = false;
      }
    );
  }
}
