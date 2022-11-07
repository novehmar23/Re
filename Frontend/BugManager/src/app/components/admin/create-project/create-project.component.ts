import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ProjectsService } from 'src/app/services/project.service';
import { InfoMessage } from '../../message/model/message';

@Component({
  selector: 'app-create-project',
  templateUrl: './create-project.component.html',
  styleUrls: ['./create-project.component.css']
})
export class CreateProjectComponent implements OnInit {
  showCancelButton = true;
  loading = true;
  infoMessage: InfoMessage = { error: true, text: '' };


  form = new FormGroup({
    name: new FormControl('', [Validators.required]),
  });

  project = { name: "" };
  id: number;
  constructor(private route: ActivatedRoute, private service: ProjectsService) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.queryParams["id"];
    if (this.id != null)
      this.getProject(this.id);
    this.project.name = '';
    this.loading = false;
  }

  save() {
    if (this.id == null)
      this.createNewProject();
    else
      this.editProject(this.id);
  }

  getProject(id) {
    this.service.getProject(id).subscribe(

      (response) => {
        this.project.name = response.name;
        this.loading = false;
      },

      error => {
        this.loading = false;
        this.infoMessage.error = true;
        this.infoMessage.text = error

      });
  }
  createNewProject() {
    this.service.createProject(this.project).subscribe(

      (response) => {
        this.loading = false;
        this.infoMessage.error = false;
        this.infoMessage.text = `Project "${this.project.name}" added successfully`
        this.project.name = '';
        this.form.markAsUntouched();
      },

      error => {
        this.loading = false;
        this.infoMessage.error = true;
        this.infoMessage.text = error
      });
  }

  editProject(id: number) {
    this.service.editProject(this.project, id).subscribe(

      (response) => {
        this.loading = false;
        this.infoMessage.error = false;
        this.infoMessage.text = `Project "${this.project.name}" edited successfully`
      },

      error => {
        this.loading = false;
        this.infoMessage.error = true;
        this.infoMessage.text = error
      });
  }
}
