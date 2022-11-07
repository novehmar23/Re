import { Component, Input, OnInit } from '@angular/core';
import { InfoMessage } from './model/message';

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.css']
})
export class MessageComponent implements OnInit {

  @Input() message: InfoMessage;
  constructor() { }

  ngOnInit(): void {
  }

}
