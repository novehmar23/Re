import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Assignment } from 'src/app/models/Assignment';
import { Project } from 'src/app/models/Project';
import { AssignmentService } from 'src/app/services/assignment.service';
import { ProjectsService } from 'src/app/services/project.service';
import { InfoMessage } from '../../message/model/message';

@Component({
  selector: 'app-create-assignment',
  templateUrl: './create-assignment.component.html',
  styleUrls: ['./create-assignment.component.css']
})
export class CreateAssignmentComponent implements OnInit {

  constructor(private route: ActivatedRoute, private projectService: ProjectsService, private assignmentService: AssignmentService) { }

  loading = false;
  infoMessage: InfoMessage = { error: true, text: '' };

  form = new FormGroup({
    name: new FormControl('', [Validators.required]),
    time: new FormControl('', [Validators.required]),
    project: new FormControl('', [Validators.required]),
    cost: new FormControl('', [Validators.required])
  });

  projects: Project[] = [];
  selectedProjectId: number;

  assignment: Assignment = { name: '', time: 0, cost: 0, projectId: 0, projectName: '' }

  ngOnInit(): void {
    this.loadProjects();
    this.loadAssignment();
  }

  loadProjects() {
    this.projectService.getProjects().subscribe(

      (response: Project[]) => {
        this.loading = false;
        this.projects = response;
        if (this.projects.length == 0)
          this.infoMessage = { error: true, text: "You don't have any assigned projects yet" };
      },

      error => {
        this.loading = false;
        this.infoMessage.error = true;
        this.infoMessage.text = `Problem loading projects: ${error}`
      });
  }

  loadAssignment() {
    let id = this.route.snapshot.queryParams["id"];
    if (id == null)
      return;

    this.assignmentService.getAssignment(id).subscribe(

      (response: Assignment) => {
        this.loading = false;
        this.assignment = response;
        this.selectedProjectId = this.assignment.projectId;
      },

      error => {
        this.loading = false;
        this.infoMessage.error = true;
        this.infoMessage.text = `Problem loading bug: ${error}`
      });
  }

  save() {
    this.loading = true;
    this.assignment.projectId = this.selectedProjectId;
    this.assignment.projectName = this.projects.find(p => p.id == this.selectedProjectId)?.name || "";
    this.createAssignment()
  }

  createAssignment() {
    this.assignmentService.createAssignment(this.assignment).subscribe(

      (response) => {
        this.loading = false;
        this.infoMessage.error = false;
        this.infoMessage.text = `Assignment "${this.assignment.name}" added successfully`
        //this.form.reset();
        this.assignment.name = '';
        this.assignment.cost = 0;
        this.assignment.time = 0;
        this.form.markAsUntouched();
      },

      error => {
        this.loading = false;
        this.infoMessage.error = true;
        this.infoMessage.text = error
      });

  }
}
