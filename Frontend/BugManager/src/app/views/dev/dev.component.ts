import { Component, OnInit } from '@angular/core';
import { SideBarItem } from 'src/app/components/nav/model/sidebarItem';

@Component({
  selector: 'app-dev',
  templateUrl: './dev.component.html',
  styleUrls: ['./dev.component.css']
})
export class DevComponent implements OnInit {

  devMenuItems: SideBarItem[] = [
    { path: '/dev/bugs', title: 'Bugs', icon: 'bug_report' },
    { path: '/dev/assignments', title: 'Assignments', icon: 'assignment' },
  ]

  constructor() { }

  ngOnInit(): void {
  }

}
