import { Component, OnInit } from '@angular/core';
import { SideBarItem } from 'src/app/components/nav/model/sidebarItem';

@Component({
  selector: 'app-tester',
  templateUrl: './tester.component.html',
  styleUrls: ['./tester.component.css']
})
export class TesterComponent implements OnInit {

  testerMenuItems: SideBarItem[] = [
    { path: '/tester/bugs', title: 'Bugs', icon: 'bug_report' },
    { path: '/tester/assignments', title: 'Assignments', icon: 'assignment' },
  ]


  constructor() { }

  ngOnInit(): void {
  }

}
