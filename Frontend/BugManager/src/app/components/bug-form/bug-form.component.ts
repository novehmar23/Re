import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Bug } from 'src/app/models/Bug';
import { Developer } from 'src/app/models/Developer';
import { Project } from 'src/app/models/Project';
import { BugsService } from 'src/app/services/bug.service';
import { DeveloperService } from 'src/app/services/developer.service';
import { ProjectsService } from 'src/app/services/project.service';
import { Display } from 'src/app/utils/display';
import { InfoMessage } from '../message/model/message';

@Component({
  selector: 'app-bug-form',
  templateUrl: './bug-form.component.html',
  styleUrls: ['./bug-form.component.css']
})
export class BugFormComponent implements OnInit {
  constructor(private route: ActivatedRoute, private projectService: ProjectsService,
    private devService: DeveloperService, private bugService: BugsService) { }

  loading = false;
  infoMessage: InfoMessage = { error: true, text: '' };

  form = new FormGroup({
    name: new FormControl('', [Validators.required]),
    description: new FormControl('', [Validators.required]),
    project: new FormControl('', [Validators.required]),
    version: new FormControl('', [Validators.required]),
    time: new FormControl('', [Validators.required]),
    isActive: new FormControl('', [])
  });

  projects: Project[] = [];
  selectedProjectId: number;

  devs: Developer[] = []
  selectedDevId?: number;

  display = Display.IsActiveAsResolve;
  bug: Bug = { name: '', description: '', version: '', time: 0, projectId: 0, projectName: '', isActive: true }


  ngOnInit(): void {
    this.loadProjects();
    this.loadBug();
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

  loadDevs() {
    this.projectService.getDevelopers(this.selectedProjectId).subscribe(

      (response: Developer[]) => {
        this.loading = false;
        this.devs = response;
      },

      error => {
        this.loading = false;
        this.infoMessage.error = true;
        this.infoMessage.text = `Problem loading developers: ${error}`
      });
    this.devs = [];
  }

  loadBug() {
    let id = this.route.snapshot.queryParams["id"];
    if (id == null)
      return;

    this.bugService.getBug(id).subscribe(

      (response: Bug) => {
        this.loading = false;
        this.bug = response;
        this.selectedProjectId = this.bug.projectId;
        this.loadDevs();
        this.selectedDevId = this.bug.completedById || undefined;
      },

      error => {
        this.loading = false;
        this.infoMessage.error = true;
        this.infoMessage.text = `Problem loading bug: ${error}`
      });
  }

  save() {
    this.loading = true;
    this.bug.projectId = this.selectedProjectId;
    this.bug.projectName = this.projects.find(p => p.id == this.selectedProjectId)?.name || "";
    if (!this.bug.isActive)
      this.bug.completedById = this.selectedDevId
    else
      this.bug.completedById = undefined;

    let id = this.route.snapshot.queryParams["id"];
    if (id == null)
      this.createBug()
    else
      this.editBug(id)
  }

  createBug() {
    this.bugService.createBug(this.bug).subscribe(

      (response) => {
        this.loading = false;
        this.infoMessage.error = false;
        this.infoMessage.text = `Bug "${this.bug.name}" added successfully`
        //this.form.reset();
        this.bug  = { name: '', description: '', version: '', time: 0, projectId: 0, projectName: '', isActive: true }
        this.form.markAsUntouched();
      },

      error => {
        this.loading = false;
        this.infoMessage.error = true;
        this.infoMessage.text = error
      });

  }

  editBug(id: number) {
    this.bugService.editBug(id, this.bug).subscribe(

      (response) => {
        this.loading = false;
        this.infoMessage.error = false;
        this.infoMessage.text = `Bug "${this.bug.name}" edited successfully`
      },

      error => {
        this.loading = false;
        this.infoMessage.error = true;
        this.infoMessage.text = error
      });

  }
}
