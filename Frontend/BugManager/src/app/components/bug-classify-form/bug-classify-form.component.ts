import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Bug } from 'src/app/models/Bug';
import { BugType } from 'src/app/models/BugType';
import { BugsService } from 'src/app/services/bug.service';
import { Display } from 'src/app/utils/display';
import { InfoMessage } from '../message/model/message';

@Component({
  selector: 'app-bug-classify-form',
  templateUrl: './bug-classify-form.component.html',
  styleUrls: ['./bug-classify-form.component.css'],
})
export class BugClassifyFormComponent implements OnInit {
  constructor(private route: ActivatedRoute, private bugService: BugsService) {}

  loading = false;
  infoMessage: InfoMessage = { error: true, text: '' };
  bug: Bug = {
    name: '',
    description: '',
    version: '',
    time: 0,
    projectId: 0,
    projectName: '',
    isActive: true,
  };

  form = new FormGroup({
    name: new FormControl('', [Validators.required]),
    description: new FormControl('', [Validators.required]),
    comments: new FormControl('', [Validators.required]),
    value: new FormControl('', [Validators.required]),
  });

  name: string = '';
  description: string = '';
  comments: string = '';
  value: string = '';
  valueOptions: string[] = [];

  display = Display.IsActiveAsResolve;
  bugType: BugType = {
    name: '',
    description: '',
    comments: '',
    value: '',
  };

  severity: string[] = ['Crítico', 'Mayor', 'Menor', 'Leve'];
  priority: string[] = ['Inmediata', 'Alta', 'Media', 'Baja'];

  ngOnInit(): void {
    this.bug = this.bugService.getBug(this.route.snapshot.queryParams['id']);
    alert(window.location.pathname.split('/')[1]);
    if (window.location.pathname.split('/')[1] == 'tester') {
      this.valueOptions = this.severity;
      alert('a');
    } else if (window.location.pathname.split('/')[1] == 'admin') {
      this.valueOptions = this.priority;
      alert('b');
    }
  }

  save() {
    this.loading = true;
    this.bugType.name = this.name;
    this.bugType.description = this.description;
    this.bugType.comments = this.comments;
    this.bugType.value = this.value;
    const id = this.route.snapshot.queryParams['id'];
    this.bugType.id = id;

    this.editBug(id);
  }

  editBug(id: number) {
    this.bugService.editBug(id, this.bug).subscribe(
      (response) => {
        this.loading = false;
        this.infoMessage.error = false;
        this.infoMessage.text = `Bug Type to "${this.bug.name}" added successfully`;
      },

      (error) => {
        this.loading = false;
        this.infoMessage.error = true;
        this.infoMessage.text = error;
      }
    );
  }
}
