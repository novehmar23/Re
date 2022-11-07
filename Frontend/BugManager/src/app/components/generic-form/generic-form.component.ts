import { Component, EventEmitter, Input, OnInit, Output, } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { InfoMessage } from '../message/model/message';
import { Location } from '@angular/common';

@Component({
  selector: 'app-generic-form',
  templateUrl: './generic-form.component.html',
  styleUrls: ['./generic-form.component.css']
})
export class GenericFormComponent implements OnInit {
  @Input() title: string = "Form";
  @Input() form: FormGroup = new FormGroup({});
  @Input() infoMessage: InfoMessage = { error: false, text: "" }
  @Input() showCancelButton: boolean = true;
  @Input() loading: boolean

  @Output() save = new EventEmitter();

  cancel() {
    this.location.back();
  }
  constructor(private location: Location) { }

  ngOnInit(): void {
  }


}
