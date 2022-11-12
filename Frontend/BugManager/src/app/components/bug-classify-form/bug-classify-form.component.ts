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

  severity: string[] = ['Critico', 'Mayor', 'Menor', 'Leve'];
  priority: string[] = ['Inmediata', 'Alta', 'Media', 'Baja'];
  valueType: string = '';

  ngOnInit(): void {
    if (window.location.pathname.split('/')[1] == 'tester') {
      this.valueOptions = this.severity;
      this.valueType = 'severity';
    } else if (window.location.pathname.split('/')[1] == 'admin') {
      this.valueOptions = this.priority;
      this.valueType = 'priority';
    }
  }

  save() {
    this.loading = true;
    this.bugType.value = this.value;

    this.classifyBug(this.route.snapshot.queryParams['id']);
  }

  classifyBug(id: number) {
    this.bugService.classifyBug(id, this.bugType, this.valueType).subscribe(
      (response) => {
        this.loading = false;
        this.infoMessage.error = false;
        this.infoMessage.text = `Bug Type added successfully`;
      },

      (error) => {
        this.loading = false;
        this.infoMessage.error = true;
        this.infoMessage.text = error;
      }
    );
  }
}
